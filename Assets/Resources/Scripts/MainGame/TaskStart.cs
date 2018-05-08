using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskStart : MonoBehaviour {

	public QuestManager QuestM;
	public BarsAnimation bars;
	public GameObject TaskStarted;
	public GameObject QuestStarted;
	public GameObject ButtonStart;
	public GameObject QuestProgress;
	public GameObject Joystick;

	private bool isTimerDone;
	public float QuestTime;
	private float Minutes;
	private float Seconds;
	public Text Timer;
	public GameObject ButtonFinish;
	public GameObject QuestCompleted;
	public GameObject Reward;

	void Update(){
		if (QuestProgress.activeSelf && !isTimerDone) {
			Time.timeScale = 1f;
			QuestTime -= Time.deltaTime;
			Minutes = QuestTime/60;
			Seconds = QuestTime%60;
			if ((int)Seconds < 10) {
				Timer.text = "Tempo - " + "0" + (int)Minutes + ":" + "0" + (int)Seconds;
			} else {
				Timer.text = "Tempo - " + "0" + (int)Minutes + ":" + (int)Seconds;
			}
			if (QuestTime <= 0) {
				ButtonFinish.SetActive (true);
				isTimerDone = true;
				QuestProgress.SetActive (false);

			}
		}	
	}

	public void StartMission(){
		ButtonStart.SetActive (false);
		QuestTime = 30;
		isTimerDone = false;
		QuestProgress.SetActive (true);
	}

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
		Joystick.SetActive (true);
		QuestStarted.SetActive (true);
		ButtonStart.SetActive (true);
		ButtonFinish.SetActive (false);
		QuestM.player.isPaused = false;
		TaskStarted.SetActive (false);
		QuestM.SetQuestToNext (QuestM.CurrentQuest);
	}
		
	public void EnterMinigame(){
		BackToMain ();
		QuestM.StartButton ();
	}
}
