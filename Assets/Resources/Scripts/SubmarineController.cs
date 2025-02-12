using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    public Camera mainCam;
    public float speed = 5f;
    public float verticalSpeed = 3f;
    public float rotationSpeed = 2f;
    public float mouseSensitivity = 2f;

    private void Start()
    {
        mainCam = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        ProcessInput();
        RotateWithMouse();
    }

    public void ProcessInput()
    {
        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(Vector3.forward * move);
    }

    public void RotateWithMouse()
    {
        transform.localRotation = mainCam.transform.localRotation;
    }

    public void UseGrapplingHook()
    {
        Debug.Log("Grappling Hook Activated!");
    }
}
