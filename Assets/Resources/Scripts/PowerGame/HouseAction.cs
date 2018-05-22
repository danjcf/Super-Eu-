using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAction : MonoBehaviour {

	public Animator anim;
	public GameController GC;
	public PowerPlayerMovement player;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			GC.EventStarted = true;
		}
	}


}
