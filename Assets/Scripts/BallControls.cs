using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] public float jumpPower = 5f;
    [SerializeField] private Rigidbody rb;
    //[SerializeField] private float projectileSpeed;
    //[SerializeField] private Projectile projectilePrefab;
    public GameOverManager gameOverManager;
    public GameObject prefabToShoot;
    public int maxAmmo = 5;
    public int currentAmmo;
    private bool isGrounded;
    private AudioSource bullet;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bullet = GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1")) && currentAmmo > 0)
        {
            if (bullet != null)
            {
                bullet.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource (bullet) not found.");
            }
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
        }
    }


    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed;
        rb.AddForce(movement);

        // Instead of calling IsGrounded() every frame in FixedUpdate, we'll rely on collision detection
    }

    bool IsGrounded()
    {
      // Raycast down to check for ground
        Ray groundRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        // Adjust the distance based on your ball size
        float distance = 1.5f;

        if (Physics.Raycast(groundRay, out hit, distance))
        {
            return hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground");
        }
        return false;
    }

    void OnCollisionEnter(Collision other)
    {
        //  "RollingObject" should be correctly tagged on enemy objects.
        if (other.gameObject.CompareTag("RollingObject"))
        {
            gameOverManager.ShowGameOver();
        }
        // Enable jumping again when colliding with the ground
        if (other.contacts[0].normal.y > 0.5)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0) return;

        currentAmmo--;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0; // Ensures that the projectile travels horizontally
            GameObject projectile = Instantiate(prefabToShoot, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddForce(direction.normalized * 1000); // Increase force for faster speed
        }
    }

    void IncreaseAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }
}
