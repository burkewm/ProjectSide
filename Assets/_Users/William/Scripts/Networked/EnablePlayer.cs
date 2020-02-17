using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnablePlayer : NetworkBehaviour
{
    public Camera cam;
    public NetworkPlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() {

            cam.enabled = true;
            controller.enabled = true;
        
    }
    private void OnConnectedToServer() {

            cam.enabled = true;
            controller.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
