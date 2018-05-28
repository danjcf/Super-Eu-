using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour {

	GameControl Controller;
	public CharacterManager CharManager;
	public QuestManager QtManager;
	public PlayerController PlayerManager;

	void Awake(){
		Controller = FindObjectOfType<GameControl> ();
	}

	void OnEnable(){
		Controller.LoadGame ();
	}
		
	public void QuitGame(){
		StartCoroutine (SaveAll ());
		#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		//Quit the application
		Application.Quit();
		#endif
	}

	public void SaveGame(){
		StartCoroutine (SaveAll ());
	}

	IEnumerator SaveAll(){
		CharManager.SaveLevelData ();
		QtManager.SaveQuestData ();
		PlayerManager.SavePlayerData();
		while (Controller.SaveGame ())
			yield return null;
	}
}
