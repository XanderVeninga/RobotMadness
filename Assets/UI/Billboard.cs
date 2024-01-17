using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cameraTransform;

    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(cameraTransform.position);
    }
}
