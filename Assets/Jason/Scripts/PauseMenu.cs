using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    Transform menuPanel;
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject titleMenuUI;
    public GameObject titleCamera;
    public GameObject SettingsMenuUI;
    public GameObject HUDUI;

    Event keyEvent;
    Button BindingButton;
    Text buttonText;
    KeyCode newKey;

    public bool waitingForKey;
    public bool keyPressed ;

    void Start()
    {
        //buttonText = BindingButton.GetComponent<Text>();
        //Assign menuPanel to the Panel object in our Canvas
        //Make sure it's not active when the game starts

        HUDUI.GetComponent<Canvas>().enabled = false;
        menuPanel = transform.Find("Settings Buttons");
        //menuPanel.gameObject.SetActive(false);
        waitingForKey = false;
        keyPressed = false;

        /*iterate through each child of the panel and check
		 * the names of each one. Each if statement will
		 * set each button's text component to display
		 * the name of the key that is associated
		 * with each command. Example: the ForwardKey
		 * button will display "W" in the middle of it
		 */
        for (int i = 0; i < menuPanel.childCount; i++)
        {
            if (menuPanel.GetChild(i).name == "Move North Button")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.north.ToString();
            else if (menuPanel.GetChild(i).name == "Move South Button")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.south.ToString();
            else if (menuPanel.GetChild(i).name == "Move West Button")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.west.ToString();
            else if (menuPanel.GetChild(i).name == "Move East Button")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.east.ToString();
            else if (menuPanel.GetChild(i).name == "Interact Button")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.interact.ToString();
            else if (menuPanel.GetChild(i).name == "Order Tiger Button")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.interact.ToString();
            else if (menuPanel.GetChild(i).name == "Order Bird Button")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.interact.ToString();
            else if (menuPanel.GetChild(i).name == "Command Range Button")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.interact.ToString();
            else if (menuPanel.GetChild(i).name == "Command Range Toggle Button")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.interact.ToString();
        }
    }

    // Update is called once per frame
    void Update () {
		
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
	}

    void Pause()
    {
        pauseMenuUI.GetComponent<Canvas>().enabled = true;
        HUDUI.GetComponent<Canvas>().enabled = false;
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        HUDUI.GetComponent<Canvas>().enabled = true;
        titleCamera.SetActive(false);
        pauseMenuUI.GetComponent<Canvas>().enabled = false;
        titleMenuUI.GetComponent<Canvas>().enabled = false;
        //pauseMenuUI.SetActive(false);
        //SettingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    #region Settings

    public void Settings()
    {
        if (SettingsMenuUI.activeSelf == false)
        {
            SettingsMenuUI.SetActive(true);
        }
        else
        {
            SettingsMenuUI.SetActive(false);
        }
    }

    void OnGUI()
    {
        /*keyEvent dictates what key our user presses
		 * bt using Event.current to detect the current
		 * event
		 */
        keyEvent = Event.current;

        //Executes if a button gets pressed and
        //the user presses a key
        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode; //Assigns newKey to the key user presses
            waitingForKey = false;

            Debug.Log("Pressed " + newKey);

            keyPressed = true;
        }
        else if (keyEvent.isMouse && waitingForKey)
        {
            keyPressed = true;
            if (keyEvent.button == 0 && keyEvent.isMouse)
            {
                newKey = KeyCode.Mouse0;
                Debug.Log("Pressed Mouse0");
            }
            else if (keyEvent.button == 1)
            {
                newKey = KeyCode.Mouse1;
                Debug.Log("Pressed Mouse1");
            }
            else if (keyEvent.button == 2)
            {
                newKey = KeyCode.Mouse2;
                Debug.Log("Pressed Mouse2");
            }
            else if (keyEvent.button > 2)
            {
                Debug.Log("Another button in the mouse clicked");
            }
            waitingForKey = false;
        }
    }

    /*Buttons cannot call on Coroutines via OnClick().
	 * Instead, we have it call StartAssignment, which will
	 * call a coroutine in this script instead, only if we
	 * are not already waiting for a key to be pressed.
	 */
    public void StartAssignment(string keyName)
    {
        Debug.Log("Button Pressed.");
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    //Assigns buttonText to the text component of
    //the button that was pressed
    public void SendText(Text text)
    {
        //if (waitingForKey)
        //{
            buttonText = text;
        //}
    }

    IEnumerator WaitForKey()
    {
        int frames = 0;
        while (!keyEvent.isKey && !keyPressed/*|| frames < 600 || !keyEvent.isMouse*/)
        {
            //BindingButton.enabled = false;
            frames++;
            if (frames%15 == 0)
            {
                buttonText.text = " ";
            }

            if (frames %30 == 0)
            {
                buttonText.text = "_";
            }
            yield return null;
        }
        yield return null;
    }

    /*AssignKey takes a keyName as a parameter. The
	 * keyName is checked in a switch statement. Each
	 * case assigns the command that keyName represents
	 * to the new key that the user presses, which is grabbed
	 * in the OnGUI() function, above.
	 */
    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        Debug.Log("Waiting for key");

        yield return WaitForKey(); //Executes endlessly until user presses a key

        Debug.Log("Key pressed");

        switch (keyName)
        {
            case "Move North Button":
                InputManager.IM.north = newKey; //Set forward to new keycode
                buttonText.text = InputManager.IM.north.ToString(); //Set button text to new key
                PlayerPrefs.SetString("northKey", InputManager.IM.north.ToString()); //save new key to PlayerPrefs
                break;
            case "Move South Button":
                InputManager.IM.south = newKey; //set backward to new keycode
                buttonText.text = InputManager.IM.south.ToString(); //set button text to new key
                PlayerPrefs.SetString("southKey", InputManager.IM.south.ToString()); //save new key to PlayerPrefs
                break;
            case "Move West Button":
                InputManager.IM.west = newKey; //set left to new keycode
                buttonText.text = InputManager.IM.west.ToString(); //set button text to new key
                PlayerPrefs.SetString("westKey", InputManager.IM.west.ToString()); //save new key to playerprefs
                break;
            case "Move East Button":
                InputManager.IM.east = newKey; //set right to new keycode
                buttonText.text = InputManager.IM.east.ToString(); //set button text to new key
                PlayerPrefs.SetString("eastKey", InputManager.IM.east.ToString()); //save new key to playerprefs
                break;
            case "Interact Button":
                InputManager.IM.interact = newKey; //set jump to new keycode
                buttonText.text = InputManager.IM.interact.ToString(); //set button text to new key
                PlayerPrefs.SetString("interactKey", InputManager.IM.interact.ToString()); //save new key to playerprefs
                break;
            case "Order Tiger Button":
                InputManager.IM.groundPet = newKey; //set jump to new keycode
                buttonText.text = InputManager.IM.groundPet.ToString(); //set button text to new key
                PlayerPrefs.SetString("groundPetKey", InputManager.IM.groundPet.ToString()); //save new key to playerprefs
                break;
            case "Order Bird Button":
                InputManager.IM.flyingPet = newKey; //set jump to new keycode
                buttonText.text = InputManager.IM.flyingPet.ToString(); //set button text to new key
                PlayerPrefs.SetString("flyingPetKey", InputManager.IM.flyingPet.ToString()); //save new key to playerprefs
                break;
            case "Command Range Button":
                InputManager.IM.commandRange = newKey; //set jump to new keycode
                buttonText.text = InputManager.IM.commandRange.ToString(); //set button text to new key
                PlayerPrefs.SetString("commandRangeKey", InputManager.IM.commandRange.ToString()); //save new key to playerprefs
                break;
        }

        keyPressed = false;
        yield return null;
    }

    public void Toggle()
    {
        InputManager.IM.toggleCommand = !InputManager.IM.toggleCommand;
        if (InputManager.IM.toggleCommand)
        {
            buttonText.text = "Toggle";
        }
        else buttonText.text = "Hold";
        PlayerPrefs.SetString("commandRangeKey", InputManager.IM.commandRange.ToString()); //save new key to playerprefs
    }
    #endregion

    public void QuitGame()
    {
        titleCamera.SetActive(true);
        pauseMenuUI.GetComponent<Canvas>().enabled = false;
        titleMenuUI.GetComponent<Canvas>().enabled = true;
        //Application.Quit();
    }
}
