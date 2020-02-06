using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour
{
    PlayerControls controls;
    Vector2 movement;
    Vector2 aimDirection;

    //Movement Speed
    [Header("Movement Settings")]
    public float movePower;
    private float storedMovePower;

    //Calculate Jumps
    [Header("Jump Settings")]
    public float jumpForce;
    public float wallJumpForce;
    public float wallJumpCoolDown;
    public int multiJumps;
    [SerializeField]
    public int jumpCount;
    public bool canjump;


    //Wall Jump Checks
    public bool wallJumpLeft = false;
    public bool wallJumpRight = false;

    //For Guns
    [Header("Gun Settings")]
    public float FireRate;
    public float lastFired;
    public float bulletForce;
    public bool isAuto;
    public GameObject projectile;


    //Input Checks
    bool isFiring = false;

    private void Awake() {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement = Vector2.zero;

        controls.Player.Look.performed += ctx => aimDirection = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => aimDirection = Vector2.zero;

        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Jump.performed += ctx => WallJump();

        controls.Player.Shoot.performed += FireAction;
        controls.Player.Shoot.performed += ctx => StartCoroutine(FireSemiAuto());
        //controls.Player.Shoot.performed += ctx => shootButton = false;
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }
    // Start is called before the first frame update
    void Start() {
        storedMovePower = movePower;
        //Null out checks

    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {
        Movement();
        Aiming();
        StartCoroutine(FireGunAuto());
    }

    public void Jump() {
        if (jumpCount < multiJumps && canjump) {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            var explosionForce = new Vector2(0, jumpForce);
            var transform = new Vector2(this.transform.position.x, this.transform.position.y) * Time.deltaTime;
            this.GetComponent<Rigidbody2D>().AddForceAtPosition(explosionForce, transform);
            jumpCount++;

        }
    }

    public void Movement() {
        Vector2 m = new Vector2(movement.x, 0) * movePower * Time.deltaTime;
        transform.Translate(m, Space.World);
    }

    public void Aiming() {
        Vector2 aim = new Vector2(aimDirection.x, aimDirection.y) * Time.deltaTime;
        //Debug.Log("currently Aiming At" + aimDirection);
    }

    public IEnumerator FireGunAuto() {

        if (aimDirection != new Vector2(0, 0) && isFiring && isAuto) {
            if (Time.time - lastFired > 1 / FireRate) {
                lastFired = Time.time;
                var bullet = Instantiate(projectile, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.AngleAxis(0, aimDirection));
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                bullet.GetComponent<Rigidbody2D>().AddForce(aimDirection * bulletForce * 10);
                Destroy(bullet, 5f);
                yield return new WaitForSeconds(.2f);
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), false);
            }
        }
    }

    public IEnumerator FireSemiAuto() {
        if (aimDirection != new Vector2(0, 0) && isFiring && !isAuto) {
            if (Time.time - lastFired > 1 / FireRate) {
                lastFired = Time.time;
                var bullet = Instantiate(projectile, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.AngleAxis(0, aimDirection));
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                bullet.GetComponent<Rigidbody2D>().AddForce(aimDirection * bulletForce * 10);
                Destroy(bullet, 5f);
                yield return new WaitForSeconds(.2f);
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), false);
            }
        }
    }

    public void LookCheck() {
    //check x value of move direction to flip sprite, also check aim direction as well!
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Ground" && jumpCount <= multiJumps && jumpCount >= 0)
        {
            Debug.Log("Hit Ground, Reseting Jumps");
            jumpCount = 0;
        }
    }

    public void WallJump() {
        if (wallJumpLeft) {
            movePower = 0;
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            var explosionForce = new Vector2(wallJumpForce, jumpForce);
            var transform = new Vector2(this.transform.position.x, this.transform.position.y);
            this.GetComponent<Rigidbody2D>().AddForceAtPosition(explosionForce, transform);
            StartCoroutine(RestartMovement());
        }
        if (wallJumpRight) {
            movePower = 0;
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            var explosionForce = new Vector2(-wallJumpForce, jumpForce);
            var transform = new Vector2(this.transform.position.x, this.transform.position.y);
            this.GetComponent<Rigidbody2D>().AddForceAtPosition(explosionForce, transform);
            StartCoroutine(RestartMovement());
        }
    }

    public IEnumerator RestartMovement() {
        yield return new WaitForSeconds(wallJumpCoolDown);
        movePower = storedMovePower;
    }

    public IEnumerator Testing() {
        yield return new WaitForSeconds(2f);
        Debug.Log("Action Not Canned");
    }

    void FireAction(InputAction.CallbackContext context) {
        var value = context.ReadValue<float>();
        isFiring = value >= .5f;
    }
}
