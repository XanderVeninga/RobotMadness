using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameras;
    // Start is called before the first frame update
    public void SwitchCamera(CinemachineVirtualCamera camToActivate)
    {
        for(int i = 0; i < cameras.Count; i++)
        {
            if(camToActivate == cameras[i])
            {
                cameras[i].enabled = true;
            }
            else
            {
                cameras[i].enabled = false;
            }
        }
    }
}
