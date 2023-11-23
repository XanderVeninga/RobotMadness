using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AplianceClass : MonoBehaviour
{
    public List<ResourceData> resources = new List<ResourceData>();
    bool working = false;
    public bool Working
    {
        get { return working; }
        set { working = value; }
    }
}
