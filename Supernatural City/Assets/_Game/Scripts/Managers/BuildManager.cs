using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class BuildManager : MonoBehaviour
{
    // Placing system by https://www.youtube.com/watch?v=snUe2oa_iM0 
    [SerializeField] Tilemap CurrentTileMap;
    [SerializeField] public TileBase CurrentTile;
    [SerializeField] Camera Camera;
    public bool BulldoseEnable;


    private void Update()
    {
        Vector3Int pos = CurrentTileMap.WorldToCell(Camera.ScreenToWorldPoint(Input.mousePosition));

        if (CurrentTile != null && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            PlaceTile(pos);
        }
        if (BulldoseEnable == true && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            DeleteTile(pos);
        }
    }

    void PlaceTile(Vector3Int Pos)
    {
        Pos.z = 0;
        Pos = new Vector3Int(Pos.x, Pos.y, Pos.z);
        CurrentTileMap.SetTile(Pos, CurrentTile);
    }

    void DeleteTile(Vector3Int Pos)
    {
        Pos.z = 0;
        Pos = new Vector3Int(Pos.x, Pos.y, Pos.z);
        CurrentTileMap.SetTile(Pos, null);
    }

    public void SetCurrentTile(TileBase Tile)
    {
        CurrentTile = Tile;
    }

    public void ClearCurrentTile()
    {
        CurrentTile = null;
    }

    public void BulldoseBoolSet(bool Setter)
    {
        BulldoseEnable = Setter;
    }
}
