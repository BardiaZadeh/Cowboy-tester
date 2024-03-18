using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform platformTransform;
    public float speed = 2.0f;
    public float distance = 15.0f;
    public bool isHit = false;
    private Vector3 initialPlatformPosition;
    void Start()
    {
        initialPlatformPosition = platformTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit) {
            TranslatePlatform();
        }
    }
    void OnTriggerEnter(Collider other) {
        if (!isHit) {
            isHit = true;
            Debug.Log("Hit");
        }    }
    void TranslatePlatform() {
        platformTransform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (platformTransform.position.z >= initialPlatformPosition.z + distance) {
            //platformTransform.position = initialPlatformPosition;
            isHit = false;
        }
    }
    
}
