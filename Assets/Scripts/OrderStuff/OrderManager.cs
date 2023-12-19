using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private ResourceManager resourceManager;
    private BuildManager buildManager;
    public List<ResourceData> availableitems = new();
    public List<ClientOrder> orders = new();
    [SerializeField] bool DebugOrder;
    private void Start()
    {
        resourceManager = ResourceManager.Instance;
        buildManager = BuildManager.Instance;
        AddStartItems();
    }
    public void AddStartItems()
    {
        foreach(ApplianceClass appliance in buildManager.applianceList)
        {
            List<CraftingRecipe> recipes = new(appliance.GetRecipes());
            for(int i = 0; i < recipes.Count; i++)
            {
                if (!availableitems.Contains(recipes[i].outputItem))
                {
                    availableitems.Add(recipes[i].outputItem);
                }                
            }
        }
    }
    public void AddItemAvailability(int itemID)
    {
        if (!availableitems.Contains(resourceManager.resources[itemID]))
        {
            availableitems.Add(resourceManager.resources[itemID]);
        }
    }
    public ClientOrder GenerateOrder()
    {
        int wantedID = Random.Range(0, availableitems.Count);
        ClientOrder order = new() {wantedItem = availableitems[wantedID]};
        Debug.Log(order.wantedItem);
        orders.Add(order);
        GameObject spawnedObject = Instantiate(order.wantedItem.prefab, gameObject.transform);
        spawnedObject.transform.localPosition = new Vector3(0,1,0);
        return order;
    }
    public void ClearOrder(PlayerController player)
    {
        Destroy(player.resourceHolder.GetComponentInChildren<Resource>().gameObject);
        Destroy(gameObject.GetComponentInChildren<Resource>().resourceObject);
    }
}
