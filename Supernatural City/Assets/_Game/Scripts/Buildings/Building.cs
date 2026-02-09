using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class Building : ScriptableObject
{
    public string Building_Name;
    public bool Need_Road;
    public int People_Amount;
    public Sprite Building_Sprite;
    // Needs
    public float Need_Electricity;
    public float Need_Water;
    public float Need_Magic;
    public bool Functional;
    // Producing?
    [SerializeField] public Product_Produced Producing;
    public enum Product_Produced { None, Commercial, Electricity, Water, Magic }

    public void Check_Function(float Water_Amount, float Electricity_Amount, float Magic_Amount)
    {
        if (Water_Amount > Need_Water && Electricity_Amount > Need_Electricity && Magic_Amount > Need_Magic)
        {
            Functional = true;
        }
        else
        {
            Functional = false;
            //Put Type of issue on the building like on top via UI
        }
    }



}
