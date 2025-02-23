using UnityEngine;
using System.Collections.Generic;

// --- CombatManager.cs ---
public class CombatManager : MonoBehaviour
{
    public List<ShipController> playerShips;
    public List<ShipController> enemyShips;

    public void CheckForEndConditions()
    {
        if (playerShips.Count == 0)
        {
            Debug.Log("Defeat");
            // Trigger defeat UI
        }
        else if (enemyShips.Count == 0)
        {
            Debug.Log("Victory");
            // Trigger victory UI
        }
    }
}