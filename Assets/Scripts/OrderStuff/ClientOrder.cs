using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ClientOrder
{
    [SerializeField] public ResourceData wantedItem;
    [SerializeField] public float timeLeft = 60;
    public int moneyReward;
}
