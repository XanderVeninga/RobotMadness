using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class BlastFurnaceController : ApplianceClass
{
    public ApplianceClass resourceManager;
    // Start is called before the first frame update
    private void Start()
    {
        resourceManager = FindAnyObjectByType<ApplianceClass>();
        itemHolder = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.resources.Count > 0 && !this.Working)
        {
            this.Working = true;
            
        }
    }

    void SmeltResource(Resource resource)
    {
        if(this.resources.Contains(resource.resourceObject))
        {

        }
        Debug.Log(resource.data.Id);
        switch (resource.data.Id)
        {
            case CopperWire_ID: //copper
                //play animation
                ChangeResource(resource, resourceManager.resources[].GetComponent<Resource>());
                break;
            case 5: //steel plate
                ChangeResource(resource, resourceManager.resources[9].GetComponent<Resource>());
                break;
        }
    }
}