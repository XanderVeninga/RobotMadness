using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private ResourceManager resourceManager;
    private BuildManager buildManager;
    [SerializeField] public List<ResourceData> availableitems = new List<ResourceData>();
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
            List<CraftingRecipe> recipes = new List<CraftingRecipe>(appliance.GetRecipes());
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
    private void Update()
    {
        if(DebugOrder)
        {
            GenerateOrder();
            DebugOrder = false;
        }
    }
    public ClientOrder GenerateOrder()
    {
        int wantedID = Random.Range(0, availableitems.Count);
        
        ClientOrder order = new ClientOrder();
        order.wantedItem = availableitems[wantedID];
        Debug.Log(order.wantedItem);
        return order;
    }
}
