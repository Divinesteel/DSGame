using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drag : MonoBehaviour {

    public DragAndDrop dragAndDropSystem;

    private Text text;
	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDNDName()
    {
        dragAndDropSystem.SetDraggedItem(text.text);
    }

    public void RemoveDNDName()
    {
        dragAndDropSystem.DropItem();
        dragAndDropSystem.RemoveDraggedItem();

    }
}
