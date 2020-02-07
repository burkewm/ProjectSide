using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    PlayerControls controls;
    public List<GameObject> playerBoxes;
    public MainMenu taskToPerform;
    public TextMeshProUGUI TextMesh;
    public GameObject FadeText;

    bool isControllerStarted;
    bool fadeIsDone = true;
    int curentIndex;


    private void Awake() {
        controls = new PlayerControls();
        controls.UI.DpadUP.performed += ctx => StartController();
        controls.UI.DpadUP.performed += ctx => MoveUpList();
        controls.UI.DpadDown.performed += ctx => StartController();
        controls.UI.DpadDown.performed += ctx => MoveDownList();
        controls.UI.Confirm.performed += ctx => StartController();
        controls.UI.Confirm.performed += ctx => ConfirmAction();
    }

    private void OnEnable() {
        controls.Enable();
    }
    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < playerBoxes.Count; i++) {
            playerBoxes[i].SetActive(false);
        }
        taskToPerform = GetComponentInParent<MainMenu>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void StartController() {
        if (!isControllerStarted) {
            playerBoxes[0].SetActive(true);
            isControllerStarted = true;
            curentIndex = 0;
        }
    }

    public void MoveDownList() {
        if (isControllerStarted) {
            playerBoxes[curentIndex].SetActive(false);
            if (curentIndex + 1 < playerBoxes.Count) {
                curentIndex++;
            } else {
                curentIndex = 0;
            }
            playerBoxes[curentIndex].SetActive(true);
        }
    }
    public void MoveUpList() {
        if (isControllerStarted && curentIndex != 0) {
            playerBoxes[curentIndex].SetActive(false);
            curentIndex--;
            playerBoxes[curentIndex].SetActive(true);
        }
    }

    public void ConfirmAction() {
        switch (curentIndex) {
            case 0:
                Debug.Log("Campaign Isn't In Yet");
                StartCoroutine(DisableTextFade("Campaign Is WIP"));
                break;
            case 1:
                Debug.Log("MultiPlayer Isn't In Yet");
                StartCoroutine(DisableTextFade("MultiPlayer Is WIP"));
                break;
            case 2:
                taskToPerform.ShowSettings();
                break;
            case 3:
                taskToPerform.Exit();
                break;
            case 4:
                Debug.Log("Credits are WIP");
                StartCoroutine(DisableTextFade("Credits are WIP"));
                break;
        }
    }

    public IEnumerator DisableTextFade(string thingToSay) {
        if (fadeIsDone) {
            fadeIsDone = false;
            FadeText.SetActive(true);
            FadeText.GetComponent<Animation>().Play();
            TextMesh.text = thingToSay;
            yield return new WaitForSeconds(3);
            FadeText.SetActive(false);
            fadeIsDone = true;
        }
    }
}
