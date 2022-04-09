using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewHealthKitScriptableObject")]
public class MedicalItem : InventoryItem
{
    // medical item type
    [SerializeField] private HealthKitType healthKitType;

    // amount of health contains
    [SerializeField] private int HealthContains;

    public HealthKitType HealthKitType { get { return healthKitType; } }

    public int HealthAmount { get { return HealthContains; } } 

}