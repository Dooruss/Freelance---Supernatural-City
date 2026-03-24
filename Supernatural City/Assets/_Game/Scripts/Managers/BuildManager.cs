using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class BuildManager : MonoBehaviour
{
    // Placing system by https://www.youtube.com/watch?v=snUe2oa_iM0 
    [SerializeField] Tilemap CurrentTileMap;
    [SerializeField] Tilemap GroundTileMap;
    [SerializeField] public TileBase CurrentTile;
    [SerializeField] public Building CurrentObject;
    [SerializeField] private Building RoadTile;
    [SerializeField] Camera Camera;
    [SerializeField] private Building[] AllHousing;
    public bool BulldoseEnable;
    private GameManager gameManager;
    Dictionary<Vector3Int, Building> PlacedBuildings = new Dictionary<Vector3Int, Building>();
    // Hi Dorus from a couple days for now incase u forgot how this works
    // Ty for unity discussions and Reddit for this , So like It saves a Position (Vector3Int) and like an value on it (A building type)
    // So like to save stuff its PlacedBuildings[PositionHere] (KEY) = Building (VALUE) ; (0,0,0) = Road (For example)
    // And to remove its PlacedBuildings.Remove(PositionHere);

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        Vector3 worldPos = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int pos = CurrentTileMap.WorldToCell(worldPos);
        pos.z = 0;

        if (CurrentTile != null && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            PlaceTile(pos, CurrentObject);
        }
        if (BulldoseEnable == true && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            DeleteTile(pos, CurrentObject);
        }
    }

    void PlaceTile(Vector3Int Pos, Building Building)
    {
        if (gameManager.Money >= Building.Cost)
        {
            if (!CurrentTileMap.HasTile(Pos))
            {
                print(!CurrentTileMap.HasTile(Pos));

                if (!CheckRoadNeed(Building, Pos))
                {
                    Debug.Log("Needs an road");
                    return;
                }

                switch (Building.Type)
                {
                    case Building.Building_Type.Road:
                        BuildItem(Pos, Building);
                        break;
                    case Building.Building_Type.Housing:
                        BuildItem(Pos, Building);
                        AddPopulation(Building);
                        gameManager.MoneyGeneration += Building.MoneyProduceAmount;
                        break;
                    case Building.Building_Type.Commercial:
                        //yay shops
                        BuildItem(Pos, Building);
                        gameManager.MoneyGeneration += Building.MoneyProduceAmount;
                        gameManager.CommercialAmount += 1;
                        break;
                    case Building.Building_Type.Park:
                        //Yay happy
                        BuildItem(Pos, Building);
                        break;
                    case Building.Building_Type.Generator:
                        //if (Building.Building_Name == "Magic_Generator") { if (!CheckGroundType(Pos)) { break; } }
                        BuildItem(Pos, Building);
                        gameManager.MoneyGeneration += Building.MoneyProduceAmount;
                        break;
                }

                if (ProduceItem(Building))
                {
                    Debug.Log("Doesn't produce");
                }

            }
        }
    }

    void BuildItem(Vector3Int Pos, Building Building)
    {
        Pos.z = 0;
        Pos = new Vector3Int(Pos.x, Pos.y, Pos.z);
        CurrentTileMap.SetTile(Pos, CurrentTile);
        PlacedBuildings[Pos] = Building;
        gameManager.Money -= Building.Cost;
        gameManager.Research_Points += Building.ResearchPointsAdded;
        gameManager.UpKeepCosts += Building.UpKeepCost;
        gameManager.Usage_Elec += Building.Need_Electricity;
        gameManager.Usage_Magic += Building.Need_Magic;
        gameManager.Usage_Water += Building.Need_Water;
    }

    //INCL: Bulldose , Clearcurrentitle, Setcurrentitle , Deletetile
    #region Deleting & Setting the Currentile

    void DeleteTile(Vector3Int Pos, Building Building)
    {
        Pos.z = 0;
        if (PlacedBuildings.ContainsKey(Pos))
        {
            switch (Building.Type)
            {
                case Building.Building_Type.Housing:
                    gameManager.Population -= Building.People_Amount;
                    gameManager.MoneyGeneration -= Building.MoneyProduceAmount;
                    break;
                case Building.Building_Type.Commercial:
                    gameManager.MoneyGeneration -= Building.MoneyProduceAmount;
                    break;
            }
            PlacedBuildings.Remove(Pos);
        }
        CurrentTileMap.SetTile(Pos, null);
    }

    public void SetCurrentTile(Building Building)
    {
        CurrentTile = Building.Building_Sprite;
        CurrentObject = Building;
    }

    public void ClearCurrentTile()
    {
        CurrentTile = null;
    }

    public void BulldoseBoolSet(bool Setter)
    {
        BulldoseEnable = Setter;
    }
    #endregion 

    //Specific functions for 
    #region Building Specifics
    private bool CheckRoadNeed(Building Building, Vector3Int Position)
    {
        if (!Building.Need_Road)
        {
            return true;
        }

        Vector3Int[] Directions = new Vector3Int[] {
            new Vector3Int(1,0,0),
            new Vector3Int(-1,0,0),
            new Vector3Int(0,1,0),
            new Vector3Int(0,-1,0)
        };

        foreach (Vector3Int Direction in Directions)
        {
            Vector3Int CheckPos = Position + Direction;
            if (PlacedBuildings.TryGetValue(CheckPos, out Building neighbor))
            {
                if (neighbor.Type == Building.Building_Type.Road)
                    return true;
            }
        }

        return false;
    }

    private bool ProduceItem(Building BuildingObject)
    {
        if (BuildingObject.Type != Building.Building_Type.Generator)
        {
            return true;
        }

        switch (BuildingObject.Producing)
        {
            case Building.Product_Produced.Water:
                gameManager.WaterGeneration += BuildingObject.Product_Produce_Amount;
                break;
            case Building.Product_Produced.Electricity:
                gameManager.ElectraGeneration += BuildingObject.Product_Produce_Amount;
                break;
            case Building.Product_Produced.Magic:
                gameManager.MagicGeneration += BuildingObject.Product_Produce_Amount;
                break;
        }
        return false;
    }

    private void AddPopulation(Building BuildingObject)
    {
        gameManager.Population += BuildingObject.People_Amount;
        switch (BuildingObject.Population_Sort)
        {
            case Building.Population_Type.Witch:
                gameManager.Population_Witch += BuildingObject.People_Amount;
                break;
            case Building.Population_Type.Vampire:
                gameManager.Population_Vampire += BuildingObject.People_Amount;
                break;
        }
    }

    //private bool CheckGroundType(Vector3Int Pos)
    //{

    //}

    #endregion
}
