using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathTransition : MonoBehaviour {

    private Image image;
    private float targetAlpha = 1f;
    public float FadeRate = 1f;
    public GameObject DeathScreen;
    public GameObject black;

    int frames = 0;

    private void Start()
    {
        DeathScreen.SetActive(false);
        image = black.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () {

        if (GetComponent<PlayerController>().PlayerStatus == PlayerController.PlayerStat.Dead)
        {
            black.SetActive(true);
            Color curColor = image.color;
            //float alphaDiff = Mathf.Abs(curColor.a - targetAlpha);
            //Debug.Log(curColor.a);
            //Debug.Log(targetAlpha);
            curColor.a = 1;
            image.color = curColor;

            if (frames < 60)
            {
                frames++;
            }
            else
            {
                DeathScreen.SetActive(true);
            }
            //if (alphaDiff > 0.0001f)
            //{
            //    curColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRate * Time.deltaTime);
            //    image.color = curColor;
            //}
            //if (curColor.a > 0.99f)
            //{
            //}
        }

        if (GetComponent<PlayerController>().PlayerStatus == PlayerController.PlayerStat.Alive)
        {
            frames = 0;
            black.SetActive(false);
            Color curColor = image.color;
            curColor.a = 0;
            image.color = curColor;
            DeathScreen.SetActive(false);
        }
    }
}
