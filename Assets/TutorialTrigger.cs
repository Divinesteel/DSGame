using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

    public GameObject TutorialCanvas;
    public GameObject UICanvas;

    private bool shownTutorial;

    // Use this for initialization
    void Start () {

        TutorialCanvas.SetActive(false);
        shownTutorial = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !shownTutorial)
        {
            Time.timeScale = 0f;
            TutorialCanvas.SetActive(true);
            UICanvas.GetComponent<Canvas>().enabled = false;
            shownTutorial = true;
        }
    }


    public void CloseTutorial()
    {
        TutorialCanvas.SetActive(false);
        UICanvas.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 1f;
    }
}
