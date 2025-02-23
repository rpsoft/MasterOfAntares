using UnityEngine;
using System.Collections.Generic;

// --- ShipController.cs ---
public class ShipController : MonoBehaviour
{
    public int Initiative;
    public int MovementRange;
    public int Health;
    public int MaxHealth;
    public int Damage;
    public GameObject ProjectilePrefab;

    public bool IsAIControlled;
    
    private TurnManager turnManager;

    public void StartTurn(TurnManager manager)
{
    turnManager = manager;
    Debug.Log(name + "'s turn");

    // Show movement grid
    MovementGridManager gridManager = FindObjectOfType<MovementGridManager>();
    if (gridManager != null)
    {
        gridManager.ShowMovementGrid(this);
    }
}

    public void Move(Vector3 targetPosition)
    {
        if (Vector3.Distance(transform.position, targetPosition) <= MovementRange)
        {
            transform.position = targetPosition;
        }
        EndTurn();
    }

  public void FireAt(ShipController target)
{
    GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
    projectile.GetComponent<Projectile>().Initialize(target, Damage);
    EndTurn();
}


    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject); // Ship is destroyed
        }
    }

    public void EndTurn()
    {
        turnManager.EndTurn();
    }

    public void HighlightShip(bool highlight)
{
    Renderer renderer = GetComponent<Renderer>();

    if (highlight)
    {
        renderer.material.color = Color.yellow; // Change to highlight color
    }
    else
    {
        renderer.material.color = Color.white; // Change to default color
    }
}

}