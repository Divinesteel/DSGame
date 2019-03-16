using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public static InputManager IM;

    //Create Keycodes that will be associated with each of our commands.
    //These can be accessed by any other script in our game
    public KeyCode north { get; set; }
    public KeyCode south { get; set; }
    public KeyCode west { get; set; }
    public KeyCode east { get; set; }
    public KeyCode interact { get; set; }
    public KeyCode groundPet { get; set; }
    public KeyCode flyingPet { get; set; }
    public KeyCode callBackGroundPet { get; set; }
    public KeyCode callBackFlyingPet { get; set; }
    public KeyCode commandRange { get; set; }
    //public bool toggleCommand { get; set; }

    void Awake()
    {
        //Singleton pattern
        if (IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }
        else if (IM != this)
        {
            Destroy(gameObject);
        }

        /*Assign each keycode when the game starts.
		 * Loads data from PlayerPrefs so if a user quits the game, 
		 * their bindings are loaded next time. Default values
		 * are assigned to each Keycode via the second parameter
		 * of the GetString() function
		 */
        north = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("northKey", "W"));
        south = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("southKey", "S"));
        west = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("westKey", "A"));
        east = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("eastKey", "D"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("interactKey", "E"));
        groundPet = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("groundPetKey", "Mouse0"));
        flyingPet = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("flyingPetKey", "Mouse1"));
        callBackGroundPet = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("callBackGroundPetKey", "1"));
        callBackFlyingPet = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("callBackFlyingPetKey", "2"));
        commandRange = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("commandRangeKey", "Mouse2"));
        //toggleCommand = true;
    }
}
