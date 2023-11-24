using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ApplianceClass: MonoBehaviour
{
    public List<GameObject> resources = new List<GameObject>();
    public GameObject itemHolder;
    bool working = false;
    public bool Working
    {
        get { return working; }
        set { working = value; }
    }

    private void Start()
    {
        try
        {
            if (gameObject.transform.childCount > 0)
            {
                itemHolder = gameObject.transform.GetChild(0).gameObject;
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    public void AddResource(ResourceData resource)
    {
        this.resources.Add(resource.prefab);

    }
    public void ChangeResource(Resource oldRes, Resource newRes)
    {
        Debug.Log(oldRes.name);
        Destroy(oldRes.resourceObject);
        resources.Remove(resources.ElementAt(0));
        GameObject newItem = Instantiate(newRes.data.prefab, this.itemHolder.transform, false);
        newItem.transform.localPosition = Vector3.zero;
        newItem.transform.localRotation = Quaternion.identity;
    }
}
