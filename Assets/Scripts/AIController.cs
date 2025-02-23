using UnityEngine;
using System.Collections.Generic;

// --- AIController.cs ---
public class AIController : MonoBehaviour
{
    public void TakeTurn(ShipController aiShip, List<ShipController> playerShips)
{
    if (playerShips.Count == 0) return;

    // Simple AI: Fire at the first available player ship
    ShipController target = playerShips[0];
    aiShip.FireAt(target);
}
}