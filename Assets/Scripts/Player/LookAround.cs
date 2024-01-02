using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public Transform player;
    float rotationX = 0f;
    public float sensitivity = 200f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseXMove = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseYMove = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        player.Rotate(Vector3.up * mouseXMove);

        rotationX -= mouseYMove;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}