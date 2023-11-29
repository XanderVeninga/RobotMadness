using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastFurnaceController : ApplianceClass
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.resources.Count > 0 && !this.Working)
        {
            this.Working = true;
            //PressResources(itemHolder.transform.GetChild(0).GetComponent<Resource>());
        }
    }
}