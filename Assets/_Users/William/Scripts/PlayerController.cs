using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerControls controls;
    Vector2 movement;

    //Calculate Jumps
    public float jumpForce;
    public int multiJumps;
    [SerializeField]
    int jumpCount;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement = Vector2.zero;

        controls.Player.Jump.performed += ctx => Jump();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Jump()
    {
        if (jumpCount < multiJumps)
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
        Vector2 m = new Vector2(movement.x, 0) * Time.deltaTime;
        transform.Translate(m, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Ground" && jumpCount == multiJumps)
        {
            Debug.Log("Hit Ground, Reseting Jumps");
            jumpCount = 0;
        }
    }
}
