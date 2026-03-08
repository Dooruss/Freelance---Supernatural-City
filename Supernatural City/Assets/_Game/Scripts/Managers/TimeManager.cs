using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int Day;
    public int Month;
    public int Year;
    public bool IsPaused;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private TextMeshProUGUI TimeText;

    void Update()
    {
        if (IsPaused) { PauseGame(false); }
        TimeUI();
    }

    void TimeUI()
    {
        TimeText.text = "Date " + Day + "/ " + Month + "/ " + Year;
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

    public void SpeedTime(float Times)
    {
        Time.timeScale = 1 * Times;
    }
}
