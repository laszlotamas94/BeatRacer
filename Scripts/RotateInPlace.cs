using System;
using UnityEngine;

public class RotateInPlace : MonoBehaviour
{
    [SerializeField] private float rotationSpeedX = 30.0f;
    [SerializeField] private float rotationSpeedY = 30.0f;
    void Update()
    {
        float rotationAmountX = rotationSpeedX * Time.deltaTime;
        float rotationAmountY = rotationSpeedY * Time.deltaTime;
     
        transform.Rotate(rotationAmountX, rotationAmountY, 0f);
    }
}
