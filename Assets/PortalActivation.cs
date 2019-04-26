using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalActivation : MonoBehaviour {

    public GameObject crystalWater;
    public GameObject crystalEarth;
    public GameObject crystalFire;
    public GameObject crystalWind;
    public GameObject magicCircle;
    public GameObject players;
    public GameObject white;
    private Image image;
    private float targetAlpha = 1f;
    public float FadeRate = 0.1f;
    public GameObject EndText;

    private void Start()
    {
        image = white.GetComponent<Image>();
        white.SetActive(false);
        magicCircle.SetActive(false);
        EndText.SetActive(false);
    }

    void Update () {
		if (crystalWater.activeSelf && crystalEarth.activeSelf && crystalFire.activeSelf && crystalWind.activeSelf && !magicCircle.activeSelf )
        {
            magicCircle.SetActive(true);
            GetComponent<Renderer>().material.color = new Color(2, 5, 10, 0.1f);
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (IsPlayer(other))
        {
            if (magicCircle.activeSelf)
            {
                white.SetActive(true);
                Color curColor = image.color;
                float alphaDiff = Mathf.Abs(curColor.a - targetAlpha);
                
                if (alphaDiff > 0.0001f)
                {
                    curColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRate * Time.deltaTime);
                    image.color = curColor;
                }
                if(curColor.a > 0.99f)
                {
                    EndText.SetActive(true);
                }
            }
        }
    }
        bool IsPlayer(Collider other)
    {
        try
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}
