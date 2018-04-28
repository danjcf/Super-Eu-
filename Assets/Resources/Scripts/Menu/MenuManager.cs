using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	GameControl Controller;
	int current_menu;
	public GameObject [] Menus;
    public GameObject Character;
	private string player_email, player_pass, player_pin;
	public InputField Input_pass, Input_email;
	public InputField Input_pin;
	private string player_name,player_age;
	public InputField Input_name, Input_age;
	public Toggle Toggle_rapaz, Toggle_rapariga;

	void Awake(){
		Controller = FindObjectOfType<GameControl> ();
		current_menu = 0;
	}

	private void Set_next_menu(GameObject [] Menu,int current)
    {
		Menu [current].SetActive (false);
		Menu [current+1].SetActive (true);
	}

    private void Set_previous_menu(GameObject [] Menu, int current)
    {
        Menu[current].SetActive(false);
        Menu[current - 1].SetActive(true);
    }

    public void EnterGame()
    {
		
		if (Controller.Data.Hairstyle == 0)
			Controller.Data.Hairstyle = 1;
		//PlayerPrefs.SetInt ("HairStyle", 1);
		SceneManager.LoadScene ("MainGame", LoadSceneMode.Single);
    }

	public void EnterFirstTimeMenu(){
		PlayerPrefs.SetInt ("FirstTime", 1);
		Menus [14].SetActive (false);
		Controller.CreateGame ();
		Menus [1].SetActive (true);
		current_menu = 1;
	}

	public void EnterLogin(){
		Menus [14].SetActive (false);
		Menus [16].SetActive (true);
		Controller.LoadGame ();

		current_menu = 16;
	}

	public void EnterReturnMenu(){
		//Verificar o Login
		Controller.LoadGame ();
		PlayerPrefs.SetInt ("FirstTime", 0);
		current_menu = 15;
		Menus [16].SetActive (false);
		Menus [15].SetActive (true);
	}

	void PersonalData(){
		if (Save_age() && Save_player_name() && Save_sex())
		{
			SpriteRenderer CharacterSprite = Character.GetComponent<SpriteRenderer> ();
			Sprite Boy = Resources.Load<Sprite> ("Sprites/Characters/Boy/Boy_hair1_spritesheet");
			Sprite Girl = Resources.Load<Sprite> ("Sprites/Characters/Girl/Girl_hair1_spritesheet");
			switch (Controller.Data.sex) {
			case 'M':
				
				CharacterSprite.sprite = Boy;
				break;
			case 'F':
				
				//PlayerPrefs.SetInt ("HairStyle", 1);
				Controller.Data.Hairstyle = 1;
				CharacterSprite.sprite = Girl;
				break;

			}

			Controller.Data.playerPos = new Vector3 (50f, -130f, 150);
			Controller.Data.playerRotation = new Quaternion (0, 0, 0, 0);
			Controller.Data.level = 1;
			current_menu++;
			Character.transform.setXposition(680f);
			Set_next_menu(Menus, 1);
		}
	}

	public void Next_menu (){
		
		switch (current_menu) {
		case 0:
			current_menu = 14;
			Menus [0].SetActive (false);
			Menus [current_menu].SetActive (true);
			break;
		case 1:      
			PersonalData ();  
			break;
		case 2:
			if (Controller.Data.Hairstyle == 0) {
				Controller.Data.Hairstyle = 1;
			}
            current_menu++;
			Set_next_menu (Menus, 2); 
            break;
		case 3:
            current_menu++;
			Set_next_menu (Menus, 3);
			break;
		case 4:
			Character.transform.setXposition (-2000f);
			current_menu++;
			Set_next_menu (Menus, 4);
			break;
		case 5:
			current_menu++;
			Set_next_menu (Menus, 5);
			break;
		case 6:
			current_menu++;
			Set_next_menu (Menus, 6);
			break;
		case 7:
			if (Save_parent_email () && Save_parent_password ()) {
				current_menu++;
				Set_next_menu (Menus, 7);
			}
			break;
		case 8:
			current_menu++;
			Set_next_menu (Menus, 8);
			break;
		case 10:
			if (SaveParentPin()) {
				Controller.SaveGame ();
				Menus [10].SetActive (false);
				current_menu = 12;
				Menus [12].SetActive (true);
			}
			break;
		case 11:
			Controller.SaveGame ();
			current_menu++;
			Set_next_menu (Menus, 11);
																												//Mandar mail com os dados da conta aqui
																												//ADICIONAR UMA COROUTINE PO MAIL
			/*
			mono_gmail mail = GameObject.Find ("MenuManager").GetComponent<mono_gmail> ();
			mail.SendMail (PlayerPrefs.GetString("ParentEmail"));
			*/
			break;
		case 12:
			current_menu++;
			Set_next_menu (Menus, 12);
			break;
		default:
			break;
		}

	}

    public void Previous_menu()
    {
        print("Menu:" + current_menu);
        switch (current_menu)
        {
            case 0:
                current_menu=0;
                
                break;
            case 2:
                current_menu--;
                Set_previous_menu(Menus, 2);
                Character.transform.setXposition(-2000f);
                break;
            case 3:
                current_menu--;
                Set_previous_menu(Menus, 3);
                break;
			case 4:
				current_menu--;
				Set_previous_menu(Menus, 4);
				break;
			case 5:
				current_menu--;
				Set_previous_menu (Menus, 5);
				break;
			case 6:
				current_menu--;
				Set_previous_menu (Menus, 6);
				break;
			case 7:
				current_menu--;
				Set_previous_menu (Menus, 7);
				break;
			case 8:
				current_menu--;
				Set_previous_menu (Menus, 8);
				break;
			case 9:
				current_menu--;
				Set_previous_menu (Menus, 9);
				break;
			case 10:
				current_menu--;
				Set_previous_menu (Menus, 10);
				break;
			case 11:
				Menus [current_menu].SetActive (false);
				current_menu = 9;
				Menus [current_menu].SetActive (true);
				break;
        }
    }

	public void MenuYes(){
		
		current_menu++;
		Menus [9].SetActive (false);
		Menus [current_menu].SetActive (true);
		print ("Menu" + current_menu);
	}

	public void MenuNo(){
		current_menu = 11;
		Menus [9].SetActive (false);
		Menus [current_menu].SetActive (true);
	}



	//------------Menu switch  -------------------------------------

	public bool Save_player_name (){
		if (Input_name.text != "")
		{
			player_name = Input_name.text;
			//PlayerPrefs.SetString("Nome", player_name);
			Controller.Data.playerName = player_name;
			return true;
		}
		return false;
	}

	public bool Save_sex (){
		if (Toggle_rapaz.isOn) {
			//PlayerPrefs.SetString ("Sexo","M");
			Controller.Data.sex = 'M';
			return true;
		} 
		if (Toggle_rapariga.isOn) {
			//PlayerPrefs.SetString ("Sexo","F");
			Controller.Data.sex = 'F';
			return true;
		}

		return false;
	}

	public bool Save_age () {
		if (Input_age.text != "") {
			player_age = Input_age.text;
			//PlayerPrefs.SetString("Idade", player_age);
			Controller.Data.age = player_age;
			return true;
		}
		return false;
	}

	//------------Parents info  -------------------------------------

	public bool Save_parent_email (){
		if (Input_email.text != ""+ "@"+"") {
			player_email = Input_email.text;
			//PlayerPrefs.SetString ("ParentEmail", player_email);
			Controller.Data.email = player_email;
			return true;
		} else {
			return false;
		}
	}

	public bool Save_parent_password (){
		if (Input_pass.text.Length >= 4) {
			player_pass = Input_pass.text;
			//PlayerPrefs.SetString ("ParentPassword", player_pass);
			Controller.Data.pass = player_pass;
			return true;
		} else {
			return false;
		}
	}

	public bool SaveParentPin(){
		if (Input_pin.text.Length >= 4) {
			player_pin = Input_pin.text;
			//PlayerPrefs.SetString ("ParentPin", player_pin);
			Controller.Data.pin = player_pin;
			return true;
		} else {
			return false;
		}
	}

}
