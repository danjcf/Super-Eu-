using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject Window;

	public void Activate_window()
    {
        if (!Window.activeSelf)
        {
            Window.SetActive(true);
            Time.timeScale = 0.0f;
            
        }
    }

    public void Deactivate_window()
    {
        if (Window.activeSelf)
        {
            Window.SetActive(false);
            Time.timeScale = 1.0f;
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
}
