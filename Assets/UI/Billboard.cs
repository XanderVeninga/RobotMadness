using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform camera;

    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(camera.position);
    }
}
