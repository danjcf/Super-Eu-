using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

	public float scrollSpeed;
	private GameObject currentChild;
	public GameObject[] GroundObstacles;
	public GameObject[] AirObstacles;
	public float GroundFrequency, AirFrequency;
	float GroundCounter = 0.0f;
	float AirCounter = 0.0f;
	public Transform ObstacleSpawnGroundPoint;
	public Transform[] ObstacleSpawnAirPoints;

	public GameController GC;
	// Use this for initialization
	void Start () {
		currentChild = null;
		//NO FUTURO PÔR ISTO EM PAUSA
		GenerateRandomGroundObstacle ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GC.isPaused) {
			//Generate Objects
			if (GroundCounter <= 0.0f) {
				GenerateRandomGroundObstacle ();
			} else {
				GroundCounter -= Time.deltaTime * GroundFrequency;
			}

			if (AirCounter <= 0.0f) {
				GenerateRandomAirObstacle ();
			} else {
				AirCounter -= Time.deltaTime * Random.Range (0.2f, 0.8f) * AirFrequency;
			}

			//Scrolling
			for (int i = 0; i < transform.childCount; i++) {
				currentChild = transform.GetChild (i).gameObject;
				ScrollObstacle (currentChild);
				if (currentChild.transform.position.x <= -30f) {
					Destroy (currentChild);
				}
			}
		}
	}

	void ScrollObstacle( GameObject CurrentObstacle){
		CurrentObstacle.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
	}

	void GenerateRandomGroundObstacle(){
		GameObject newObstacle = Instantiate (GroundObstacles [Random.Range (0, GroundObstacles.Length)], ObstacleSpawnGroundPoint.position, Quaternion.identity);
		newObstacle.transform.SetParent (transform);
		GroundCounter = 1.0f;
	}

	void GenerateRandomAirObstacle(){
		GameObject newObstacle = Instantiate (AirObstacles [Random.Range (0, AirObstacles.Length)], ObstacleSpawnAirPoints[Random.Range(0,ObstacleSpawnAirPoints.Length)].position, Quaternion.identity);
		newObstacle.transform.SetParent (transform);
		AirCounter = 1.0f;
	}
}
