using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
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
    public int day;
    public int ordersCompleted;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
        resourceManager = ResourceManager.Instance;
        buildManager = BuildManager.Instance;
        AddStartItems();

    }

    public void OrderCountDown()
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
        if(ordersCompleted == 20)
        {
            gameManager.currentRoundType = GameManager.RoundType.Build;
            day++;
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
            
            if (!coroutineRunning)
            {
                timeSinceStart = 0;
                InvokeRepeating("OrderCountDown", 0, 1);
                coroutineRunning = true;
            }
        }
        if (gameManager.currentRoundType == GameManager.RoundType.Build)
        {
            if (coroutineRunning)
            {
                CancelInvoke("OrderCountDown");
                coroutineRunning = false;
                GenerateBluePrints();
            }
        }
    }
    public void AddStartItems()
    {
        if (buildManager.applianceList.Count >= 1)
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
    public void GenerateBluePrints()
    {
        for (int g = 0; g < 5; g++)
        {
            Transform pos = gameObject.transform;
            pos.position = new Vector3(gameObject.transform.position.x + g, gameObject.transform.position.y, gameObject.transform.position.z);
            int randInt = Random.Range(0, buildManager.placementSystem.database.objectsData.Count-1);
            Instantiate(buildManager.placementSystem.database.objectsData[randInt].Prefab, pos);
        }
    }
    public void AddItemAvailability(ApplianceClass appliance)
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
    public ClientOrder GenerateOrder()
    {
        int wantedID = Random.Range(0, availableitems.Count);
        ClientOrder order = new() { wantedItem = availableitems[wantedID], moneyReward = Random.Range(10, 50) };
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
                moneyText.GetComponent<Text>().text = ": " + buildManager.Money.ToString();
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
