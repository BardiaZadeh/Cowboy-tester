using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public string enemyTag = "RollingObject";
    void Start()
    {
        
    }
    
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag(enemyTag)) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
