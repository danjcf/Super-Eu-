using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {

	GameControl Controller;

	public int PowerValue = 10;												//REVER DEPOIS
	//current level
	public int vLevel = 1;
	//current Power Level
	public int PowerXP = 0;
	//current exp amount
	public int vCurrExp = 0;
	//exp amount needed for lvl 1
	public int vExpBase = 50;
	//exp amount left to next levelup
	public int vExpLeft = 50;
	//modifier that increases needed exp each level
	public float vExpMod = 1.3f;

	public Slider LvlSlider;
	public Slider PwrSlider;
	public Text LvlText;

	public bool EnoughPower;
	public bool PeriodDone;
	public bool ButtonSet;

	public Button ExitDoorButton;
	public GameObject ExitQuestMark;
	public GameObject PowerWindow;
	public GameObject PowerGWindow;
	public GameObject PowerGameDone;
	public GameObject PowerBox;
	public QuestManager QM;

	void Awake(){
		Controller = FindObjectOfType<GameControl> ();

	}

	void Start(){
		if (PlayerPrefs.GetInt ("FirstTime") == 1) {
			LoadLevelData ();
			vExpLeft = 50;
			vLevel = 1;
		} else {
			PlayerPrefs.SetInt ("FirstTime", 0);
			LoadLevelData ();
		}
		LvlSlider.maxValue = vExpLeft;
		if (PlayerPrefs.GetInt ("PGJustPlayed") == 1) {
			PowerGameDone.SetActive (true);
			PlayerPrefs.SetInt ("PGJustPlayed", 0);
		}
	}

	void Update(){
		if (EnoughPower || PeriodDone) {
			//Open window of Powers Game and sets the button to play it
			//Open window to inform player here
			if (EnoughPower) {
				PowerGameWindow ();
			}
			PowerBox.SetActive (true);
			ExitQuestMark.SetActive(false);
			ExitDoorButton.gameObject.SetActive (true);
			ButtonSet = true;
			EnoughPower = false;
		}
		if (PowerXP >= 67) {
			PowerBox.SetActive (true);
			ExitQuestMark.SetActive(false);
			ExitDoorButton.gameObject.SetActive (true);
		}

	}

	public void ClosePowerGameDone(){
		PowerGameDone.SetActive (false);
	}

	public void PowerGameWindow(){
		PowerWindow.SetActive (true);
	}

	public void ClosePowerWindow(){
		PowerWindow.SetActive (false);
	}

	public void PowerGW(){
		if (PlayerPrefs.GetInt ("DayCompleted") == 1) {
			Text PowerText = PowerGWindow.GetComponentInChildren<Text> ();
			PowerText.text = "Como completaste todas as missões do dia, estás na máxima força dos teus poderes! Podes continuar a salvar a cidade até o teu dia terminar."; 
		}
		PowerGWindow.SetActive (true);
	}

	public void ClosePGW(){
		PowerGWindow.SetActive (false);
	}

	void LoadLevelData(){
		vLevel = Controller.Data.level;
		LvlText.text = vLevel.ToString ();
		vCurrExp = Controller.Data.experience;
		vExpLeft = Controller.Data.XpLeft;
		PowerXP = Controller.Data.power_level;
		LvlSlider.value = vCurrExp;
		PwrSlider.value = PowerXP;
		if (PowerXP >= 67) {
			PowerBox.SetActive (true);
			ExitDoorButton.gameObject.SetActive (true);
			ButtonSet = true;
			EnoughPower = false;
		} else {
			ExitDoorButton.gameObject.SetActive (false);
			PowerBox.SetActive (false);
		}
	}

	public void SaveLevelData(){
		Controller.Data.level = vLevel;
		Controller.Data.experience = vCurrExp;
		Controller.Data.power_level = PowerXP;
		Controller.Data.XpLeft = vExpLeft;
	}


	public void SetPowerMax(){
		PowerXP = 100;
		PwrSlider.value = PowerXP;
	}

	//leveling methods
	public void GainExp(int e)																//ALTERAR DEPOIS
	{
		vCurrExp += e;
		PowerXP += PowerValue;
		LvlSlider.value = vCurrExp;
		PwrSlider.value += 10;
		if (PwrSlider.value >= 67 && !ButtonSet) {
			EnoughPower = true;
		}
		print ("Level: " + vLevel);
		print ("Current xP: " + vCurrExp);
		print ("XP Left to next level: " + vExpLeft);
		if(vCurrExp >= vExpLeft)
		{
			int nextlvlxp = vCurrExp - vExpLeft; 
			LvlUp();
			LvlSlider.value = nextlvlxp;
		}
	}

	void LvlUp()
	{
		vCurrExp -= vExpLeft;
		LvlSlider.value = 0;
		vLevel++;
		LvlText.text = vLevel.ToString();
		float t = Mathf.Pow(vExpMod, vLevel);
		vExpLeft = (int)Mathf.Floor(vExpBase * t);
		LvlSlider.maxValue = vExpLeft;
	}

}
