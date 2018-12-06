using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetPetCursorOnHover : MonoBehaviour {

    public CursorType cursorPet;
    public bool Active;

    public enum CursorType
    {
        Feather, Paw
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        if (Active)
        {
            if (cursorPet == CursorType.Feather)
            {
                Cursor.SetCursor((Texture2D)AssetDatabase.LoadAssetAtPath("Assets/_Main Project/Icons/CursorFeather.png", typeof(Texture2D)), new Vector2(0, 0), CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor((Texture2D)AssetDatabase.LoadAssetAtPath("Assets/_Main Project/Icons/CursorPaw.png", typeof(Texture2D)), new Vector2(0, 0), CursorMode.Auto);
            }
        }
        
    }
        

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
    }


}
