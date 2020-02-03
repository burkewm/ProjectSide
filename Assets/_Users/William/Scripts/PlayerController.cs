using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerControls controls;
    Vector2 movement;

    //Movement Speed
    public float movePower;
    private float storedMovePower;

    //Calculate Jumps
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

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement = Vector2.zero;

        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Jump.performed += ctx => WallJump();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        storedMovePower = movePower;
        //Null out checks

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Jump()
    {
        if (jumpCount < multiJumps && canjump)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            var explosionForce = new Vector2(0, jumpForce);
            var transform = new Vector2(this.transform.position.x, this.transform.position.y);
            this.GetComponent<Rigidbody2D>().AddForceAtPosition(explosionForce, transform);
            jumpCount++;
        }
    }

    public void Movement()
    {
        Vector2 m = new Vector2(movement.x, 0) * movePower * Time.deltaTime;
        transform.Translate(m, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Ground" && jumpCount <= multiJumps && jumpCount > 0)
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
}
