using UnityEngine;
using System.Collections.Generic;

// --- PlayerManager.cs ---
public class PlayerManager : MonoBehaviour
{
    public List<ShipController> playerFleet;

    public void AddShip(ShipController newShip)
    {
        playerFleet.Add(newShip);
    }

    public void RemoveShip(ShipController ship)
    {
        playerFleet.Remove(ship);
    }
}