using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressController : ApplianceClass
{
    private void Update()
    {
        if (this.resources.Count > 0 && !this.Working)
        {
            this.Working = true;
            PressResources(this.resources[0].GetComponent<Resource>());
        }
    }

    private void PressResources(Resource resource)
    {
        Debug.Log(resource.data.Id);
        switch (resource.data.Id)
        {
            case Steel_ID: //steel
                //play animation
                ChangeResource(resource, resourceManager.resources[SteelPlate_ID]);
                break;
            case SteelPlate_ID: //steel plate
                ChangeResource(resource, resourceManager.resources[SteelPipe_ID]);
                break;
        }        
    }
}
