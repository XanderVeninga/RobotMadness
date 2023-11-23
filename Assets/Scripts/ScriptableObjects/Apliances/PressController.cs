using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressController : AplianceClass
{
    // Start is called before the first frame update
    private void Update()
    {
        if(this.resources.Count > 0)
        {
            this.Working = true;
            while (this.resources.Count > 0)
            {
                if(this.Working)
                {
                    PressResources(resources[0]);
                }
            }
        }
    }

    private void PressResources(ResourceData resource)
    {
        switch(resource.Id)
        {
            case 0:
                //make steel plate
                break;
            case 1:
                //error, copper not allowed
                break;

        }
    }
}
