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
    [Header("Demand & Usage")]
    public int Demand_Housing;
    public int Demand_Commercial;
    public int Demand_Elec;
    public int Demand_Water;
    public int Demand_Magic;
    public int Usage_Magic;
    public int Usage_Water;
    public int Usage_Elec;
    [Header("Generation")]
    public int MoneyGeneration = 0;
    public int WaterGeneration = 0;
    public int MagicGeneration = 0;
    public int ElectraGeneration = 0;
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI Text_Money;
    [SerializeField] private TextMeshProUGUI Text_MoneyPerHour;
    [SerializeField] private TextMeshProUGUI Text_Research_Points;
    [SerializeField] private TextMeshProUGUI Text_Population;
    //Demand
    [SerializeField] private TextMeshProUGUI Text_Demand_Housing;
    [SerializeField] private TextMeshProUGUI Text_Demand_Commercial;
    [SerializeField] private TextMeshProUGUI Text_Demand_Electricity;
    [SerializeField] private TextMeshProUGUI Text_Demand_Water;
    [SerializeField] private TextMeshProUGUI Text_Demand_Magic;
    [Header("Other")]
    [SerializeField] private Tilemap Buildable_Tilemap; // For saving the tiles
    [SerializeField] private TimeManager TimeManager;

    private void Awake()
    {
        TimeManager = FindFirstObjectByType<TimeManager>();
    }

    void Start()
    {
        GetSaveData();
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
        Text_MoneyPerHour.text = MoneyGeneration.ToString() + " Per Day";
        Text_Research_Points.text = Research_Points.ToString();
        Text_Population.text = Population.ToString();
        //Demand
        Text_Demand_Housing.text = Demand_Housing.ToString();
        Text_Demand_Commercial.text = Text_Demand_Commercial.ToString();
        Text_Demand_Water.text = Demand_Water.ToString();
        Text_Demand_Electricity.text = Demand_Elec.ToString();
        Text_Demand_Magic.text = Demand_Magic.ToString();
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
        //Demand
        PlayerPrefs.SetInt("Demand_Housing", Demand_Housing);
        PlayerPrefs.SetInt("Demand_Commercial", Demand_Commercial);
        PlayerPrefs.SetInt("Demand_Elec", Demand_Elec);
        PlayerPrefs.SetInt("Demand_Water", Demand_Water);
        PlayerPrefs.SetInt("Demand_Magic", Demand_Magic);
        SaveMap();
    }

    void GetSaveData()
    {
        Money = PlayerPrefs.GetInt("Money", 400000);
        Population = PlayerPrefs.GetInt("Population", 0);
        Research_Points = PlayerPrefs.GetInt("Research_Points", 10);
        MoneyGeneration = PlayerPrefs.GetInt("MoneyGeneration", 0);
        TimeManager.Day = PlayerPrefs.GetInt("Day", 1);
        TimeManager.Month = PlayerPrefs.GetInt("Month", 1);
        TimeManager.Year = PlayerPrefs.GetInt("Year", 1);
        //Demand
        Demand_Housing = PlayerPrefs.GetInt("Demand_Housing", 0);
        Demand_Commercial = PlayerPrefs.GetInt("Demand_Commercial", 0);
        Demand_Water = PlayerPrefs.GetInt("Demand_Water", 0);
        Demand_Elec = PlayerPrefs.GetInt("Demand_Elec", 0);
        Demand_Magic = PlayerPrefs.GetInt("Demand_Magic", 0);
        LoadMap();
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

    private void LoadMap()
    {

    }
    #endregion 
}
