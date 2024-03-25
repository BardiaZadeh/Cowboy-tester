using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public Image[] redHearts;
    public Image[] blackHearts;
    private bool canTakeDamage = true;
    public float damageCooldown = 1f; // player is immune for 1 second
    public GameOverManager gameOverManager;


    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
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
}
