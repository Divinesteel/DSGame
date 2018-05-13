using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveEntrance : MonoBehaviour {

    public GameObject caveTop;

    private MeshRenderer[] topBoldersRenderers;

	// Use this for initialization
	void Start ()
    {
        topBoldersRenderers = caveTop.GetComponentsInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            foreach (MeshRenderer rnd in topBoldersRenderers)
            {
            rnd.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }

            CameraController cc = Camera.main.gameObject.GetComponent<CameraController>();
            cc.LockLeft();
            cc.LockRight();
            cc.SetPanVertical(6);
        }    
    }
}
