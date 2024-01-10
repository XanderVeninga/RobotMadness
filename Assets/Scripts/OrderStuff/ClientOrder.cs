using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClientOrder : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] public ResourceData wantedItem;
    [SerializeField] private int RemTime {get; set; }

    public void Start()
    {
        gameManager = GameManager.Instance;
        StartCoroutine(CountDown());
    }

    public IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        RemTime--;
        if(RemTime <= 0 )
        {
            gameManager.GameOver();
        }
    }
}
