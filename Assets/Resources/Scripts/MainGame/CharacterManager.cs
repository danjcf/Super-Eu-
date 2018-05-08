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

	void Awake(){
		Controller = FindObjectOfType<GameControl> ();

	}

	void Start(){
		if(PlayerPrefs.GetInt("FirstTime") == 0)
			LoadLevelData ();
		LvlSlider.maxValue = vExpLeft;

	}

	void LoadLevelData(){
		vLevel = Controller.Data.level;
		LvlText.text = vLevel.ToString ();
		vCurrExp = Controller.Data.experience;
		vExpLeft = Controller.Data.XpLeft;
		PowerXP = Controller.Data.power_level;
		LvlSlider.value = vCurrExp;
		PwrSlider.value = PowerXP;
	}

	public void SaveLevelData(){
		Controller.Data.level = vLevel;
		Controller.Data.experience = vCurrExp;
		Controller.Data.power_level = PowerXP;
		Controller.Data.XpLeft = vExpLeft;
	}

	//leveling methods
	public void GainExp(int e)																//ALTERAR DEPOIS
	{
		vCurrExp += e;
		PowerXP += PowerValue;
		LvlSlider.value = vCurrExp;
		PwrSlider.value += 10;														
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
