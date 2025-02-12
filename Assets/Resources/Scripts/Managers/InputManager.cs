using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Cinemachine Cameras")]
    public CameraManager cameraManager;

    [Header("Control Objects")]
    public GameObject shipControls;
    public GameObject subControls;
    public GameObject ship;
    public GameObject submarine;

    private bool isSubmarineActive = false;
    private ShipController shipController;
    private SubmarineController subController;

    private Vector3 subOriginalPosition;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 2f;

    void Start()
    {
        shipController = ship.GetComponent<ShipController>();
        subController = submarine.GetComponent<SubmarineController>();
        subOriginalPosition = subController.gameObject.transform.position;

        ActivateShipView();
    }

    void Update()
    {
        HandleSwitching();
    }

    void HandleSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Press Tab to switch
        {
            ToggleView();
        }
    }

    void ToggleView()
    {
        if (isSubmarineActive)
        {
            ActivateShipView();
        }
        else
        {
            ActivateSubmarineView();
        }
    }

    void ActivateShipView()
    {
        cameraManager.shipVirtualCamera.Priority = 10;
        cameraManager.subVirtualCamera.Priority = 5;
        shipControls.SetActive(true);
        subControls.SetActive(false);

        shipController.enabled = true;
        subController.enabled = false;

        isSubmarineActive = false;

        subController.transform.position = new Vector3(shipController.transform.position.x, subOriginalPosition.y, shipController.transform.position.z);
        subController.transform.rotation = shipController.transform.rotation;
    }

    void ActivateSubmarineView()
    {
        subController.transform.position = new Vector3(shipController.transform.position.x, subOriginalPosition.y, shipController.transform.position.z);
        subController.transform.rotation = shipController.transform.rotation;

        cameraManager.shipVirtualCamera.Priority = 5;
        cameraManager.subVirtualCamera.Priority = 10;
        subControls.SetActive(true);

        shipController.enabled = false;
        subController.enabled = true;

        isSubmarineActive = true;
    }
}