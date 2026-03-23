using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class Building : ScriptableObject
{
    [Header("Main")]
    public string Building_Name;
    public bool Need_Road;
    public TileBase Building_Sprite;
    [SerializeField] public Building_Type Type;
    [Header("Population & Money")]
    [SerializeField] public Population_Type Population_Sort;
    public int People_Amount;
    public int Cost;
    public int UpKeepCost;
    [SerializeField] public int MoneyProduceAmount;
    public int ResearchPointsAdded;
    [Header("Needs")]
    public int Need_Electricity;
    public int Need_Water;
    public int Need_Magic;
    public bool Functional;
    [Header("Producing")]
    [SerializeField] public Product_Produced Producing;
    
    [SerializeField] public int Product_Produce_Amount;
    public enum Product_Produced { None, Electricity, Water, Magic }
    public enum Building_Type { Road, Housing, Commercial, Park, Generator }
    public enum Population_Type { Witch , Vampire}




   
}
