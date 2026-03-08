using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    public int Population;
    [Header("int Resources")]
    public int Money;
    private int Research_Points;
    [Header("Demand")]
    public int Demand_Housing;
    public int Demand_Commercial;
    public int Demand_Elec;
    public int Demand_Water;
    public int Demand_Magic;
    public int MoneyGeneration = 0;
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI Text_Money;
    [SerializeField] private TextMeshProUGUI Text_Research_Points;
    [SerializeField] private TextMeshProUGUI Text_Population;
    [Header("Other")]
    [SerializeField] private Tilemap Buildable_Tilemap; // For saving the tiles
    [SerializeField] private TimeManager TimeManager;

    private void Awake()
    {
        TimeManager = FindFirstObjectByType<TimeManager>();
    }

    void Start()
    {
        Money = PlayerPrefs.GetInt("Money", 400000);
        Population = PlayerPrefs.GetInt("Population", 0);
        Research_Points = PlayerPrefs.GetInt("Research_Points", 10);
        MoneyGeneration = PlayerPrefs.GetInt("MoneyGeneration" , 0);
        TimeManager.Day = PlayerPrefs.GetInt("Day", 1);
        TimeManager.Month = PlayerPrefs.GetInt("Month", 1);
        TimeManager.Year = PlayerPrefs.GetInt("Year", 1);
    }

    void Update()
    {
        UpdateUI();

        //TEMP
        if (Input.GetKeyUp(KeyCode.F9))
        {
            DeleteProgress();
        }
        Money += MoneyGeneration;
    }

    public void UpdateUI()
    {
        Text_Money.text = Money.ToString();
        Text_Research_Points.text = Research_Points.ToString();
        Text_Population.text = Population.ToString();
    }

    #region Save Data
    void SaveInformation()
    {
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.SetInt("Population", Population);
        PlayerPrefs.SetInt("Research_Points", Research_Points);
        PlayerPrefs.SetInt("MoneyGeneration", MoneyGeneration);
        PlayerPrefs.SetInt("Day", TimeManager.Day);
        PlayerPrefs.SetInt("Month", TimeManager.Month);
        PlayerPrefs.SetInt("Year", TimeManager.Year);
        SaveMap();
    }

    void DeleteProgress()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Main");
    }
    private void OnApplicationQuit()
    {
        SaveInformation();
    }

    private void SaveMap()
    {
        //foreach(Tile in Buildable_Tilemap)
        //{

        //}
    }
    #endregion 
}
