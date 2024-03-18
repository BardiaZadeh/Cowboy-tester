using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = .125f;
    public Vector3 offset = new Vector3(-4, 3, -4);
    
    void Start()
    {
        
    }

    void FixedUpdate() {
        if (target != null) {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target);
        }
    }
}
