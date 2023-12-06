using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCrafterController : ApplianceClass
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
            CraftResource(this.resources[0].GetComponent<Resource>());
        }
    }

    public void CraftResource(Resource resource)
    {

    }
}
