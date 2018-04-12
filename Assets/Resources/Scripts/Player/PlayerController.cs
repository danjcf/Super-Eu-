using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

	public float moveSpeed;

	private Animator anim;
	private Rigidbody2D myRigidbody;
	public bool isPaused;
	private bool playerMoving;
	private Vector2 lastMove;
	Sprite Hair1, Hair2, Hair3;
	SpriteRenderer PlayerSprite;
	SwapColor ColorSwapper;

    // Use this for initialization
    void Start()
	{
		isPaused = false;
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
		PlayerSprite = GetComponent<SpriteRenderer>();
		print("Player prefs cabelo estilo: " + PlayerPrefs.GetInt("HairStyle"));
		print("Player prefs cabelo cor: " + PlayerPrefs.GetInt("HairColor"));
		print("Player prefs olhos cor: " + PlayerPrefs.GetInt("EyesColor"));
		print("Player prefs corpo cor: " + PlayerPrefs.GetInt("BodyColor"));
		print("Player prefs camisola cor: " + PlayerPrefs.GetInt("ShirtColor"));
		print("Player prefs calças cor: " + PlayerPrefs.GetInt("PantsColor"));
		switch (PlayerPrefs.GetString ("Sexo")) 													
		{
		case "M":
			anim.SetBool ("isMale", true);
			print ("entrou gajo");
			Hair1 = Resources.Load<Sprite>("Sprites/Characters/Boy/Boy_hair1_spritesheet");
			Hair2 = Resources.Load<Sprite>("Sprites/Characters/Boy/Boy_hair2_spritesheet");
			Hair3 = Resources.Load<Sprite>("Sprites/Characters/Boy/Boy_hair3_spritesheet");
			break;
		case "F":
			anim.SetBool ("isMale", false);
			print("entrou gaja");
			Hair1 = Resources.Load<Sprite>("Sprites/Characters/Girl/Sprites/Girl_template");
			break;
		}


		anim.SetInteger ("HairStyle",PlayerPrefs.GetInt("HairStyle"));
		switch (PlayerPrefs.GetInt("HairStyle"))
		{
		case 1:
			PlayerSprite.sprite = Hair1;
			break;
		case 2:
			PlayerSprite.sprite = Hair2;
			break;
		case 3:
			PlayerSprite.sprite = Hair3;
			break;
		}
		SwapColor ColorSwapper = GetComponent<SwapColor>();
		ColorSwapper.SwapHairColor(PlayerPrefs.GetInt("HairColor"));
		ColorSwapper.SwapEyesColor(PlayerPrefs.GetInt("EyesColor"));
		ColorSwapper.SwapBodyColor(PlayerPrefs.GetInt("BodyColor"));
		ColorSwapper.SwapShirtColor(PlayerPrefs.GetInt("ShirtColor"));
		ColorSwapper.SwapPantsColor(PlayerPrefs.GetInt("PantsColor"));
		ColorSwapper.SwapShoesColor(PlayerPrefs.GetInt("ShoesColor"));
    }

	// Update is called once per frame
	void Update()
	{
        if (isPaused == false) {
			playerMoving = false;
			//#if UNITY_EDITOR || UNITY_EDITOR_WIN
			//PCController();	

			//#else
			AndroidController();

			//#endif

		}

	}

	void PCController(){
		if ((Input.GetAxisRaw ("Horizontal") > 0.5f || Input.GetAxisRaw ("Horizontal") < -0.5f)) {								//Esquerda e direita										
			//transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));

			myRigidbody.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal") * moveSpeed, myRigidbody.velocity.y);
			playerMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);

		}

		if (Input.GetAxisRaw ("Vertical") > 0.5f || Input.GetAxisRaw ("Vertical") < -0.5f) {									//Cima e baixo
			//transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));

			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, Input.GetAxisRaw ("Vertical") * moveSpeed);
			playerMoving = true;
			lastMove = new Vector2 (0f, Input.GetAxisRaw ("Vertical"));
		}


		if (Input.GetAxisRaw ("Horizontal") < 0.5f && Input.GetAxisRaw ("Horizontal") > -0.5f) {								//Esquerda e direita
			
			myRigidbody.velocity = new Vector2 (0f, myRigidbody.velocity.y);
		}
		if (Input.GetAxisRaw ("Vertical") < 0.5f && Input.GetAxisRaw ("Vertical") > -0.5f) {									//Cima e baixo
			
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, 0f);
		}

		anim.SetFloat ("MoveX",Input.GetAxisRaw ("Horizontal"));
		anim.SetFloat ("MoveY", Input.GetAxisRaw ("Vertical"));
		anim.SetBool ("PlayerMoving", playerMoving);
		anim.SetFloat ("LastMoveX", lastMove.x);
		anim.SetFloat ("LastMoveY", lastMove.y);
	}

	void AndroidController(){
		if ((CrossPlatformInputManager.GetAxisRaw ("Horizontal") > 0.5f || CrossPlatformInputManager.GetAxisRaw ("Horizontal") < -0.5f)) {								//Esquerda e direita										
			//transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));

			myRigidbody.velocity = new Vector2 (CrossPlatformInputManager.GetAxisRaw ("Horizontal") * moveSpeed, myRigidbody.velocity.y);
			playerMoving = true;
			lastMove = new Vector2 (CrossPlatformInputManager.GetAxisRaw ("Horizontal"), 0f);

		}

		if ((CrossPlatformInputManager.GetAxisRaw ("Vertical") > 0.5f || CrossPlatformInputManager.GetAxisRaw ("Vertical") < -0.5f)) {								//Esquerda e direita										
			//transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));

			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, CrossPlatformInputManager.GetAxisRaw ("Vertical") * moveSpeed);
			playerMoving = true;
			lastMove = new Vector2 (0f, CrossPlatformInputManager.GetAxisRaw ("Vertical"));

		}
		if (CrossPlatformInputManager.GetAxisRaw ("Horizontal") < 0.5f && CrossPlatformInputManager.GetAxisRaw ("Horizontal") > -0.5f) {								//Esquerda e direita
			
			myRigidbody.velocity = new Vector2 (0f, myRigidbody.velocity.y);
		}
		if (CrossPlatformInputManager.GetAxisRaw ("Vertical") < 0.5f && CrossPlatformInputManager.GetAxisRaw ("Vertical") > -0.5f) {									//Cima e baixo
			
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, 0f);
		}
		anim.SetFloat ("MoveX",CrossPlatformInputManager.GetAxisRaw ("Horizontal"));
		anim.SetFloat ("MoveY",CrossPlatformInputManager.GetAxisRaw ("Vertical"));
		anim.SetBool ("PlayerMoving", playerMoving);
		anim.SetFloat ("LastMoveX", lastMove.x);
		anim.SetFloat ("LastMoveY", lastMove.y);
	}
}

