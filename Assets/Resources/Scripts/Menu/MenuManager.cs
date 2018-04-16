using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	int current_menu = 0;
	public GameObject [] Menus;
    public GameObject Character;


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

	public void Next_menu (){
		
		switch (current_menu) {
		case 0:
			current_menu++;
            
			Set_next_menu (Menus, 0); 
			break;
		case 1:
                Menu_switch dataSaved = GameObject.Find("SaveData").GetComponent<Menu_switch>();
                
                if (dataSaved.Save_age() && dataSaved.Save_player_name() && dataSaved.Save_sex())
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
			current_menu++;
			Set_next_menu (Menus, 7);
			break;
		case 8:
			current_menu++;
			Set_next_menu (Menus, 8);
			break;
		case 9:
			current_menu++;
			Set_next_menu (Menus, 9);
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
				Set_next_menu (Menus, 5);
				break;
			case 6:
				current_menu--;
				Set_next_menu (Menus, 6);
				break;
			case 7:
				current_menu--;
				Set_next_menu (Menus, 7);
				break;
			case 8:
				current_menu--;
				Set_next_menu (Menus, 8);
				break;
        }
    }

}
