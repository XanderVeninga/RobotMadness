using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildInputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    public PlayerController _playerController;
    private Vector3 lastPosistion;
    [SerializeField] LayerMask placementMask;

    public event Action OnClicked, OnExit;

    public void Start()
    {
        BuildManager.Instance.inputManager = this;
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_playerController.buildChecker.position, _playerController.buildChecker.forward, 1000, placementMask))
            {
                OnClicked?.Invoke();
            }
        }
    }
    
    public Vector3 GetSelectedMapPosistion()
    {
        Debug.DrawRay(_playerController.buildChecker.position, _playerController.buildChecker.forward, Color.red, 10000);
        if (Physics.Raycast(_playerController.buildChecker.position, _playerController.buildChecker.forward, out RaycastHit target, 1000, placementMask))
        {
            lastPosistion = target.point;
        }
        lastPosistion = new Vector3(lastPosistion.x + 0.5f, lastPosistion.y, lastPosistion.z + 0.5f);
        return lastPosistion;
    }
}
