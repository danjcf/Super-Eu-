using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	int current_menu = 0;
	public GameObject [] Menus;
    public GameObject Character;
	private string player_email, player_pass, player_pin;
	public InputField Input_pass, Input_email;
	public InputField Input_pin;
	private string player_name,player_age;
	public InputField Input_name, Input_age;
	public Toggle Toggle_rapaz, Toggle_rapariga;


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
		if (PlayerPrefs.GetInt ("HairStyle") == 0)
			PlayerPrefs.SetInt ("HairStyle", 1);
		SceneManager.LoadScene ("MainGame", LoadSceneMode.Single);
    }

	public void EnterFirstTimeMenu(){
		Menus [14].SetActive (false);
		PlayerPrefs.DeleteAll ();
		Menus [1].SetActive (true);
		current_menu = 1;
	}

	public void EnterLogin(){
		Menus [14].SetActive (false);
		Menus [16].SetActive (true);
		current_menu = 16;
	}

	public void EnterReturnMenu(){
		//Verificar o Login
		current_menu = 15;
		Menus [16].SetActive (false);
		Menus [15].SetActive (true);
	}

	public void Next_menu (){
		
		switch (current_menu) {
		case 0:
			current_menu = 14;
			Menus [0].SetActive (false);
			Menus [current_menu].SetActive (true);
			break;
		case 1:      
                if (Save_age() && Save_player_name() && Save_sex())
                {
				SpriteRenderer CharacterSprite = Character.GetComponent<SpriteRenderer> ();
				Sprite Boy = Resources.Load<Sprite> ("Sprites/Characters/Boy/Boy_hair1_spritesheet");
				Sprite Girl = Resources.Load<Sprite> ("Sprites/Characters/Girl/Girl_hair1_spritesheet");
				switch (PlayerPrefs.GetString ("Sexo")) {
					case "M":
						CharacterSprite.sprite = Boy;
						break;
					case "F":
						PlayerPrefs.SetInt ("HairStyle", 1);
						CharacterSprite.sprite = Girl;
						break;

				}
                    current_menu++;
                    Character.transform.setXposition(680f);
                    Set_next_menu(Menus, 1);
                }
			break;
		case 2:
            current_menu++;
			Set_next_menu (Menus, 2); 
            break;
		case 3:
            current_menu++;
			Set_next_menu (Menus, 3);
			break;
		case 4:
			Character.transform.setXposition(-2000f);
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
				Menus [10].SetActive (false);
				current_menu = 12;
				Menus [12].SetActive (true);
			}
			break;
		case 11:
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
			PlayerPrefs.SetString("Nome", player_name);

			return true;
		}
		return false;
	}

	public bool Save_sex (){
		if (Toggle_rapaz.isOn) {
			PlayerPrefs.SetString ("Sexo","M");

			return true;
		} 
		if (Toggle_rapariga.isOn) {
			PlayerPrefs.SetString ("Sexo","F");

			return true;
		}

		return false;
	}

	public bool Save_age () {
		if (Input_age.text != "") {
			player_age = Input_age.text;
			PlayerPrefs.SetString("Idade", player_age);

			return true;
		}
		return false;
	}

	//------------Parents info  -------------------------------------

	public bool Save_parent_email (){
		if (Input_email.text != ""+ "@"+"") {
			player_email = Input_email.text;
			PlayerPrefs.SetString ("ParentEmail", player_email);
			print ("Email:" + PlayerPrefs.GetString ("ParentEmail"));
			return true;
		} else {
			return false;
		}
	}

	public bool Save_parent_password (){
		if (Input_pass.text.Length >= 4) {
			player_pass = Input_pass.text;
			PlayerPrefs.SetString ("ParentPassword", player_pass);
			print ("Password:" + PlayerPrefs.GetString ("ParentPassword"));
			return true;
		} else {
			return false;
		}
	}

	public bool SaveParentPin(){
		if (Input_pin.text.Length >= 4) {
			player_pin = Input_pin.text;
			PlayerPrefs.SetString ("ParentPin", player_pin);
			return true;
		} else {
			return false;
		}
	}

}
