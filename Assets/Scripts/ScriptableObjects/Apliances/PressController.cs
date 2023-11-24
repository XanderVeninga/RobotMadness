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
            StartCoroutine(PressResources(itemHolder.transform.GetChild(0).GetComponent<Resource>()));
        }
    }

    private IEnumerator PressResources(Resource resource)
    {
        switch(resource.data.Id)
        {
            
            case 0: //steel
                //play animation
                ChangeResource(resource, resourceManager.resources[5].GetComponent<Resource>());
                break;
            case 1: //copper
                //error, copper not allowed
                break;

        }
        if(this.Working)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            StopCoroutine(nameof(PressResources));
        }
    }
}
