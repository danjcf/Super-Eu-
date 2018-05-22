using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public int points;

	private GameController GC;

	void Awake(){
		GC = FindObjectOfType<GameController> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			//Collect points
			GC.GamePoints += points;
			GC.UpdatePoints (GC.GamePoints);
			gameObject.SetActive(false);
		}
	}
}
