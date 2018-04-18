using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayAnimationPillar : MonoBehaviour {

	public GameObject Player;
	private Animation anim;
	public GameObject Link_Obstacle;
	private NavMeshLink[] Scripts;

	private bool IsAnimPlayed;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation>();
		IsAnimPlayed = false;
		Scripts = Link_Obstacle.GetComponents<NavMeshLink>();
	}
	
	// Update is called once per frame
	void Update () {

		if (IsAnimPlayed)
		{
			if (anim.isPlaying)
			{
				return;
			}
			else
			{
				foreach (NavMeshLink script in Scripts)
				{
					script.enabled = true;
				}

			}
		}
		

	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.E) && !IsAnimPlayed)
			{
				Link_Obstacle.SetActive(true);
				anim.Play();
				IsAnimPlayed = true;
			}
		}

	}
}
