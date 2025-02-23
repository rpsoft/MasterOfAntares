using UnityEngine;
using System.Collections.Generic;

// --- TurnManager.cs ---
public class TurnManager : MonoBehaviour
{
    public List<ShipController> ships;
    private int currentTurnIndex = 0;

    void Start()
    {
        ships.Sort((a, b) => b.Initiative.CompareTo(a.Initiative)); // Higher initiative ships go first
        StartTurn();
    }
    public void StartTurn()
    {
        if (ships.Count == 0) return;

        ShipController currentShip = ships[currentTurnIndex];
        if (currentShip != null)
        {
            if (currentShip.IsAIControlled)
            {
                AIController aiController = FindObjectOfType<AIController>();
                aiController.TakeTurn(currentShip, FindObjectOfType<CombatManager>().playerShips);
            }
            else
            {
                currentShip.StartTurn(this); // For player ships, show the action menu
            }
        }
    }


   public void EndTurn()
    {
        FindObjectOfType<CombatManager>().CheckForEndConditions(); // Check if battle has ended

        currentTurnIndex = (currentTurnIndex + 1) % ships.Count;
        StartTurn();
    }

}
