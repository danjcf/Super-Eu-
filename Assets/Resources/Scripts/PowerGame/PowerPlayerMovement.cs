using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlayerMovement : MonoBehaviour {

	public bool GameStart;
	public GameController GC;

	Animator anim;
	Rigidbody2D myRigidbody;
	public bool isGrounded = false;
	bool canDoubleJump = false;
	public float jumpPower;
	//private int jumpCounter;

	// Use this for initialization
	void Start () {
		jumpPower = 5f;
		GameStart = true;
		anim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D>();


		//Provisório
		anim.SetBool("isPaused",false);
	}

	void FixedUpdate () {
		if (GameStart && !GC.isPaused) {														//Jogo a correr (com if para saltar) 
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
	