using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    public BallControls ballControls;   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) {
        if (ballControls != null) {
            
        
            if (other.gameObject.CompareTag("Player")) {
                ballControls.currentAmmo += 2;
                Destroy(gameObject);
            }
        }
    }
}
