using UnityEngine;

public class Close_UI : MonoBehaviour
{
    [SerializeField] private GameObject[] UI_Objects;

    public void CloseUI(GameObject TurnOnObj)
    {
        foreach (GameObject obj in UI_Objects)
        {
            obj.SetActive(false);
            if (TurnOnObj != null) { TurnOnObj.SetActive(true); }
        }
    }
}
