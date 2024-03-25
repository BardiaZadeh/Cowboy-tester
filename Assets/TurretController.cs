using UnityEngine;
using System.Collections;


public class TurretController : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public Transform gunMount;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 5f;
    [SerializeField] private float bulletSpeed = 20f;

    private bool playerDetected = false;
    private float fireTimer = 0f;

    void Update()
    {
        if (playerDetected)
        {
          Vector3 targetDirection = player.position - gunMount.position;
          Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

          // Apply rotation smoothly
          gunMount.rotation = Quaternion.Lerp(gunMount.rotation, targetRotation, Time.deltaTime * 5f);

            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                fireTimer = 0f;
                animator.SetTrigger("Fire");
                StartCoroutine(FireWithDelay(0.5f));
            }
        }
    }

    IEnumerator FireWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Fire();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
            animator.SetBool("PlayerDetected", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
            animator.SetBool("PlayerDetected", false);
            fireTimer = 0f;
            gunMount.localRotation = Quaternion.identity;
        }
    }

    private void Reset()
    {
        gunMount = transform.Find("GunMount") ?? gunMount;
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        Vector3 firingDirection = (player.position - firePoint.position).normalized;
        bulletRigidbody.velocity = firingDirection * bulletSpeed;
        Destroy(bullet, 10f);
    }
}
