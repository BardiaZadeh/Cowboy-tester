using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Buffer : MonoBehaviour
{
    public float boostMultiplier = 3f; // Multiplier to increase jump power
    public float boostDuration = 10f; // Duration of the boost in seconds
    private BallControls ballControls; // Reference to the BallControls script
    public Material blueMaterial; // Material named Blue to apply during the boost
    private Material originalMaterial; // To store the original material
    private GameObject sphere; // To reference the Sphere object within Character

    private void Start()
    {
        GameObject ballPlayer = GameObject.FindGameObjectWithTag("Player");
        if (ballPlayer != null)
        {
            ballControls = ballPlayer.GetComponent<BallControls>();
            if (ballControls == null)
            {
                Debug.LogError("BallControls component not found on Player object.");
            }

            sphere = ballPlayer.transform.Find("Sphere").gameObject; // Changed path here
            if (sphere != null)
            {
                Renderer sphereRenderer = sphere.GetComponent<Renderer>();
                if (sphereRenderer != null)
                {
                    originalMaterial = sphereRenderer.material;
                }
                else
                {
                    Debug.LogError("Renderer component not found on Sphere object.");
                }
            }
            else
            {
                Debug.LogError("Sphere object not found. Check hierarchy and names.");
            }
        }
        else
        {
            Debug.LogError("Player object not found. Check if the Player tag is correctly assigned.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && ballControls != null && sphere != null)
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }

            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            StartCoroutine(JumpBoost());
        }
    }

    IEnumerator JumpBoost()
    {
        float originalJumpPower = ballControls.jumpPower;
        ballControls.jumpPower *= boostMultiplier;

        if (sphere != null)
        {
            Renderer sphereRenderer = sphere.GetComponent<Renderer>();
            if (sphereRenderer != null)
            {
                sphereRenderer.material = blueMaterial;
            }
        }

        yield return new WaitForSeconds(boostDuration);

        ballControls.jumpPower = originalJumpPower;

        
        if (sphere != null)
        {
            Renderer sphereRenderer = sphere.GetComponent<Renderer>();
            if (sphereRenderer != null)
            {
                sphereRenderer.material = originalMaterial;
            }
        }
    }
}
