using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit_game : MonoBehaviour {

	// Use this for initialization
	public GameObject Exit_window;
	public GameObject FirstMenu;
	public GameObject SecondMenu;
	public Toggle MusicT;
	public MenuManager MM;
	public AudioSource Music;
	
	public void Activate_exit_window(){
		if (!Exit_window.activeSelf) {
			Exit_window.SetActive (true);
            Time.timeScale = 0.0f;
		}
	}

	public void Quit_game (){
        //If we are running in a standalone build of the game
        //If we are running in the editor
     #if UNITY_EDITOR
        //Stop playing the scene
		PlayerPrefs.DeleteAll();
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        //Quit the application
		PlayerPrefs.DeleteAll();
        Application.Quit();
    #endif
    }

    public void Deactivate_exit_window(){
		if (Exit_window.activeSelf) {
			Exit_window.SetActive (false);
            Time.timeScale = 1.0f;
        }
	}

	public void OpenSettings(){
		FirstMenu.SetActive (false);
		SecondMenu.SetActive (true);
	}

	public void CloseSettings()
	{
		SecondMenu.SetActive (false);
		FirstMenu.SetActive (true);
	}

	public void EnterCredits(){
		Exit_window.SetActive (false);
		MM.OpenCredits ();
	}

	public void ChangeMusic(){
		if (MusicT.isOn) {
			if (!Music.isPlaying) {
				Music.Play ();
			}
			//Music is ON
		} else {
			if (Music.isPlaying) {
				Music.Pause ();
			}
			//Music is off
		}
	}

	public void ExitCredits()
	{
		Exit_window.SetActive (true);
		MM.CloseCredits ();
	}
}
