using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskStart : MonoBehaviour {

	GameControl Controller;

	public QuestManager QuestM;
	public BarsAnimation bars;
	public GameObject TaskStarted;
	public GameObject QuestStarted;
	public GameObject ButtonStart;
	public GameObject QuestProgress;
	public GameObject Joystick;
	public List<Reward> RewardsToChoose;
	private bool isTimerDone;
	public float QuestTime;
	private float Minutes;
	private float Seconds;
	public Text Timer;
	public GameObject ButtonFinish;
	public GameObject QuestCompleted;
	public GameObject Reward;
	public GameObject PinOption;
	public InputField Pin;
	public GameObject LvlUpReward;
	public GameObject RewardWindow;
	public GameObject RewardFirstWindow;
	public GameObject RewardGiven;
	public Text[] RewardButtonsText;
	public Text RewardChosenName;
	public Text RewardChosenDescription;
	public Reward ChosenReward;
	private int ChosenNumber;
	public CharacterManager CM;
	public GameObject PeriodDone;
	public Text PeriodDoneText;

	void Awake(){
		Controller = FindObjectOfType<GameControl> ();
	}

	void OnEnable(){
		QuestTime = QuestM.CurrentQuest.questTime*60;
	}

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
		if (Controller.Data.pin != "") {
			PinOption.SetActive (true);
			isTimerDone = true;
		} else {
			isTimerDone = false;
			QuestProgress.SetActive (true);
		}
		ButtonStart.SetActive (false);
	}

	public void EndQuest(){
		if (Controller.Data.pin != "") {
			//CheckPin
			if(Pin.text == Controller.Data.pin)
			{
				Pin.text = "";
				QuestStarted.SetActive (false);
				PinOption.SetActive(false);
				QuestCompleted.SetActive(true);
			}
		} else {
			QuestStarted.SetActive (false);
			QuestCompleted.SetActive (true);
		}
	}

	public void ContinueNext()
	{
		QuestCompleted.SetActive (false);
		if (bars.LvlGained && CheckIfRewardAllowed()) {
			LvlUpReward.SetActive (true);
			RewardsToChoose = QuestM.SortRewards ();
			RewardButtonsText [0].text = RewardsToChoose [0].RewardName;
			RewardButtonsText [1].text = RewardsToChoose [1].RewardName;
			RewardButtonsText [2].text = RewardsToChoose [2].RewardName;
		} else {
			Reward.SetActive (true);
		}

	}

	bool CheckIfRewardAllowed(){
		if (CM.vLevel == 1 || CM.vLevel == 3 || CM.vLevel == 7 || ((CM.vLevel % 5) == 0)) {
			return true;
		} else {
			return false;
		}
	}

	public void ShowReward(int RewardNumber){
		RewardWindow.SetActive (true);
		RewardChosenName.text = RewardsToChoose [RewardNumber].RewardName;
		RewardChosenDescription.text = RewardsToChoose [RewardNumber].RewardDescription;
		ChosenNumber = RewardNumber;
	}

	public void CloseRewardWindow(){
		RewardWindow.SetActive (false);
	}

	public void SelectReward(){
		//Botão onde aplica a recompensa escolhida - mete a recompensa na lista de recompensas ganhas (botão que irá tar no menu principal) e o RewardChosen a true
		ChosenReward = RewardsToChoose[ChosenNumber];
		if (!QuestM.RewardsToGive.Contains (ChosenReward)) {
			QuestM.RewardsToGive.Add (ChosenReward);
		}
		if (!QuestM.rewardsList [ChosenNumber].RewardChosen) {
			QuestM.rewardsList [ChosenNumber].RewardChosen = true;
		}

		RewardFirstWindow.SetActive (false);
		RewardGiven.SetActive (true);
		//Abrir janela a dizer que escolheu recompensas
	}

	public void ExitLevelReward(){
		RewardGiven.SetActive (false);
		RewardFirstWindow.SetActive (true);
		RewardWindow.SetActive (false);
		LvlUpReward.SetActive (false);
		Reward.SetActive (true);
		bars.LvlGained = false;
	}

	public void BackToMain(){
		QuestM.CurrentQuest.questCompleted = true;
		Quest QuestCompleted = QuestM.CurrentQuest;
		Reward.SetActive (false);
		Joystick.SetActive (true);
		QuestStarted.SetActive (true);
		ButtonStart.SetActive (true);
		ButtonFinish.SetActive (false);
		QuestM.player.isPaused = false;
		TaskStarted.SetActive (false);
		QuestM.SetQuestToNext (QuestM.CurrentQuest);
		Quest NextQuest = QuestM.CurrentQuest;
		if (((QuestCompleted.questPeriod != NextQuest.questPeriod) && NextQuest != null) || QuestCompleted.questNumber == QuestM.questsList.Length) {
			//Abre janela que diz que o período em questão foi completado
			PeriodDone.SetActive(true);
			PeriodDoneText.text = "Parabéns! Completaste o período da " + QuestCompleted.questPeriod + "!\n Graças a isso, sentes-te na máxima força e podes ir salvar a cidade!";
			CM.PowerXP = 100;
			CM.PwrSlider.value = 100;
			CM.PeriodDone = true;
		}
	}

	public void ClosePeriodofDay(){
		PeriodDone.SetActive (false);
	}
		

	public void EnterMinigame(){
		BackToMain ();
		QuestM.StartButton ();
	}
}
