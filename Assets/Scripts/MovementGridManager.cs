// --- MovementGridManager.cs ---
using UnityEngine;
using System.Collections.Generic;

public class MovementGridManager : MonoBehaviour
{
    public GameObject gridTilePrefab; // Prefab for the grid tile
    public List<GameObject> activeGridTiles = new List<GameObject>();
    public ShipController currentShip;

    public void ShowMovementGrid(ShipController ship)
    {
        ClearGrid();

        currentShip = ship;
        int range = ship.MovementRange;

        Debug.Log("range: " + range);
        // Generate a grid around the ship's position
        Vector3 shipPosition = ship.transform.position;
        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                Vector3 tilePosition = shipPosition + new Vector3(x, y, 0.5f);
                if (Vector3.Distance(shipPosition, tilePosition) <= range)
                {
                    GameObject gridTile = Instantiate(gridTilePrefab, tilePosition, Quaternion.identity);
                    gridTile.GetComponent<GridTile>().Initialize(this, tilePosition);
                    activeGridTiles.Add(gridTile);
                }
            }
        }
    }

    public void OnTileSelected(Vector3 targetPosition)
    {
        if (currentShip != null)
        {
            currentShip.Move(targetPosition);
            ClearGrid();
        }
    }

    public void ClearGrid()
    {
        foreach (GameObject tile in activeGridTiles)
        {
            Destroy(tile);
        }
        activeGridTiles.Clear();
    }
}
