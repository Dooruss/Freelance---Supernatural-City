using Unity.VisualScripting;
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
    public bool BulldoseEnable;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        Vector3Int pos = CurrentTileMap.WorldToCell(Camera.ScreenToWorldPoint(Input.mousePosition));

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

                switch (Building.Type)
                {
                    case Building.Building_Type.Road:
                        BuildItem(Pos, Building);
                        break;
                    case Building.Building_Type.Housing:
                        BuildItem(Pos, Building);
                        gameManager.Population += Building.People_Amount;
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
        gameManager.Money -= Building.Cost;
    }

    //INCL: Bulldose , Clearcurrentitle, Setcurrentitle , Deletetile
    #region Deleting & Setting the Currentile

    void DeleteTile(Vector3Int Pos, Building Building)
    {
        Pos.z = 0;
        Pos = new Vector3Int(Pos.x, Pos.y, Pos.z);
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

    #region Building Specifics
    private bool CheckRoadNeed(Building Building)
    {
        if (Building.Need_Road)
        {
            //check if theres a road next to it
            return true;
        } 
        return false;
    }

    #endregion
}
