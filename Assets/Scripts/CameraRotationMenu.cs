using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationMenu : MonoBehaviour
{
    [SerializeField] private float rotationAmplitude = 10.0f; 
    [SerializeField] private float rotationSpeed = 5.0f; 
    private float currentAngle;

    void Start()
    {
        currentAngle = transform.localEulerAngles.x;
    }

    void Update()
    {
        currentAngle += rotationSpeed * Time.deltaTime;
        float newAngle = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * rotationAmplitude;

        // Clamp the rotation to prevent exceeding limits
        newAngle = Mathf.Clamp(newAngle, -rotationAmplitude, rotationAmplitude);

        transform.localEulerAngles = new Vector3(newAngle, 0f, 0f);
    }
}
