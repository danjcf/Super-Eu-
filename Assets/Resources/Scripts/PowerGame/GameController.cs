using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	GameControl controller;

	public Text PointsText;
	public Slider Stamina;
	public Button WaterButton;

	public bool isPaused;
	public bool EventStarted;
	private bool PersonSaved;
	private bool EventFinished;
	private bool SetRestart;
	public bool GameInMenu;

	public static int GlobalGameCounter = 3;
	public int GameCounter;
	public int GamePoints;
	public int HighestGP;
	public float StaminaValue;

	public float difficulty;
	public float SpecialTimer,ActionTimer;
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
	public BuildingController BC;

	public GameObject StartG;
	public GameObject FirstMenu;
	public GameObject SecondMenu;
	public GameObject InGame;
	public GameObject EndGame;
	public GameObject RestartMenu;
	public GameObject FinalGame;
	public GameObject MainMenu;
	public Text FinalPoints;
	public Text FinalPeople;
	public Text RestartText;
	public Text RestartPoints;
	public Text RestartPeople;

	private static int FinalScore = 0;
	private static int FinalPeopleSaved = 0;

	void Awake(){
		controller = FindObjectOfType<GameControl> ();
	}

	// Use this for initialization
	void Start () {
		GameCounter = GlobalGameCounter;
		GameInMenu = true;
		KillRate = 10;
		SetRestart = true;
		PersonSaved = false;
		isPaused = true;
		EventFinished = false;
		speedHurtRate = 100;
		playerWater = 0f;
		FireHealth = 100f;
		SpecialTimer = 0f;
		ActionTimer = 0f;
		StaminaValue = UpdateStamina (controller.Data.power_level);
		UpdatePoints (0);
		WaterJet = WaterStream.GetComponent<ParticleSystem> ();
		em = WaterJet.emission;
		WaterJet.Stop ();
		valueToEmit = em.rateOverTime.constant;
		playerWater = (valueToEmit + 10f)/ speedHurtRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameInMenu) {
			if (!isPaused) {
				if (Stamina.value > 0) {
					StaminaValue -= (Time.deltaTime + difficulty);
					UpdateStamina (StaminaValue);
				}
			}
			if (Stamina.value <= 0) {
				isPaused = true;
				if (SetRestart) {
					SetPointsAndPeople ();
					SetRestart = false;
					GameInMenu = true;
				}
			}
			
			if (EventStarted) {
				isPaused = true;
				player.StartShooting ();
				WaterButton.gameObject.SetActive (true);
				WaterJet.Play ();
				ActionTimer += Time.deltaTime;
				FireHealth -= playerWater / KillRate;
				if (FireHealth <= 0 && !PersonSaved) {
					WaterJet.Stop ();
					//Mudar a sprite da casa para apagado
					house.anim.SetBool ("FireOut", true);
					EventOver ();
					//Invoke com 1 ou 2 segundos, mete os spawns a funcionar outra vez, desactiva o botão outra vez e incrementa o contador de pessoas salvas
				}
			}
			if (EventFinished) {
				player.StopShooting ();
				Invoke ("ResetStuff", 2f);
			}
		}
	}

	public void StreamWater(){
		if (WaterJet.emission.rateOverTime.constant <= 300) {
			valueToEmit = em.rateOverTime.constant;
			em.rateOverTime = new ParticleSystem.MinMaxCurve (valueToEmit + 5f);
			playerWater = (valueToEmit + 10f)/ speedHurtRate;
		}
	}

	void EventOver(){
		WaterButton.gameObject.SetActive (false);
		PeopleSaved++;
		int bonus = 0;
		int bonusGP = 0;
		float total = 0f;
		if (ActionTimer <= 10) {
			bonus = 15;
			bonusGP = 25;
		}
		if (ActionTimer <= 15 && ActionTimer > 10) {
			bonus = 10;
			bonusGP = 15;
		}
		if (ActionTimer > 15) {
			bonus = 5;
			bonusGP = 5;
		}
		float current = Stamina.value;
		GamePoints += bonusGP;
		UpdatePoints (GamePoints);
		total = current + bonus;
		if (total >= 100) {
			total = 100f;
		}

		Stamina.value = Mathf.SmoothStep (current, total, 2f);
		StaminaValue = total;
		difficulty += 0.005f;
		//Colocar aqui os pontos ganhos por salvar a pessoa
		EventStarted = false;
		EventFinished = true;
	}

	void ResetStuff(){
		player.StartRunning ();
		ResetHouse ();
		isPaused = false;
		PersonSaved = false;
		EventFinished = false;
	}

	void ResetHouse(){
		FireHealth = 100f;
		ActionTimer = 0f;
		SpecialTimer = 0f;
		playerWater = 0.5f;
		em.rateOverTime = new ParticleSystem.MinMaxCurve (60f);
	}

	public void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public float UpdateStamina(float Svalue){
		Stamina.value = Svalue;
		return Stamina.value;
	}

	public void UpdatePoints(int GPoints){
		PointsText.text = "Pontuação: " + GPoints;
	}

	void SetPointsAndPeople(){
		EndGame.SetActive (true);
		InGame.SetActive (false);
		GameCounter--;
		GlobalGameCounter = GameCounter;
		if (GameCounter != 0) {
			RestartPoints.text = GamePoints.ToString ();
			RestartPeople.text = PeopleSaved.ToString ();
			FinalScore += GamePoints;
			FinalPeopleSaved += PeopleSaved;
			RestartMenu.SetActive (true);
			RestartText.text = "Os teus poderes esgotaram-se! Mas podes recarregar os teus poderes mais " + GameCounter + " vezes e continuar a salvar a cidade!";
		} else {
			FinalScore += GamePoints;
			FinalPeopleSaved += PeopleSaved;
			FinalPoints.text = FinalScore.ToString();
			FinalPeople.text = FinalPeopleSaved.ToString ();
			FinalGame.SetActive (true);
		}
		if (GamePoints > HighestGP) {
			HighestGP = GamePoints;
		}

	}

	public void StartGame(){
		StartG.SetActive (false);
		InGame.SetActive (true);
		GameInMenu = false;
		isPaused = false;
		player.StartRunning ();
	}

	public void GoNextMenu(){
		FirstMenu.SetActive (false);
		SecondMenu.SetActive (true);
	}

	public void GoPreviousMenu()
	{
		SecondMenu.SetActive (false);
		FirstMenu.SetActive (true);
	}

	public void OpenPauseMenu(){
		Time.timeScale = 0.0f;
		isPaused = true;
		MainMenu.SetActive (true);
	}

	public void ClosePauseMenu(){
		Time.timeScale = 1.0f;
		isPaused = false;
		MainMenu.SetActive (false);
	}

	public void ContinueGame()
	{
		GlobalGameCounter = 3;
		FinalScore = 0;
		FinalPeopleSaved = 0;
		SavePowerGame ();
		//Final
		PlayerPrefs.SetInt("PGJustPlayed",1);
		SceneManager.LoadScene ("MainGame",LoadSceneMode.Single);

		//Teste
		/*
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit ();
		#endif
		*/
	}
		
	void SavePowerGame()
	{
		controller.Data.power_level = 0;
		controller.SaveGame ();
	}
}
