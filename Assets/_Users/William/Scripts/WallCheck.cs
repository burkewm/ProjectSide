using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public PlayerController player;
    public NetworkPlayerController nPlayer;
    Coroutine routineRef;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        nPlayer = GetComponent<NetworkPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {

       
        if(this.gameObject.name == "WallCheck_Left" && other.CompareTag("Wall")) {

            if (player != null) {
                player.wallJumpLeft = true;
                player.canjump = false;
                player.jumpCount = player.multiJumps;
                StopAllCoroutines();
                Debug.Log("Hit Left");
            } else {
                nPlayer.wallJumpLeft = true;
                nPlayer.canjump = false;
                nPlayer.jumpCount = player.multiJumps;
                StopAllCoroutines();
                Debug.Log("Hit Left");
            }
           
        }
        if (this.gameObject.name == "WallCheck_Right" && other.CompareTag("Wall")) {

            if (player != null) {
                player.wallJumpRight = true;
                player.canjump = false;
                player.jumpCount = player.multiJumps;
                StopAllCoroutines();
                Debug.Log("Hit Right");
            } else {
                nPlayer.wallJumpRight = true;
                nPlayer.canjump = false;
                nPlayer.jumpCount = player.multiJumps;
                StopAllCoroutines();
                Debug.Log("Hit Right");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (this.gameObject.name == "WallCheck_Left" && other.CompareTag("Wall")) {
            if (player != null) {
                player.wallJumpLeft = false;
                player.canjump = true;
                StartCoroutine(JumpStartDelay());
                Debug.Log("Left Left");
            } else {
                nPlayer.wallJumpLeft = false;
                nPlayer.canjump = true;
                StartCoroutine(JumpStartDelay());
                Debug.Log("Left Left");
            }
        }
        if (this.gameObject.name == "WallCheck_Right" && other.CompareTag("Wall")) {
            if (player != null) {
                player.wallJumpRight = false;
                player.canjump = true;
                StartCoroutine(JumpStartDelay());
                Debug.Log("Left Right");
            } else {
                nPlayer.wallJumpRight = false;
                nPlayer.canjump = true;
                StartCoroutine(JumpStartDelay());
                Debug.Log("Left Right");
            }
        }
    }

    public IEnumerator JumpStartDelay() {
        yield return new WaitForSeconds(.5f);
        if (player != null) {
            player.jumpCount = 0;
        } else {
            nPlayer.jumpCount = 0;
        }
    }
   


}
