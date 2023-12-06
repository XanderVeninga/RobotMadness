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
            SmeltResource(this.resources[0].GetComponent<Resource>());
        }
    }

    void SmeltResource(Resource resource)
    {

        Debug.Log(resource.data.Id);
        switch (resource.data.Id)
        {
            case CopperWire_ID: //copper
                                //play animation
                if (this.resources[2].GetComponent<Resource>().data.Id == Glass_ID)
                {
                    ChangeResource(resource, resourceManager.resources[LightBulb_ID].GetComponent<Resource>());
                    this.resources.RemoveAt(2);
                }
                break;
        }

    }
}