// --- GridTile.cs ---
using UnityEngine;

public class GridTile : MonoBehaviour
{
    private MovementGridManager gridManager;
    private Vector3 position;

    public void Initialize(MovementGridManager manager, Vector3 pos)
    {
        gridManager = manager;
        position = pos;
    }

    private void OnMouseDown()
    {
        gridManager.OnTileSelected(position);
    }
}
