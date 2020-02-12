using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ApplicationSettings : MonoBehaviour
{
    public TextMeshProUGUI targetFrameRate;
    public TextMeshProUGUI antiAliasing; 
    

    public Toggle isVsync;
    int vsyncNum;
    public Toggle isFullScreen;
    int fullScreenNum;
    public Resolution[] resolutions;
    public TMP_Dropdown resDropDown;
    int resX;
    int resY;


    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
        QualitySettings.antiAliasing = 16;
        //Set Quality of Textures, 0 = full 1= half;
        QualitySettings.masterTextureLimit = 1;
        //Screen.SetResolution(1920, 1080, isFullScreen, int.Parse(targetFrameRate.ToString()));
        PopulateDropdown();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplySettings() {
        //Apply all the settings that have changed (make a bool for if changed too so we don't go through EVERY option)
        if (targetFrameRate.text.ToString() == "OFF") {
            Application.targetFrameRate = -1;
        } else {
            Application.targetFrameRate = int.Parse(targetFrameRate.text.ToString());
        }
        if (isFullScreen.isOn) {
            Screen.fullScreen = true;
            Screen.SetResolution(resolutions[resDropDown.value].width, resolutions[resDropDown.value].height, true);
        } else {
            Screen.fullScreen = false;
            Screen.SetResolution(resolutions[resDropDown.value].width, resolutions[resDropDown.value].height, false);
        }
        if (isVsync.isOn) {
            QualitySettings.vSyncCount = 1;
            Debug.Log("Vsync is on");
        } else {
            QualitySettings.vSyncCount = 0;
            Debug.Log("Vsync is off");
        }
        if (antiAliasing.text.ToString() == "OFF") {
            QualitySettings.antiAliasing = 0;
        } else 
        {
            QualitySettings.antiAliasing = int.Parse(antiAliasing.text.ToString()); 
        }
    }

    public void SaveSettings() {
        //PlayerPrefs.SetInt("ResX", resolutions[resDropDown.value].width);
        //PlayerPrefs.SetInt("ResY", resolutions[resDropDown.value].height);
        if (targetFrameRate.text.ToString() == "OFF") {
            PlayerPrefs.SetInt("FrameRate", -1);
        } else {
            PlayerPrefs.SetInt("FrameRate", int.Parse(targetFrameRate.text.ToString()));
        }
        CheckBoolsForSave();
        PlayerPrefs.SetInt("FullScreen", fullScreenNum);
        PlayerPrefs.SetInt("vsync", vsyncNum);
        PlayerPrefs.SetInt("AA", QualitySettings.antiAliasing);
    }

    public void Cancel() {
        //Backs them out of the menu and resets settings values to their current saved status
    }

    public void CancelMenu() {
        //Bring up cancel menu to confim they really want to back out. 
    }

    public void PopulateDropdown() {

        resolutions = Screen.resolutions.Select(res => new Resolution { width = res.width, height = res.height }).Distinct().ToArray();
       // resDropDown.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[resDropDown.value].width, resolutions[resDropDown.value].height, false); });
        for (int i = 0; i < resolutions.Length; i++) {
                resDropDown.options[i].text = ResToString(resolutions[i]);
                resDropDown.value = i;
                resDropDown.options.Add(new TMP_Dropdown.OptionData(resDropDown.options[i].text));
            
        }
        resDropDown.options.RemoveAt(resDropDown.options.Count - 1);
    }
    string ResToString(Resolution res) {
        return res.width + " x " + res.height;
    }

    public void CheckBoolsForSave() {
        if (isVsync.isOn) {
            vsyncNum = 1;
        } else {
            vsyncNum = 0;
        }
        if (isFullScreen.isOn) {
            fullScreenNum = 1;
        } else {
            fullScreenNum = 0;
        }
    }

}
