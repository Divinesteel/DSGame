using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour {

    public GameObject menuPanel;
    public static bool gameIsPaused = false;
    public GameObject pauseMenuCanvas;
    public GameObject titleMenuCanvas;
    public GameObject TutorialCanvas;
    public GameObject titleCamera;
    public GameObject SettingsMenuCanvas;
    public GameObject HUDUI;
    public GameObject NoteCanvas;

    public Text TextPrompt;

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

        pauseMenuCanvas.SetActive(false);
        SettingsMenuCanvas.SetActive(false);
        HUDUI.GetComponent<Canvas>().enabled = false;
        //menuPanel = transform.Find("Settings Buttons");
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
        for (int i = 0; i < menuPanel.transform.childCount; i++)
        {
            if (menuPanel.transform.GetChild(i).name == "Move North Button")
                menuPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.north.ToString();
            else if (menuPanel.transform.GetChild(i).name == "Move South Button")
                menuPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.south.ToString();
            else if (menuPanel.transform.GetChild(i).name == "Move West Button")
                menuPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.west.ToString();
            else if (menuPanel.transform.GetChild(i).name == "Move East Button")
                menuPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.east.ToString();
            else if (menuPanel.transform.GetChild(i).name == "Interact Button")
            {
                menuPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.interact.ToString();
                TextPrompt.text = InputManager.IM.interact.ToString();
            }
            else if (menuPanel.transform.GetChild(i).name == "Order Tiger Button")
                menuPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.orderGroundPet.ToString();
            else if (menuPanel.transform.GetChild(i).name == "Order Bird Button")
                menuPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.orderFlyingPet.ToString();
            else if (menuPanel.transform.GetChild(i).name == "Call Back Tiger Button")
                menuPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.callBackGroundPet.ToString();
            else if (menuPanel.transform.GetChild(i).name == "Call Back Bird Button")
                menuPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.callBackFlyingPet.ToString();
            //else if (menuPanel.transform.GetChild(i).name == "Command Range Button")
            //    menuPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.commandRange.ToString();
            //else if (menuPanel.transform.GetChild(i).name == "Toggle Command Range Button")
            //{
            //    if (InputManager.IM.toggleCommand)
            //    {
            //        menuPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = "Toggle";
            //    }else menuPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = "Hold";
            //}
        }
    }


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        titleCamera.SetActive(true);
        pauseMenuCanvas.SetActive(false);
        SettingsMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update () {
		
        if (Input.GetKeyDown(KeyCode.T))
        {
            Tutorial();
        }

        if (!titleMenuCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
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
        NoteCanvas.GetComponent<Canvas>().enabled = false;
        gameIsPaused = true;
        Debug.Log("Paused.");
        pauseMenuCanvas.SetActive(true);
        HUDUI.GetComponent<Canvas>().enabled = false;
        //pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        NoteCanvas.GetComponent<Canvas>().enabled = false;
        gameIsPaused = false;
        HUDUI.GetComponent<Canvas>().enabled = true;
        titleCamera.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        titleMenuCanvas.SetActive(false);
        SettingsMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    #region Settings

    public void Settings()
    {
        if (SettingsMenuCanvas.activeSelf == false)
        {
            SettingsMenuCanvas.SetActive(true);
        }
        else
        {
            SettingsMenuCanvas.SetActive(false);
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
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        int frames = 0;
        while (!keyEvent.isKey && !keyPressed)
        {
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
                TextPrompt.text = InputManager.IM.interact.ToString();
                break;
            case "Order Tiger Button":
                InputManager.IM.orderGroundPet = newKey; //set jump to new keycode
                buttonText.text = InputManager.IM.orderGroundPet.ToString(); //set button text to new key
                PlayerPrefs.SetString("groundPetKey", InputManager.IM.orderGroundPet.ToString()); //save new key to playerprefs
                break;
            case "Order Bird Button":
                InputManager.IM.orderFlyingPet = newKey; //set jump to new keycode
                buttonText.text = InputManager.IM.orderFlyingPet.ToString(); //set button text to new key
                PlayerPrefs.SetString("flyingPetKey", InputManager.IM.orderFlyingPet.ToString()); //save new key to playerprefs
                break;
            case "Call Back Tiger Button":
                InputManager.IM.callBackGroundPet = newKey; //set jump to new keycode
                buttonText.text = InputManager.IM.callBackGroundPet.ToString(); //set button text to new key
                PlayerPrefs.SetString("groundPetKey", InputManager.IM.callBackGroundPet.ToString()); //save new key to playerprefs
                break;
            case "Call Back Bird Button":
                InputManager.IM.callBackFlyingPet = newKey; //set jump to new keycode
                buttonText.text = InputManager.IM.callBackFlyingPet.ToString(); //set button text to new key
                PlayerPrefs.SetString("flyingPetKey", InputManager.IM.callBackFlyingPet.ToString()); //save new key to playerprefs
                break;
            //case "Command Range Button":
            //    InputManager.IM.commandRange = newKey; //set jump to new keycode
            //    buttonText.text = InputManager.IM.commandRange.ToString(); //set button text to new key
            //    PlayerPrefs.SetString("commandRangeKey", InputManager.IM.commandRange.ToString()); //save new key to playerprefs
            //    break;
        }
        keyPressed = false;
        yield return null;
    }

    //public void Toggle()
    //{
    //    InputManager.IM.toggleCommand = !InputManager.IM.toggleCommand;
    //    if (InputManager.IM.toggleCommand)
    //    {
    //        buttonText.text = "Toggle";
    //    }
    //    else buttonText.text = "Hold";
    //}


    #endregion


    public void Tutorial()
    {
        if (TutorialCanvas.activeSelf == false)
        {
            TutorialCanvas.SetActive(true);
        }
        else
        {
            TutorialCanvas.SetActive(false);
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //titleCamera.SetActive(true);
        //pauseMenuCanvas.SetActive(false);
        //titleMenuCanvas.SetActive(true);
        //Time.timeScale = 1f;
        //Application.Quit();
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
