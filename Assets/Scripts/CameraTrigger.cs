using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private CameraController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            controller.SwitchCamera(gameObject.GetComponentInParent<CinemachineVirtualCamera>());
        }
    }

    
}
