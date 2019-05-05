using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drag : MonoBehaviour {

    public DragAndDrop dragAndDropSystem;

    private Text text;
    private Image image;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (image != null)
        {
            image.transform.position = Input.mousePosition;
        }
	}

    public void SetDNDName()
    {
        dragAndDropSystem.SetDraggedItem(text.text);
        if (text != null && Input.GetKey(InputManager.IM.orderGroundPet))
        {
            //GetComponentInParent<Image>().sprite = ...;
            image = GetComponentInParent<Image>();
            image = Instantiate(image,this.gameObject.transform);
        }
    }

    public void RemoveDNDName()
    {
        dragAndDropSystem.DropItem();
        dragAndDropSystem.RemoveDraggedItem();
        Destroy(image.gameObject);
        image = null;
        //if (text != null)
        //{
        //    GetComponentInParent<Image>().sprite = sprite;
        //}
    }
}
