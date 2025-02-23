using UnityEngine;
using System.Collections.Generic;
// --- DamageSystem.cs ---
public class DamageSystem : MonoBehaviour
{
   public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int effectiveDamage = Mathf.Max(baseDamage - targetArmor, 0); // Ensure minimum damage is 0
        return effectiveDamage;
    }

}