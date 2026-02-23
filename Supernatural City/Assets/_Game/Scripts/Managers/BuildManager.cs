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
            Pos.z = 0;
            Pos = new Vector3Int(Pos.x, Pos.y, Pos.z);
            CurrentTileMap.SetTile(Pos, CurrentTile);
            gameManager.Money -= Building.Cost;

            if (Building.Type == Building.Building_Type.Housing)
            {
                gameManager.Population += Building.People_Amount;
            }
        }
    }

    void DeleteTile(Vector3Int Pos, Building Building)
    {
        Pos.z = 0;
        Pos = new Vector3Int(Pos.x, Pos.y, Pos.z);
        CurrentTileMap.SetTile(Pos, null);
        gameManager.Money += Building.Cost / 2;
        if (Building.Type == Building.Building_Type.Housing)
        {
            gameManager.Population -= Building.People_Amount;
        }
    }

    public void SetCurrentTile(Building Building)
    {
        CurrentTile = Building.Building_Sprite;
        CurrentObject = Building;
    }

    public void ClearCurrentTile()
    {
        CurrentTile = null;
        CurrentObject = null;
    }

    public void BulldoseBoolSet(bool Setter)
    {
        BulldoseEnable = Setter;
    }
}
