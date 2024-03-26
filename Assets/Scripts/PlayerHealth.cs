using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public Image[] redHearts;
    public Image[] blackHearts;
    private bool canTakeDamage = true;
    public float damageCooldown = 1f; // Player is immune for a certain period after taking damage
    public GameOverManager gameOverManager;
    private Vector3 startPosition; // Start position to reset player if they fall
    public AudioClip respawnSound; // Assign a respawn sound clip in the Inspector
    private AudioSource audioSource; // AudioSource component attached to the player

    void Start()
    {
        startPosition = transform.position; // Sets the start position to the player's initial position
        currentHealth = maxHealth;
        UpdateHearts();
        audioSource = GetComponent<AudioSource>(); // Ensure there's an AudioSource component attached
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHearts();
            canTakeDamage = false;
            Invoke(nameof(ResetDamageCooldown), damageCooldown);

            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(ResetPositionAndBlink()); // Adjusted method name for clarity
            }
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < redHearts.Length; i++)
        {
            blackHearts[i].enabled = i >= currentHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        gameOverManager.ShowGameOver();
    }

    private void ResetDamageCooldown()
    {
        canTakeDamage = true;
    }

    private IEnumerator ResetPositionAndBlink()
    {
        transform.position = startPosition; // Reset to start position immediately
        GetComponent<Rigidbody>().velocity = Vector3.zero; // Reset velocity
        if (audioSource && respawnSound)
        {
            audioSource.PlayOneShot(respawnSound); // Play respawn sound effect
        }
        StartCoroutine(BlinkEffect(2f, 0.1f)); // Start blinking effect immediately
        yield return new WaitForSeconds(2f); // Wait for blinking to complete
        ResetDamageCooldown(); // Re-enable damage after blinking
    }

    IEnumerator BlinkEffect(float duration, float blinkTime)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (!renderer) renderer = GetComponentInChildren<Renderer>(); // Attempt to get Renderer

        float endTime = Time.time + duration;
        bool visible = true;

        while (Time.time < endTime)
        {
            visible = !visible;
            renderer.enabled = visible;
            yield return new WaitForSeconds(blinkTime);
        }

        renderer.enabled = true; // Ensure renderer is visible after blinking
    }
}
