using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrintScript : MonoBehaviour
{
    [SerializeField] BluePrintData bluePrintData;
    PlacementSystem placementSystem;
    BuildInputManager inputManager;

    private void Start()
    {
        placementSystem = BuildManager.Instance.placementSystem;
        inputManager = BuildManager.Instance.inputManager;
    }

    public void ActivateBluePrint()
    {
        placementSystem.StartPlacement(bluePrintData.ID);
    }    
}
