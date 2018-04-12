using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour {


	public GameObject Window;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Window.SetActive (false);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		
		if (other.gameObject.name == "Player") 
		{
			if (!Window.activeSelf)
			{
				Window.SetActive(true);
				Time.timeScale = 0.0f;

			}
		
		}
	
	}
}
