using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float speed = 10f;
    public float boostMultiplier = 2f; // Speed multiplier when boosting
    public float rotationSpeed = 100f;

    void Update()
    {
        ProcessInput();
    }

    public void ProcessInput()
    {
        float currentSpeed = speed;

        // Boost when Left Shift is held
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= boostMultiplier;
        }

        float move = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;
        float rotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * move);
        transform.Rotate(Vector3.up * rotate);
    }
}