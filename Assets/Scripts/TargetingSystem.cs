using UnityEngine;
using System.Collections.Generic;

// --- TargetingSystem.cs ---
public class TargetingSystem : MonoBehaviour
{
    public ShipController SelectTarget(List<ShipController> potentialTargets)
    {
        if (potentialTargets == null || potentialTargets.Count == 0) return null;

        // For simplicity, select the first target. Add UI or other logic for manual selection.
        return potentialTargets[0];
    }
}