using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

	public float moveSpeed;
	GameControl Controller;
	private Animator anim;
	private Rigidbody2D myRigidbody;
	public bool isPaused;
	private bool playerMoving;
	private Vector2 lastMove;
	Sprite Hair1, Hair2, Hair3;
	SpriteRenderer PlayerSprite;
	SwapColor ColorSwapper;

	void Awake()
	{
		Controller = FindObjectOfType<GameControl> ();
		ColorSwapper = GetComponent<SwapColor>();
	}
    // Use this for initialization
    void Start()
	{
		
		isPaused = false;
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
		PlayerSprite = GetComponent<SpriteRenderer>();
		LoadPlayerData ();
		switch (Controller.Data.sex) 													
		{
		case 'M':
			anim.SetBool ("isMale", true);

			Hair1 = Resources.Load<Sprite>("Sprites/Characters/Boy/Boy_hair1_spritesheet");
			Hair2 = Resources.Load<Sprite>("Sprites/Characters/Boy/Boy_hair2_spritesheet");
			Hair3 = Resources.Load<Sprite>("Sprites/Characters/Boy/Boy_hair3_spritesheet");
			break;
		case 'F':
			anim.SetBool ("isMale", false);
		
			Hair1 = Resources.Load<Sprite>("Sprites/Characters/Girl/Sprites/Girl_template");
			break;
		}


		anim.SetInteger ("HairStyle", Controller.Data.Hairstyle);
		switch (Controller.Data.Hairstyle)
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


		ColorSwapper.SwapHairColor(Controller.Data.Hair);
		ColorSwapper.SwapEyesColor(Controller.Data.Eyes);
		ColorSwapper.SwapBodyColor(Controller.Data.Skin);
		ColorSwapper.SwapShirtColor(Controller.Data.Shirt);
		ColorSwapper.SwapPantsColor(Controller.Data.Pants);
		ColorSwapper.SwapShoesColor(Controller.Data.Shoes);
    }

	void LoadPlayerData(){
		gameObject.transform.position = Controller.Data.playerPos;
		gameObject.transform.rotation = Controller.Data.playerRotation;
	}

	public void SavePlayerData(){
		Controller.Data.playerPos = gameObject.transform.position;
		Controller.Data.playerRotation = gameObject.transform.rotation;
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

