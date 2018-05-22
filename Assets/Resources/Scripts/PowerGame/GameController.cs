using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text PointsText;
	public Slider Stamina;
	public Button WaterButton;

	public bool isPaused;
	public bool SpecialEvent;
	public bool EventStarted;
	private bool PersonSaved;
	public int GamePoints;
	public float StaminaValue;
	GameControl controller;
	public int difficulty;
	public float SpecialTimer;
	public float FireHealth;
	public int PeopleSaved;
	private float valueToEmit;
	public int speedHurtRate;
	public float playerWater;

	public GameObject WaterStream;
	private ParticleSystem WaterJet;
	private ParticleSystem.EmissionModule em;
	public PowerPlayerMovement player;
	public int KillRate;
	public HouseAction house;

	// Use this for initialization
	void Start () {
		KillRate = 10;
		PersonSaved = false;
		speedHurtRate = 100;
		playerWater = 0f;
		FireHealth = 100f;
		SpecialTimer = 0f;
		SpecialEvent = false;
		difficulty = 0;
		isPaused = false;
		StaminaValue = UpdateStamina (/*controller.Data.power_level*/100);
		UpdatePoints (0);
		WaterJet = WaterStream.GetComponent<ParticleSystem> ();
		em = WaterJet.emission;
		WaterJet.Stop ();
		valueToEmit = em.rateOverTime.constant;
		playerWater = (valueToEmit + 10f)/ speedHurtRate;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isPaused) {
			if (Stamina.value > 0) {
				SpecialTimer += Time.deltaTime;
				StaminaValue -= (Time.deltaTime + difficulty);
				UpdateStamina (StaminaValue);
				print ("Stamina -" + Stamina.value);
			}
			if ((int)SpecialTimer == 15) {
				SpecialEvent = true;
			}
		}
		if (Stamina.value <= 0) {
			isPaused = true;
			print ("Acabou!!");
		}
			
		if (EventStarted && !PersonSaved) {
			WaterButton.gameObject.SetActive (true);
			WaterJet.Play ();
			FireHealth -= playerWater/KillRate;
			if (FireHealth <= 0) {
				WaterJet.Stop ();
				//Mudar a sprite da casa para apagado
				house.anim.SetBool("FireOut", true);
				print ("Salvaste a pessoa!!");
				//Invoke com 1 ou 2 segundos, mete os spawns a funcionar outra vez, desactiva o botão outra vez e incrementa o contador de pessoas salvas
				PersonSaved = true;

			}
		}
		if (!EventStarted) {
			WaterButton.gameObject.SetActive (false);
		}
	}

	public void StreamWater(){
		if (WaterJet.emission.rateOverTime.constant <= 300) {
			valueToEmit = em.rateOverTime.constant;
			em.rateOverTime = new ParticleSystem.MinMaxCurve (valueToEmit + 5f);
			playerWater = (valueToEmit + 10f)/ speedHurtRate;
		}
	}

	public float UpdateStamina(float Svalue){
		Stamina.value = Svalue;
		return Stamina.value;
	}

	public void UpdatePoints(int GPoints){
		PointsText.text = "Pontuação - " + GPoints;
	}
}
