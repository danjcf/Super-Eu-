using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskStart : MonoBehaviour {

	public QuestManager QuestM;
	public BarsAnimation bars;
	public GameObject TaskStarted;
	public GameObject QuestStarted;
	public GameObject QuestCompleted;
	public GameObject Reward;

	public void EndQuest(){
		QuestStarted.SetActive (false);
		QuestCompleted.SetActive (true);
	}

	public void ContinueNext()
	{
		QuestCompleted.SetActive (false);
		Reward.SetActive (true);
	}

	public void BackToMain(){
		QuestM.CurrentQuest.questCompleted = true;
		Reward.SetActive (false);
		QuestStarted.SetActive (true);
		QuestM.player.isPaused = false;
		TaskStarted.SetActive (false);
		QuestM.SetQuestToNext (QuestM.CurrentQuest);
	}
		
	public void EnterMinigame(){
		BackToMain ();
		QuestM.StartButton ();
	}
}
