using UnityEngine;
using UnityEngine.EventSystems;

public class ShipSelectionManager : MonoBehaviour
{
    private ShipController selectedShip;
    public ActionMenuController actionMenuController;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f); // Draws a visible ray in the scene

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Raycast hit: " + hit.transform.name);
        }
        else
        {
            Debug.Log("Raycast did not hit any object.");
        }
    }
        // Check if left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Raycast hit: " + hit.transform.name); // Debugging info

                ShipController ship = hit.transform.GetComponent<ShipController>();

                if (ship != null)
                {
                    Debug.Log("Ship detected: " + ship.name); // Ensure ship detection is working
                    SelectShip(ship);
                }
                else
                {
                    Debug.Log("No ShipController found on the hit object.");
                }
            }
            else
            {
                Debug.Log("Raycast did not hit any object.");
            }
        }
    }

    public void SelectShip(ShipController ship)
    {
        if (ship != null)
        {
            selectedShip = ship;
            Debug.Log("Selected " + ship.name);

            // Show the action menu for the selected ship
            if (actionMenuController != null)
            {
                actionMenuController.ShowMenu(ship);
            }
            else
            {
                Debug.LogWarning("ActionMenuController is not assigned.");
            }
        } 
    }
}
