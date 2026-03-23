using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    public int Population;
    public int CommercialAmount;
    [Header("int Resources")]
    public int Money;
    public int Research_Points;
    [Header("Demand & Usage")]
    public float Demand_Housing;
    public float Demand_Commercial;
    public float Demand_Elec;
    public float Demand_Water;
    public float Demand_Magic;
    //usage
    public int Usage_Magic;
    public int Usage_Water;
    public int Usage_Elec;
    [Header("Generation")]
    public int MoneyGeneration = 0;
    public int UpKeepCosts;
    public int WaterGeneration = 0;
    public int MagicGeneration = 0;
    public int ElectraGeneration = 0;
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI Text_Money;
    [SerializeField] private TextMeshProUGUI Text_MoneyPerHour;
    [SerializeField] private TextMeshProUGUI Text_Research_Points;
    [SerializeField] private TextMeshProUGUI Text_Population;
    [SerializeField] private GameObject[] Infos;
    //Demand
    [SerializeField] private TextMeshProUGUI Text_Demand_Housing;
    [SerializeField] private TextMeshProUGUI Text_Demand_Commercial;
    [SerializeField] private TextMeshProUGUI Text_Demand_Electricity;
    [SerializeField] private TextMeshProUGUI Text_Demand_Water;
    [SerializeField] private TextMeshProUGUI Text_Demand_Magic;
    [SerializeField] private TextMeshProUGUI[] Text_Production;
    [SerializeField] private TextMeshProUGUI[] Text_Usage;
    [SerializeField] private TextMeshProUGUI Error_Text;
    [SerializeField] private GameObject Error_Box;
    [Header("Other")]
    [SerializeField] private Tilemap Buildable_Tilemap; // For saving the tiles
    [SerializeField] private TimeManager TimeManager;
    private Color Red = new Color(224, 70, 45, 255);
    private Color Green = new Color(42, 128, 20, 255);

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
        Check_Illegall();
        ManageDemand();

        //TEMP
        if (Input.GetKeyUp(KeyCode.F9))
        {
            DeleteProgress();
        }
        Money += MoneyGeneration - UpKeepCosts;
        if (ElectraGeneration < Usage_Elec || WaterGeneration < Usage_Water || MagicGeneration < Usage_Magic)
        {
            Resource_Error(true);
        }
        else { Resource_Error(false); }
    }

    private void ManageDemand()
    {
        //Housing
        if (CommercialAmount * 5  > Population )
        {
            Demand_Housing += 0.20f;
            Demand_Commercial -= 0.10f;
        }

        //Commercial
        if (Population > CommercialAmount * 10)
        {
            Demand_Commercial += 0.20f;
            Demand_Housing -= 0.20f;
        }

        //Resources

    }

    private void Check_Illegall()
    {
        if (Demand_Commercial > 1f) { Demand_Commercial = 1f; }
        if (Demand_Commercial < 0f) { Demand_Commercial = 0f; }
        if (Demand_Elec > 1f) { Demand_Elec = 1f; }
        if (Demand_Elec < 0f) { Demand_Elec = 0f; }
        if (Demand_Housing > 1f) { Demand_Housing = 1f; }
        if (Demand_Housing < 0f) { Demand_Housing = 0f; }
        if (Demand_Magic > 1f) { Demand_Magic = 1f; }
        if (Demand_Magic < 0f) { Demand_Magic = 1f; }
        if (Demand_Water > 1f) { Demand_Water = 1f; }
        if (Demand_Water < 0f) { Demand_Water = 0f; }
    }
    #region UI stuff
    public void UpdateUI()
    {
        Text_Money.text = Money.ToString();
        Text_MoneyPerHour.text = MoneyGeneration - UpKeepCosts + " Per Day";
        if (MoneyGeneration - UpKeepCosts <= 0) { Text_MoneyPerHour.color = Red / 255; }
        else { Text_MoneyPerHour.color = Green / 255; }
        Text_Research_Points.text = Research_Points.ToString();
        Text_Population.text = Population.ToString();
        //Demand
        Text_Demand_Housing.text = Demand_Housing.ToString();
        Text_Demand_Commercial.text = Demand_Commercial.ToString();
        Text_Demand_Water.text = Demand_Water.ToString();
        Text_Demand_Electricity.text = Demand_Elec.ToString();
        Text_Demand_Magic.text = Demand_Magic.ToString();
        //Usage & Production
        Text_Production[0].text = "Production: " + WaterGeneration.ToString();
        Text_Usage[0].text = "Usage: " + Usage_Water.ToString();
        Text_Production[1].text = "Production: " + ElectraGeneration.ToString();
        Text_Usage[1].text = "Usage: " + Usage_Elec.ToString();
        Text_Production[2].text = "Production: " + MagicGeneration.ToString();
        Text_Usage[2].text = "Usage: " + Usage_Magic.ToString();
    }

    public void CloseInfoUI()
    {
        foreach (GameObject info in Infos)
        {
            info.SetActive(false);
        }
    }

    public void Resource_Error(bool TheBool)
    {
        Error_Box.gameObject.SetActive(TheBool);
        Error_Text.text = "You lack the following: ";
        List<string> lacking = new List<string>();
        if (WaterGeneration < Usage_Water) { lacking.Add("Water"); }
        if (ElectraGeneration < Usage_Elec) { lacking.Add("Electricity"); }
        if (MagicGeneration < Usage_Magic) { lacking.Add("Magic"); }
        Error_Text.text += string.Join(", ", lacking);
        Error_Text.text.TrimEnd(',');
    }
    #endregion

    #region Save Data
    void SaveInformation()
    {
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.SetInt("UpKeep", UpKeepCosts);
        PlayerPrefs.SetInt("Population", Population);
        PlayerPrefs.SetInt("Research_Points", Research_Points);
        PlayerPrefs.SetInt("Day", TimeManager.Day);
        PlayerPrefs.SetInt("Month", TimeManager.Month);
        PlayerPrefs.SetInt("Year", TimeManager.Year);
        //Demand
        PlayerPrefs.SetFloat("Demand_Housing", Demand_Housing);
        PlayerPrefs.SetFloat("Demand_Commercial", Demand_Commercial);
        PlayerPrefs.SetFloat("Demand_Elec", Demand_Elec);
        PlayerPrefs.SetFloat("Demand_Water", Demand_Water);
        PlayerPrefs.SetFloat("Demand_Magic", Demand_Magic);
        //Generation 
        PlayerPrefs.SetInt("MoneyGeneration", MoneyGeneration);
        PlayerPrefs.SetInt("ElectraGeneration", ElectraGeneration);
        PlayerPrefs.SetInt("WaterGeneration", WaterGeneration);
        PlayerPrefs.SetInt("MagicGeneration", MagicGeneration);

        SaveMap();
    }

    void GetSaveData()
    {
        Money = PlayerPrefs.GetInt("Money", 400000);
        UpKeepCosts = PlayerPrefs.GetInt("UpKeep", 5);
        Population = PlayerPrefs.GetInt("Population", 0);
        Research_Points = PlayerPrefs.GetInt("Research_Points", 10);
        TimeManager.Day = PlayerPrefs.GetInt("Day", 1);
        TimeManager.Month = PlayerPrefs.GetInt("Month", 1);
        TimeManager.Year = PlayerPrefs.GetInt("Year", 1);
        //Demand
        Demand_Housing = PlayerPrefs.GetInt("Demand_Housing", 1);
        Demand_Commercial = PlayerPrefs.GetInt("Demand_Commercial", 0);
        Demand_Water = PlayerPrefs.GetInt("Demand_Water", 0);
        Demand_Elec = PlayerPrefs.GetInt("Demand_Elec", 0);
        Demand_Magic = PlayerPrefs.GetInt("Demand_Magic", 0);
        //Generation
        MoneyGeneration = PlayerPrefs.GetInt("MoneyGeneration", 0);
        ElectraGeneration = PlayerPrefs.GetInt("ElectraGeneration", 0);
        WaterGeneration = PlayerPrefs.GetInt("WaterGeneration", 0);
        MagicGeneration = PlayerPrefs.GetInt("MagicGeneration", 0);
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
