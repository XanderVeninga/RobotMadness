using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrintScript : MonoBehaviour
{
    [SerializeField] BluePrintData bluePrintData;
    BuildManager buildManager;
    PlacementSystem placementSystem;
    BuildInputManager inputManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
        placementSystem = buildManager.placementSystem;
        inputManager = buildManager.inputManager;
    }

    public void ActivateBluePrint()
    {
        placementSystem = buildManager.placementSystem;
        inputManager = buildManager.inputManager;
        if (bluePrintData.BlueprintCost <= buildManager.Money)
        {
            buildManager.Money = buildManager.Money - bluePrintData.BlueprintCost;
            placementSystem.StartPlacement(bluePrintData.ID);
            Destroy(gameObject);
        }
        
    }    
}
