using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressController : ApplianceClass
{
    public ApplianceClass resourceManager;
    // Start is called before the first frame update
    private void Start()
    {
        resourceManager = FindAnyObjectByType<ApplianceClass>();
        itemHolder = this.transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if (this.resources.Count > 0 && !this.Working)
        {
            this.Working = true;
            PressResources(itemHolder.transform.GetChild(0).GetComponent<Resource>());
        }
    }

    private void PressResources(Resource resource)
    {
        Debug.Log(resource.data.Id);
        switch (resource.data.Id)
        {
            case Steel_ID: //steel
                //play animation
                ChangeResource(resource, resourceManager.resources[SteelPlate_ID].GetComponent<Resource>());
                break;
            case SteelPlate_ID: //steel plate
                ChangeResource(resource, resourceManager.resources[9].GetComponent<Resource>());
                break;
        }        
    }
}
