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
	public int PowerLvl;
	public float minimumlvl;
	public float maximumlvl;
	private float minPwr;
	private float maxPwr;
	public float duration;
	private float startTime;
	private float t;
	public float xpleft;
	public float Maxslider;
	public bool LvlGained;

	void Awake(){
		LvlGained = false;
		QuestM = FindObjectOfType<QuestManager> ();
		lvl.maxValue = LvlManager.vExpLeft;
	}
	void OnEnable(){
		startTime = Time.time;
		duration = 2f;
		minimumlvl = LvlManager.vCurrExp;
		maximumlvl = LvlManager.vCurrExp + QuestM.CurrentQuest.questXP;
		Maxslider = lvl.maxValue;
		if (maximumlvl >= Maxslider) {
			xpleft = maximumlvl - Maxslider;
		}

		minPwr = LvlManager.PowerXP;
		maxPwr = minPwr + PowerLvl;
		LvlManager.GainExp (QuestM.CurrentQuest.questXP);
		lvltext.text = "Nível " + LvlManager.vLevel + ":";
		XPText.text = "+" + QuestM.CurrentQuest.questXP;
		PwrText.text = "+" + PowerLvl;													//VALOR TEMPORÁRIO
	}

	void Update(){
		t = (Time.time - startTime) / duration;
		/*																					Loop Infinito
		while (lvl.value != LvlManager.vCurrExp) {
			if (maximumlvl >= lvl.maxValue) {
				while (lvl.value != lvl.maxValue)
					lvl.value = Mathf.SmoothStep (minimumlvl, lvl.maxValue, t);
				minimumlvl = 0;
				lvl.value = Mathf.SmoothStep (minimumlvl, maximumlvl - lvl.maxValue, t);
			} else {
				lvl.value = Mathf.SmoothStep(minimumlvl,maximumlvl,t);
			}
		}
		*/
		if (maximumlvl >= Maxslider) {
			LvlGained = true;
			StartCoroutine (LevelUp ());
		} else {
			lvl.value = Mathf.SmoothStep(minimumlvl,maximumlvl,t);
		}
		pwr.value = Mathf.SmoothStep(minPwr,maxPwr,t);
	}

	IEnumerator LevelUp(){
		lvltext.text = "Nível " + (LvlManager.vLevel-1) + ":";
		lvl.value = Mathf.SmoothStep(minimumlvl,lvl.maxValue,t);
		yield return new WaitUntil (() => lvl.value == lvl.maxValue);
		maximumlvl = xpleft;
		minimumlvl = 0;
		lvl.maxValue = LvlManager.vExpLeft;
		lvl.value = Mathf.SmoothStep(minimumlvl, xpleft,t);
		lvltext.text = "Nível " + LvlManager.vLevel + ":";
		yield return new WaitUntil (() => lvl.value == xpleft);
	}
}
