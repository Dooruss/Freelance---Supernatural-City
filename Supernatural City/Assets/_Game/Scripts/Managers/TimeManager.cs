using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int Day;
    private int Year;
    public bool IsPaused;
    [SerializeField] private GameObject PauseScreen;

    void Start()
    {
        Day = 1;
        Year = 1;
    }


    void Update()
    {
        if (IsPaused) { PauseGame(false); }
    }

    public void PauseGame(bool ByButton)
    {
        Time.timeScale = 0;
        if (ByButton) { PauseScreen.SetActive(true); }
    }

    public void UnPauseGame(bool ByButton)
    {
        Time.timeScale = 1;
        if (ByButton) { PauseScreen.SetActive(false); }
    }
}
