using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int Population;
    [Header("int Resources")]
    private int Money;
    private int Research_Points;
    [Header("Demand")]
    public int Demand_Housing;
    public int Demand_Commercial;
    public int Demand_Elec;
    public int Demand_Water;
    public int Demand_Magic;
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI Text_Money;
    [SerializeField] private TextMeshProUGUI Text_Research_Points;
    [SerializeField] private TextMeshProUGUI Text_Population;

    void Start()
    {
        Money = PlayerPrefs.GetInt("Money", 400000);
        Population = PlayerPrefs.GetInt("Population", 0);
        Research_Points = PlayerPrefs.GetInt("Research_Points", 10);
    }

    void Update()
    {
        UpdateUI();

        //TEMP
        if (Input.GetKeyUp(KeyCode.F9))
        {
            DeleteProgress();
        }
    }

    public void UpdateUI()
    {
        Text_Money.text = Money.ToString();
        Text_Research_Points.text = Research_Points.ToString();
        Text_Population.text = Population.ToString();
    }

    #region Save Data
    //save da
    void SaveInformation()
    {
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.SetInt("Population", Population);
        PlayerPrefs.SetInt("Research_Points", Research_Points);
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
    #endregion 
}
