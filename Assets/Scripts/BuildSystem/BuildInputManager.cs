using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildInputManager : MonoBehaviour
{
    [SerializeField] private Vector2 offset;
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
        if (Physics.Raycast(_playerController.buildChecker.position, _playerController.buildChecker.forward, out RaycastHit target, 1000, placementMask))
        {
            lastPosistion = target.point;
            lastPosistion = new Vector3(lastPosistion.x + offset.x, lastPosistion.y, lastPosistion.z + offset.y);
        }
        

        return lastPosistion;
    }
}
