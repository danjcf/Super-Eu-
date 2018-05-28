using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePooler : MonoBehaviour {

	public Transform ObsSpawnGround;
	public Transform[] ObsSpawnAir;
	public Transform[] CoinSpawnAir;
	public Transform[] CoinSpawnGround;
	public float scrollSpeed;
	private ObjectPooler OP;
	public int numberofPools;
	private GameObject currentChild;
	public float GroundFrequency, AirFrequency, CoinFrequency;
	float GroundCounter = 0.0f;
	float AirCounter = 0.0f;
	float CoinCounter = 0.0f;

	public GameController GC;

	// Use this for initialization
	void Start () {
		OP = ObjectPooler.SharedInstance;
		currentChild = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (!GC.isPaused && !GC.GameInMenu) {
				if (GroundCounter <= 0.0f) {
					GenerateObstacle (1);
				} else {
					GroundCounter -= Time.deltaTime * GroundFrequency;
				}

				if (AirCounter <= 0.0f) {
					GenerateObstacle (2);
				} else {
					AirCounter -= Time.deltaTime * Random.Range (0.2f, 0.8f) * AirFrequency;
				}

				if (CoinCounter <= 0.0f) {
					GenerateObstacle (3);
				} else {
					CoinCounter -= Time.deltaTime * CoinFrequency;
				}
			for (int i = 0; i < transform.childCount; i++) {
				currentChild = transform.GetChild (i).gameObject;
				ScrollObstacle (currentChild);
				if (currentChild.transform.position.x <= -22f) {
					currentChild.SetActive (false);
				}
			}
			//FOR TEST
			/*
			if(Input.GetKeyDown(KeyCode.O))
			{
				GenerateObstacle (Random.Range(1,2));			
			}
			*/
		}
	}
		
	void ScrollObstacle( GameObject CurrentObstacle){
		CurrentObstacle.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
	}
		
	void GenerateObstacle(int choice){																					//Gera um obstáculo no chão
		int obstacleNum;
		bool CorrectName = false;
		GameObject obstacle = null;
		while(!CorrectName)													//Verifica se escolheu um obstáculo do chão
		{
			obstacleNum = Random.Range (0, OP.itemsToPool.Count);
			obstacle = OP.GetPooledObject (obstacleNum);
			if ((obstacle.name.Contains ("Air") && choice == 2) || (!obstacle.name.Contains ("Air") && choice == 1) || (obstacle.name.Contains("Coin") && choice == 3)) {		//Choice = 1 - Coloca o objecto no chão; Choice = 2 - Coloca o objecto no ar;
				CorrectName = true;
			}
		}
		if (choice == 1 && obstacle != null) {
			obstacle.transform.position = new Vector3 (ObsSpawnGround.position.x, ObsSpawnGround.position.y, 0);
			obstacle.SetActive (true);
			GroundCounter = 1.0f;
		}
		if (choice == 2 && obstacle != null) {
			int ChosenS = Random.Range (0, ObsSpawnAir.Length);  
			Transform SpawnChosen = ObsSpawnAir [ChosenS];
			obstacle.transform.position = new Vector3 (SpawnChosen.position.x, SpawnChosen.position.y, 0);
			obstacle.SetActive (true);
			AirCounter = 1.0f;
		}
		if (choice == 3 && obstacle != null) {
			int ChosenC = Random.Range (0, CoinSpawnAir.Length);
			Transform CoinSpawn = CoinSpawnAir [ChosenC];
			obstacle.transform.position = new Vector3 (CoinSpawn.position.x, CoinSpawn.position.y, 0);
			obstacle.SetActive (true);
			CoinCounter = 1.0f;
		}

	}
}
