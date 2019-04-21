using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Light : MonoBehaviour {

	private bool isFlashLightOn = true;
	public float maxIntensity;
	public float duration;

    Light light;

	// Use this for initialization
	public IEnumerator Start () {

        light = GetComponent<Light>();

		while (true)
		{
            if(InputManager.IM.toggleCommand)
            {
                if (Input.GetKeyDown(InputManager.IM.commandRange) && !isFlashLightOn)
                {
                    yield return StartCoroutine(ActivateLightIntensity(light, maxIntensity, duration));
                    isFlashLightOn = true;
                }
                else if (Input.GetKeyDown(InputManager.IM.commandRange) && isFlashLightOn)
                {
                    yield return StartCoroutine(DeactivateLightIntensity(light, light.intensity, duration));
                    isFlashLightOn = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(InputManager.IM.commandRange) && !isFlashLightOn)
                {
                    yield return StartCoroutine(ActivateLightIntensity(light, maxIntensity, duration));
                    isFlashLightOn = true;
                }
                else if (!Input.GetKey(InputManager.IM.commandRange) && isFlashLightOn)
                {
                    yield return StartCoroutine(DeactivateLightIntensity(light, light.intensity, duration));
                    isFlashLightOn = false;
                }
            }
			yield return null;
		}
	}
	
    void Update()
    {
 
    }

	IEnumerator ActivateLightIntensity(Light light, float maxIntensity, float duration)
	{
		isFlashLightOn = true;

		float time = 0f;

		while (time < 1f)
		{
			light.intensity = maxIntensity * time;
			time += Time.deltaTime / duration;
			yield return null;
		}
	}

    IEnumerator DeactivateLightIntensity(Light light, float lightIntensity, float duration)
    {
        isFlashLightOn = true;

        float time = 0f;

        while (time < 1f)
        {
            light.intensity = light.intensity - lightIntensity * time * time;
            time += Time.deltaTime / duration;
            yield return null;
        }
    }


}
