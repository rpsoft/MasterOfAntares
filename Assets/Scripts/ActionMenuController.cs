using UnityEngine;
using System.Collections.Generic;


// --- ActionMenuController.cs ---
using UnityEngine.UI;

public class ActionMenuController : MonoBehaviour
{
    public Button moveButton;
    public Button fireButton;

    private ShipController currentShip;

    public void ShowMenu(ShipController ship)
    {
        currentShip = ship;
        gameObject.SetActive(true);
    }

    public void OnMoveButtonPressed(Vector3 targetPosition)
    {
        currentShip.Move(targetPosition);
        gameObject.SetActive(false);
    }

    public void OnFireButtonPressed(ShipController target)
    {
        currentShip.FireAt(target);
        gameObject.SetActive(false);
    }
}