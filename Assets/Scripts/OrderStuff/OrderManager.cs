using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    private GameManager gameManager;
    private ResourceManager resourceManager;
    private BuildManager buildManager;

    public GameObject moneyText;

    public List<ResourceData> availableitems = new();
    public List<ClientOrder> orders = new();
    [SerializeField] bool DebugOrder;
    public float timeSinceStart;
    public bool queueActive;
    public float orderWaitTime;
    public bool coroutineRunning;
    private void Start()
    {
        gameManager = GameManager.Instance;
        resourceManager = ResourceManager.Instance;
        buildManager = BuildManager.Instance;
        AddStartItems();
        
    }

    public IEnumerator OrderCountDown()
    {
        while(coroutineRunning)
        {
            timeSinceStart++;
            if (timeSinceStart % 10 == 0)
            {
                if (orders.Count < 10)
                {
                    GenerateOrder();
                }
                else
                {
                    if (!queueActive)
                    {
                        orderWaitTime = 60;
                        queueActive = true;
                    }

                }
            }
            if (queueActive)
            {
                orderWaitTime--;
                if (orderWaitTime < 0)
                {
                    gameManager.GameOver();
                }
            }
            foreach (var order in orders)
            {
                order.timeLeft--;
                if (order.timeLeft <= 0)
                {
                    gameManager.GameOver();
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && gameManager.currentRoundType == GameManager.RoundType.Build)
        {
            gameManager.currentRoundType = GameManager.RoundType.Play;
        }
        if (gameManager.currentRoundType == GameManager.RoundType.Play)
        {
            timeSinceStart = 0;
            if(!coroutineRunning)
            {
                StartCoroutine(OrderCountDown());
                coroutineRunning = true;
            }
        }
        if(gameManager.currentRoundType == GameManager.RoundType.Play)
        {
            if(coroutineRunning)
            {
                StopCoroutine(OrderCountDown());
                coroutineRunning = false;
            }
        }
    }
    public void AddStartItems()
    {
        if(buildManager.applianceList.Count >= 1)
        {
            foreach (ApplianceClass appliance in buildManager.applianceList)
            {
                List<CraftingRecipe> recipes = new(appliance.GetRecipes());
                for (int i = 0; i < recipes.Count; i++)
                {
                    if (!availableitems.Contains(recipes[i].outputItem))
                    {
                        availableitems.Add(recipes[i].outputItem);
                    }
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
        ClientOrder order = new() { wantedItem = availableitems[wantedID], moneyReward = Random.Range(10,50)};
        Debug.Log(order.wantedItem);
        orders.Add(order);
        GameObject spawnedObject = Instantiate(order.wantedItem.prefab, gameObject.transform);
        spawnedObject.transform.localPosition = new Vector3(0, 1, 0);
        return order;
    }

    public bool CheckOrder(Resource resource)
    {
        for (int i = 0; i < orders.Count; i++)
        {
            if (orders[i].wantedItem == resource.data)
            {
                RemoveOrder(orders[i]);
                buildManager.Money += orders[i].moneyReward;
                moneyText.GetComponent<Text>().text = $": {buildManager.Money}";
                return true;
            }
        }
        return false;
    }
    public void RemoveOrder(ClientOrder order)
    {
        orders.Remove(order);
    }
}
