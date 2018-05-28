using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlayerMovement : MonoBehaviour {

	public GameController GC;
	GameControl Controller;

	Animator anim;
	Rigidbody2D myRigidbody;
	public bool isGrounded = false;
	bool canDoubleJump = false;
	public float jumpPower;
	//private int jumpCounter;
	SwapColor ColorSwapper;

	void Awake(){
		Controller = FindObjectOfType<GameControl> ();
		ColorSwapper = GetComponent<SwapColor>();
	}

	// Use this for initialization
	void Start () {
		
		jumpPower = 5f;
		anim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D>();

		switch (Controller.Data.sex) 													
		{
		case 'M':
			anim.SetBool ("isMale", true);
			break;
		case 'F':
			anim.SetBool ("isMale", false);
			break;
		}

		ColorSwapper.SwapHairColor(Controller.Data.Hair);
		ColorSwapper.SwapEyesColor(Controller.Data.Eyes);
		ColorSwapper.SwapBodyColor(Controller.Data.Skin);
		ColorSwapper.SwapShirtColor(Controller.Data.Shirt);
		ColorSwapper.SwapPantsColor(Controller.Data.Pants);
		ColorSwapper.SwapShoesColor(Controller.Data.Shoes);
	}

	void FixedUpdate () {
		if (!GC.isPaused && !GC.GameInMenu) {														//Jogo a correr (com if para saltar) 
			if (Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.Space)) {
				if (isGrounded) {
					myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, 0);
					myRigidbody.velocity = Vector2.up * (jumpPower * myRigidbody.gravityScale);
					canDoubleJump = true;
				} else {
					if (canDoubleJump) {
						myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, 0);
						myRigidbody.AddForce(new Vector2(0, jumpPower * 0.75f * myRigidbody.gravityScale),ForceMode2D.Impulse);
						canDoubleJump = false;
					}
				}
			}

		} else {
			//Jogo em Pausa 
			anim.SetBool ("isPaused", true);
		}
	}

	public void StartShooting(){
		anim.SetBool ("SpecialEvent", true);
	}

	public void StopShooting(){
		anim.SetBool ("SpecialEvent", false);
	}

	public void StartRunning(){
		anim.SetBool ("isPaused", false);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.tag == "Ground") {
			anim.SetBool ("Jumping", false);
			isGrounded = true;
		}
	}

	void OnCollisionStay2D(Collision2D other){
		if (other.collider.tag == "Ground") {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.collider.tag == "Ground") {
			anim.SetBool ("Jumping", true);
			isGrounded = false;
		}
	}
}
	