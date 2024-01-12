using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ClientOrder
{
    [SerializeField] public ResourceData wantedItem;
    [SerializeField] private float timeLeft;
    [SerializeField] public float timeCreated;
    
}
