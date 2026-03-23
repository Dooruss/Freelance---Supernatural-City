using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class Building : ScriptableObject
{
    public string Building_Name;
    public bool Need_Road;
    public int People_Amount;
    public int Cost;
    public int UpKeepCost;
    public int ResearchPointsAdded;
    public TileBase Building_Sprite;
    // Needs
    public int Need_Electricity;
    public int Need_Water;
    public int Need_Magic;
    public bool Functional;
    // Producing?
    [SerializeField] public Product_Produced Producing;
    [SerializeField] public Building_Type Type;
    [SerializeField] public int MoneyProduceAmount;
    [SerializeField] public int Product_Produce_Amount;
    public enum Product_Produced { None, Commercial, Electricity, Water, Magic }
    public enum Building_Type { Road, Housing, Commercial, Park, Generator }




   
}
