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
    [SerializeField] public TileBase CurrentTile;
    [SerializeField] public Building CurrentObject;
    [SerializeField] private Building RoadTile;
    [SerializeField] Camera Camera;
    [SerializeField] private Building[] AllHousing;
    public bool BulldoseEnable;
    private GameManager gameManager;
    Dictionary<Vector3Int, Building> PlacedBuildings = new Dictionary<Vector3Int, Building>();
    // Hi Dorus from a couple days for now incase u forgot how this works
    // Ty for unity discussions for this , So like It saves a Position (Vector3Int) and like an value on it (A building type)
    // So like to save stuff its PlacedBuildings[PositionHere] = Building; (0,0,0) = Road (For example)
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
                        gameManager.Population += Building.People_Amount;
                        gameManager.MoneyGeneration += Building.MoneyProduceAmount;
                        break;
                    case Building.Building_Type.Commercial:
                        //yay shops
                        BuildItem(Pos, Building);
                        break;
                    case Building.Building_Type.Park:
                        //Yay happy
                        BuildItem(Pos, Building);
                        break;
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
    }

    //INCL: Bulldose , Clearcurrentitle, Setcurrentitle , Deletetile
    #region Deleting & Setting the Currentile

    void DeleteTile(Vector3Int Pos, Building Building)
    {
        Pos.z = 0;
        CurrentTileMap.SetTile(Pos, null);
        PlacedBuildings.Remove(Pos);
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

    private int ProduceItem()
    {
        // Produce sutff
        return 0;
    }

    private void CheckGroundType()
    {

    }

    #endregion
}
