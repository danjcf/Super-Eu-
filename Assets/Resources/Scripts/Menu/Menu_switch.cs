using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_switch : MonoBehaviour {
	
	string player_name,player_age;
	InputField Input_name, Input_age;
	Toggle Toggle_rapaz, Toggle_rapariga;

	

	// Use this for initialization
	void Start () {
		
			Input_name = GameObject.Find ("Input_nome").GetComponent<InputField> ();
			Input_age = GameObject.Find ("AgeField").GetComponent<InputField> ();
			Toggle_rapaz = GameObject.Find ("Rapaz").GetComponent<Toggle> ();
			Toggle_rapariga = GameObject.Find ("Rapariga").GetComponent<Toggle> ();
			

	}
	//------------Menu 1 -------------------------------------

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



}
