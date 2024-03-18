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
  void Update() {
    if (Input.GetMouseButtonDown(0) && currentAmmo > 0) {
       bullet.Play();
       Shoot();
    }
  }
  void FixedUpdate()
  {

    float horizontalInput = Input.GetAxisRaw("Horizontal");
    float verticalInput = Input.GetAxisRaw("Vertical");

    Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed;


    rb.AddForce(movement);

    isGrounded = IsGrounded();
    Debug.Log(IsGrounded());

    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
      rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
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
      return true;
    }
    return false;
  }
  void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.CompareTag("RollingObject"))
    {
       gameOverManager.ShowGameOver();
    }
    else if (other.gameObject.CompareTag("NextLevelTotem")){
        SceneManager.LoadScene("LevelTwoScene");
    }
    else if (other.gameObject.CompareTag("ThirdLevelTotem")) {
        SceneManager.LoadScene("LevelThreeScene");
    }
    else if (other.gameObject.CompareTag("FourthLevelTotem")) {
        SceneManager.LoadScene("LevelFourScene");
    }
  }
  private void RespawnCharacter()
  {
    transform.position = new Vector3(0, -5.77f, -83f);
    rb.velocity = Vector3.zero;
    return;
  }
  void Shoot() {
    currentAmmo--;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit)) {
      Vector3 target = hit.point;
      Vector3 direction = target - transform.position;
      GameObject projectile = Instantiate(prefabToShoot, transform.position, Quaternion.identity);
      projectile.GetComponent<Rigidbody>().AddForce(direction * 10, ForceMode.Impulse);
      Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
      foreach (Collider c in GetComponentsInChildren<Collider>()) {
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), c);
      }
    }
  }
  void IncreaseAmmo(int amount) {
    currentAmmo += amount;
    if (currentAmmo > maxAmmo) {
      currentAmmo = maxAmmo;
    }
  }

}
