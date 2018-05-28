using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject WindowCanvas;
	public GameObject Menu1;
	public GameObject Menu2;
	public GameObject CreditsCanvas;
	public GameObject Credits;
	public GameObject RewardsMenu;
	public GameObject ScrollViewContent;
	public GameObject ClickedReward;
	public Toggle MusicG;
	public AudioSource Music;
	public QuestManager QM;
	public GameObject RewardPrefab;
	public Text[] thisRewardText;

	public void Activate_window()
    {
        if (!WindowCanvas.activeSelf)
        {
            WindowCanvas.SetActive(true);
            Time.timeScale = 0.0f;
            
        }
    }

    public void Deactivate_window()
    {
        if (WindowCanvas.activeSelf)
        {
            WindowCanvas.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

	public void ActivateSettings(){
		Menu1.SetActive (false);
		Menu2.SetActive (true);
	}

	public void DeactivateSettings(){
		Menu2.SetActive (false);
		Menu1.SetActive (true);
	}

	public void OpenCredits(){
		CreditsCanvas.SetActive (true);
		Credits.SetActive (true);
	}

	public void CloseCredits(){
		CreditsCanvas.SetActive (false);
		Credits.SetActive (false);
	}

	public void EnterRewards(){
		int counter = 0;
		bool AlreadyExists = false;
		float initialPosY = 80f;
		float increment = -140f;
		float PosY = initialPosY;
		Reward[] ContentChilds = ScrollViewContent.GetComponentsInChildren<Reward> ();
		foreach (Reward Ro in QM.RewardsToGive) {
			foreach (Reward CC in ContentChilds) {
				if (Ro == CC) {
					AlreadyExists = true;
					break;
				}
				AlreadyExists = false;
			}
			if (!AlreadyExists) {
				GameObject Rew = Instantiate (RewardPrefab as GameObject);
				Rew.transform.SetParent (ScrollViewContent.transform);
				Rew.transform.localPosition = new Vector3 (414.8f, -(PosY), 0);
				//Rew.transform.position = new Vector3 (80f, initialPosY * numberOfRewards + 100f, 0);
				Rew.transform.localScale = new Vector3 (1, 1, 1);
				Rew.name = "Reward" + counter;
				Text RewText = Rew.GetComponentInChildren<Text> ();
				RewText.text = Ro.RewardName;
				Reward thisReward = Rew.GetComponent<Reward> ();
				thisReward.RewardName = Ro.RewardName;
				thisReward.RewardDescription = Ro.RewardDescription;
				thisReward.RewardChosen = Ro.RewardChosen;
				Button RewButton = Rew.GetComponent<Button> ();
				RewButton.onClick.AddListener (ShowReward);
				PosY -= increment;
				counter++;
			}

		}
		CreditsCanvas.SetActive (true);
		Credits.SetActive (false);
		RewardsMenu.SetActive (true);
	}

	public void ShowReward(){
		GameObject buttonpressed = EventSystem.current.currentSelectedGameObject;
		Reward thisbuttonReward = buttonpressed.GetComponent<Reward> ();
		ClickedReward.SetActive (true);
		thisRewardText [0].text = thisbuttonReward.RewardName;
		thisRewardText [1].text = thisbuttonReward.RewardDescription;
	}

	public void CloseShowReward(){
		ClickedReward.SetActive (false);
	}

	public void CloseRewards(){
		RewardsMenu.SetActive (false);
		CreditsCanvas.SetActive (false);
	}

	public void ChangeMusic(){
		if (MusicG.isOn) {
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
