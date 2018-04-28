using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarsAnimation : MonoBehaviour {

	public CharacterManager LvlManager;
	private QuestManager QuestM;
	public Slider lvl;
	public Slider pwr;
	public Text lvltext, XPText, PwrText;
	public int PowerValue;
	private float minimumlvl;
	private float maximumlvl;
	private float minPwr;
	private float maxPwr;
	public float duration;
	private float startTime;

	void Awake(){
		QuestM = FindObjectOfType<QuestManager> ();
		lvl.maxValue = LvlManager.vExpLeft;
	}
	void OnEnable(){
		startTime = Time.time;
		duration = 2f;
		minimumlvl = LvlManager.vCurrExp;
		maximumlvl = LvlManager.vCurrExp + QuestM.CurrentQuest.questXP;
		LvlManager.GainExp (QuestM.CurrentQuest.questXP);
		minPwr = LvlManager.PowerXP;
		maxPwr = LvlManager.PowerXP + PowerValue;
		lvltext.text = "Nível " + LvlManager.vLevel + ":";
		XPText.text = "+" + QuestM.CurrentQuest.questXP;
		PwrText.text = "+" + PowerValue;													//VALOR TEMPORÁRIO
	}

	void Update(){
		float t = (Time.time - startTime) / duration;
		lvl.value = Mathf.SmoothStep(minimumlvl,maximumlvl,t);
		pwr.value = Mathf.SmoothStep(minPwr,maxPwr,t);
	}
}
