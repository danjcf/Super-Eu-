using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Parents_info : MonoBehaviour {
	
	string player_email,player_pass;
	InputField Input_pass, Input_email;

	// Use this for initialization
	void Start () {
		Input_pass = GameObject.Find ("Input_pass").GetComponent<InputField> ();
		Input_email = GameObject.Find ("Input_email").GetComponent<InputField> ();
	}
	
	//------------------------ Menu 6 ----------------------

	public void Save_parent_email (){
		player_email = Input_email.text;
		PlayerPrefs.SetString ("Email", player_email);
		print("Email:" + PlayerPrefs.GetString ("Email"));
	}

	public void Save_parent_password (){
		player_pass = Input_pass.text;
		PlayerPrefs.SetString ("Password", player_pass);
		print("Password:" + PlayerPrefs.GetString ("Password"));
	}
}
