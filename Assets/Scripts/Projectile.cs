using UnityEngine;
using System.Collections.Generic;

// --- Projectile.cs ---
public class Projectile : MonoBehaviour
{
    private ShipController target;
    private int damage;

    public void Initialize(ShipController target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

    void Update()
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 10f * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            target.TakeDamage(damage);
            Destroy(gameObject); // Destroy projectile on impact
        }
    }
}
