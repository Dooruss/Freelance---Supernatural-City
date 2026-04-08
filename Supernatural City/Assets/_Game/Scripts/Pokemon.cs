using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour
{
    public TMPro.TMP_InputField inputfield;
    public Image Preview;
    private AudioSource audiosource;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
      if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (inputfield.text.Length > 0 )
            {
                StartCoroutine(CheckApi());
            }
        }   
    }

    private IEnumerator CheckApi()
    {
        Pokedata data;

        using (UnityWebRequest request = UnityWebRequest.Get("https://pokeapi.co/api/v2/pokemon/" + inputfield.text))
        {
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                yield break;
            }
            data = JsonUtility.FromJson<Pokedata>(request.downloadHandler.text);
        }

        if (data != null)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(data.sprites.front_default))
            {
                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    yield break;
                }
                Texture2D preview = DownloadHandlerTexture.GetContent(request);
                Preview.sprite = Sprite.Create(preview, new Rect(0, 0, preview.width, preview.height), new Vector2(0.5f, 0.5f));
            }

            using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(data.cries.latest, AudioType.OGGVORBIS))
            {
                yield return request.SendWebRequest();
                AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                audiosource.clip = clip;
                audiosource.Play();
            }
        }
    }


    [Serializable]
    public class Pokedata
    {
        public string name;
        public Pokesprite sprites;
        public PokeCry cries;
    }

    [Serializable]
    public class Pokesprite
    {
       public string front_default;
    }
    [Serializable]
    public class PokeCry
    {
       public string latest;
    }
}

