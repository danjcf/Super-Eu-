using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

	public float scrollSpeed;
	public GameObject[] SecBuildings;
	private bool HouseCreated;
	GameObject currentChild;
	public Transform BuildingSpawnPoint;
	float ChosenBuildingYPos;
	public float BuildingFrequency;
	private Vector3 posf;
	private Vector2 p, pos;
	private bool BuildingsDone;
	public Transform SB;
	public GameController GC;
	private GameObject SE;
	public List<GameObject> ActiveSecBuildings;
	public List<GameObject> InactiveSecBuildings;

	public List<GameObject> ActiveMainBuildings;
	public List<GameObject> InactiveMainBuildings;

	// Use this for initialization
	void Start () {
		SE = null;
		currentChild = null;
		HouseCreated = false;
		ActiveSecBuildings = new List<GameObject> ();
		InactiveSecBuildings = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GC.isPaused) {
			//Scrolling
			//VER OS GAMEOBJECTS ACTIVOS - ESTES SÃO OS EM CENA | OS QUE TÃO FORA DE CENA SÃO OS DISPONIVEIS PARA COLOCAR NO SPAWN
			Transform[] Tbuildings;

			//SecBuildings contêm todos os objectos no jogo até agora
			foreach (GameObject go in SecBuildings) {
				Tbuildings = gameObject.GetComponentsInChildren<Transform> ();
				ScrollBuilding (go);
				if (go.transform.position.x <= -22.5f) {
					if (ActiveSecBuildings.Contains (go)) {
						ActiveSecBuildings.Remove (go);
					}
					if (!InactiveSecBuildings.Contains (go)) {
						InactiveSecBuildings.Add (go);
					}
					if (Tbuildings.Length <= 6 && !GC.SpecialEvent) {
						GenerateRandomBuilding ();
					}
					go.SetActive (false);
				}
			}
			if (GC.SpecialEvent) {
				if (!HouseCreated) {
					//Gerar evento especial (casa a arder), por exemplo de 30 em 30 segundos - Para teste fazer 15 em 15 segundos
					SE = GenerateSpecialHouse ();
				}
				if (!GC.EventStarted) {
					if (SE != null) {
						ScrollBuilding (SE);
					}
					if (SE.transform.position.x <= -22.5f) {
						if (ActiveMainBuildings.Contains (SE)) {
							ActiveMainBuildings.Remove (SE);
						}
						if (!InactiveMainBuildings.Contains (SE)) {
							InactiveMainBuildings.Add (SE);
						}
						HouseCreated = false;
						SE.SetActive (false);
					}
				}
				if (GC.EventStarted) {
					GC.isPaused = true;
				}
			}
		}
	}

	void ScrollBuilding( GameObject CurrentObstacle){
		CurrentObstacle.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
	}

	float FindBuildingLastPos(){ //Encontra a posição x do edificio mais à direita
		GameObject BuildController = FindObjectOfType<BuildingController> ().gameObject;
		Transform[] CurrentBuildingsPos = BuildController.GetComponentsInChildren<Transform> ();
		if (CurrentBuildingsPos.Length != 1) {
			float BiggestPosX = 0f;
			int buildcounter = 0;
			int BiggestBuilding = 0;
			foreach (Transform to in CurrentBuildingsPos) {
				if (to.localPosition.x >= BiggestPosX) {
					BiggestPosX = to.localPosition.x;
					BiggestBuilding = buildcounter;
				}
				buildcounter++;
			}
			SpriteRenderer BuildingRend = CurrentBuildingsPos [BiggestBuilding].gameObject.GetComponent<SpriteRenderer> ();
			float LastX = BiggestPosX + (BuildingRend.size.x / 2);
			return LastX;
		} else {
			return 28f; //Posição do SpawnPoint
		}
	}



	void GenerateRandomBuilding(){
		//Escolhe um edificio aleatoriamente e ativa-o no spawn point
		int ChosenBuilding = Random.Range (0, InactiveSecBuildings.Count);
		GameObject building = null;
		float NewX = 0;

		building = InactiveSecBuildings [ChosenBuilding];
		float LastX = FindBuildingLastPos ();

		SpriteRenderer SR = building.GetComponent<SpriteRenderer> ();
		if (SR != null) {
			NewX = LastX + (SR.size.x / 2);
		} else {
			SpriteRenderer[] BuildingRenderChilds = building.GetComponentsInChildren<SpriteRenderer> ();
			float TotalSize = 0f;
			foreach (SpriteRenderer SRO in BuildingRenderChilds) {
				TotalSize += SRO.size.x;
			}
			NewX = LastX + (TotalSize / 2);
		}
		building.transform.position = new Vector3 (NewX-1f, building.transform.position.y, 0); 

		if (!ActiveSecBuildings.Contains (building)) {
			ActiveSecBuildings.Add (building);
		}
		if (InactiveSecBuildings.Contains (building)) {
			InactiveSecBuildings.Remove (building);
		}
		building.SetActive (true);

	}

	GameObject GenerateSpecialHouse(){
		int ChosenOne = Random.Range (0, InactiveMainBuildings.Count);
		GameObject MainB = InactiveMainBuildings [ChosenOne];
		SpriteRenderer SR = MainB.GetComponent<SpriteRenderer> ();
		float LastX = FindBuildingLastPos ();
		float TotalX = 0f; 
		if (SR != null) {
			TotalX = LastX + SR.size.x;
		}
		MainB.transform.position = new Vector3 (TotalX, MainB.transform.position.y , 0);

		if (!ActiveSecBuildings.Contains (MainB)) {
			ActiveSecBuildings.Add (MainB);
		}
		if (InactiveMainBuildings.Contains (MainB)) {
			InactiveMainBuildings.Remove (MainB);
		}
		MainB.SetActive (true);

		HouseCreated = true;
		return MainB;
	}
}
