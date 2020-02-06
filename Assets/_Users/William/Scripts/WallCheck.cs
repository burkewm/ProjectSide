using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    private PlayerController player;
    Coroutine routineRef;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {

       
        if(this.gameObject.name == "WallCheck_Left" && other.CompareTag("Wall")) {
            player.wallJumpLeft = true;
            player.canjump = false;
            player.jumpCount = player.multiJumps;
            StopAllCoroutines();
            Debug.Log("Hit Left");
        }
        if (this.gameObject.name == "WallCheck_Right" && other.CompareTag("Wall")) {
            player.wallJumpRight = true;
            player.canjump = false;
            player.jumpCount = player.multiJumps;
            StopAllCoroutines();
            Debug.Log("Hit Right");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (this.gameObject.name == "WallCheck_Left" && other.CompareTag("Wall")) {
            player.wallJumpLeft = false;
            player.canjump = true;
            StartCoroutine(JumpStartDelay());
            Debug.Log("Left Left");
        }
        if (this.gameObject.name == "WallCheck_Right" && other.CompareTag("Wall")) {
            player.wallJumpRight = false;
            player.canjump = true;
            StartCoroutine(JumpStartDelay());
            Debug.Log("Left Right");
        }
    }

    public IEnumerator JumpStartDelay() {
        yield return new WaitForSeconds(.5f);
        player.jumpCount = 0;
    }
   


}
