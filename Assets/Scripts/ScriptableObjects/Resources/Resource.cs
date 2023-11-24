using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceData data;
    public GameObject resourceObject;

    private void Start()
    {
        resourceObject = gameObject;
    }
}
