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
        }

        void OnCollisionEnter(Collision other)
        {
            // Check for collision with enemies
            if (other.gameObject.CompareTag("RollingObject"))
            {
                PlayerHealth playerHealth = GetComponent<PlayerHealth>();
                if(playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                }
            }

            // Ground check logic remains as is
            if (other.contacts[0].normal.y > 0.5)
            {
                isGrounded = true;
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
                direction.y = 0; // Ensure projectile travels horizontally
                GameObject projectile = Instantiate(prefabToShoot, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                projectile.GetComponent<Rigidbody>().AddForce(direction.normalized * 1000);
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
