using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PickupManager))]
public class PickupManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PickupManager pickupManager = (PickupManager) target;

        foreach (PickupManager.Pickup pickup in pickupManager.Pickups)
        {
            if(GUILayout.Button("Test" + pickup.Name))
                pickupManager.OnPickUp(0, pickup);
        }

    }
}
