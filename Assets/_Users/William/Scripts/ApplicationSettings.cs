using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class ApplicationSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
        QualitySettings.antiAliasing = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplySettings() {
        //Apply all the settings that have changed (make a bool for if changed too so we don't go through EVERY option)
    }

    public void Cancel() {
        //Backs them out of the menu and resets settings values to their current saved status
    }

    public void CancelMenu() {
        //Bring up cancel menu to confim they really want to back out. 
    }
}
