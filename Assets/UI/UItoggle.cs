using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItoggle : MonoBehaviour
{
    public GameObject smallUI;
    public GameObject bigUI;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            smallUI.SetActive(false);
            bigUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            smallUI.SetActive(true);
            bigUI.SetActive(false);
        }
    }
}
