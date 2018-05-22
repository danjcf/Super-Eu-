using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public float spriteBlinkingTimer = 0.0f;
	public float spriteBlinkingMiniDuration = 0.1f;
	public float spriteBlinkingTotalTimer = 0.0f;
	public float spriteBlinkingTotalDuration = 1.0f;
	public bool startBlinking = false;

	private PowerPlayerMovement player;
	private GameController GC;

	void Awake(){
		player = FindObjectOfType<PowerPlayerMovement> ();
		GC = FindObjectOfType<GameController> ();
	}

	void Update()
	{ 
		if(startBlinking == true)
		{ 
			SpriteBlinkingEffect();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && this.gameObject.name.Contains ("WarningStand")) {
			startBlinking = true;
			BoxCollider2D col = this.gameObject.GetComponent<BoxCollider2D> ();
			col.enabled = false;
			GC.StaminaValue -= 2f;
		} else {
			if (other.tag == "Player") { 
				startBlinking = true;
				GC.StaminaValue -= 2f;
			}	
		}


	}


	private void SpriteBlinkingEffect()				//Faz o hit effect ao jogador por tocar no obstáculo
	{
		spriteBlinkingTotalTimer += Time.deltaTime;
		if(spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
		{
			startBlinking = false;
			spriteBlinkingTotalTimer = 0.0f;
			player.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			return;
		}

		spriteBlinkingTimer += Time.deltaTime;
		if(spriteBlinkingTimer >= spriteBlinkingMiniDuration)
		{
			spriteBlinkingTimer = 0.0f;
			if (player.gameObject.GetComponent<SpriteRenderer> ().enabled == true) {
				player.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			} else {
				player.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			}
		}
	}
}
