using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{
    // Placing system by https://www.youtube.com/watch?v=snUe2oa_iM0 
    [SerializeField] Tilemap CurrentTileMap;
    [SerializeField] TileBase CurrentTile;
    [SerializeField] Camera Camera;


    private void Update()
    {
        Vector3Int pos = CurrentTileMap.WorldToCell(Camera.ScreenToWorldPoint(Input.mousePosition));

        if (CurrentTile != null && Input.GetMouseButtonDown(0))
        {
            PlaceTile(pos);
        }
    }

    void PlaceTile(Vector3Int Pos)
    {
        Pos.z = 0;
        Pos = new Vector3Int(Pos.x, Pos.y, Pos.z);
        CurrentTileMap.SetTile(Pos, CurrentTile);
    }
}
