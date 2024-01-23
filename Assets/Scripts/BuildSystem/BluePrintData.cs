using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprint")]
[Serializable]
public class BluePrintData : ScriptableObject
{
    [field: SerializeField] public int ID { get; private set;}
    [field: SerializeField] public int BlueprintCost { get; private set; }
}
