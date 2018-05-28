using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

	public float scrollSpeed;
	public GameObject[] SecBuildings;

	public Transform BuildingSpawnPoint;
	float ChosenBuildingYPos;
	public float BuildingFrequency;
	private Vector3 posf;
	private Vector2 p, pos;
	private bool BuildingsDone;
	public GameController GC;
	public int ObjectCounter;

	//New Try
	public int Tier2Level;
	public int Tier3Level;

	public List<GameObject> ActiveBuilding;
	public List<GameObject> Tier1Building;
	public List<GameObject> Tier2Building;
	public List<GameObject> Tier3Building;
	public int BuildingCounter;
	private List<GameObject> RemoveNext;
	private bool NextIterationRemove;

	// Use this for initialization
	void Start () {
		NextIterationRemove = false;
		BuildingCounter = 1;
		RemoveNext = new List<GameObject> ();
	}

	void Update(){
		if (!GC.isPaused && !GC.GameInMenu) {
			
			foreach (GameObject go in ActiveBuilding.ToArray()) {
				ScrollBuilding (go);
				if (go.transform.position.x <= -22.5f) {
					if (ActiveBuilding.Count <= 6) {
						//Verifica qual o tier de edificios que tem de por através de um contador
						if (BuildingCounter >= 1 && BuildingCounter < Tier2Level) {
							GenerateBuilding (1);
							BuildingCounter++;
						}
						if (BuildingCounter == Tier2Level) {
							GenerateBuilding (2);
							BuildingCounter++;
						}
						if (BuildingCounter > Tier2Level && BuildingCounter < Tier3Level) {
							GenerateBuilding(1);
							BuildingCounter++;
						}
						if (BuildingCounter == Tier3Level) {
							GenerateBuilding(3);
							BuildingCounter = 1;
						}
					}
					//booleana que remove o objecto na proxima iteração
					NextIterationRemove = true;
					RemoveNext.Add(go);
				}
			}
			if (NextIterationRemove) {
				foreach (GameObject go in RemoveNext) {
					RemoveBuilding (go);
				}
				RemoveNext.Clear ();
			}
		}
	
	}


	void RemoveBuilding(GameObject BuildingToRemove){
		if (ActiveBuilding.Contains (BuildingToRemove)) {
			ActiveBuilding.Remove (BuildingToRemove);
		}
		if (!Tier1Building.Contains (BuildingToRemove) || !Tier2Building.Contains (BuildingToRemove) || !Tier3Building.Contains (BuildingToRemove)) {
			switch (BuildingToRemove.name) {
			case "PizzaHouse":
				Tier2Building.Add (BuildingToRemove);
				break;
			case "Hospital":
				Tier2Building.Add (BuildingToRemove);
				break;
			case "HouseFire":
				Tier3Building.Add (BuildingToRemove);
				break;
			default:
				Tier1Building.Add (BuildingToRemove);
				break;
			}
		}
		BuildingToRemove.SetActive (false);
	}

	void GenerateBuilding(int TierLevel){
		int ChosenBuilding = 0;
		GameObject ObjectToBuild = null;
		switch (TierLevel) {
		case 1:
			ChosenBuilding = Random.Range (0, Tier1Building.Count);
			ObjectToBuild = Tier1Building [ChosenBuilding];
			break;
		case 2:
			ChosenBuilding = Random.Range (0, Tier2Building.Count);
			ObjectToBuild = Tier2Building [ChosenBuilding];
			break;
		case 3:
			ChosenBuilding = Random.Range (0, Tier3Building.Count);
			ObjectToBuild = Tier3Building [ChosenBuilding];
			break;
		}
		float LastX = 0;
		float NewX = 0;
		LastX = FindBuildingLastPos ();

		SpriteRenderer SR = ObjectToBuild.GetComponent<SpriteRenderer> ();
		if (SR != null) {
			NewX = LastX + (SR.size.x / 2);
		} else {
			SpriteRenderer[] BuildingRenderChilds = ObjectToBuild.GetComponentsInChildren<SpriteRenderer> ();
			float TotalSize = 0f;
			foreach (SpriteRenderer SRO in BuildingRenderChilds) {
				TotalSize += SRO.size.x;
			}
			NewX = LastX + (TotalSize / 2);
		}
		ObjectToBuild.transform.position = new Vector3 (NewX - 1f, ObjectToBuild.transform.position.y, 0);
		switch (TierLevel) {
		case 1:
			Tier1Building.Remove (ObjectToBuild);
			break;
		case 2:
			Tier2Building.Remove (ObjectToBuild);
			break;
		case 3:
			Tier3Building.Remove (ObjectToBuild);
			break;
		}
		if (!ActiveBuilding.Contains (ObjectToBuild)) {
			ActiveBuilding.Add (ObjectToBuild);
		}
		ObjectToBuild.SetActive (true);
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
			float BuildingRendX = 0f;
			if (BuildingRend != null) {
				BuildingRendX = BuildingRend.size.x;
			}
			float LastX = BiggestPosX + (BuildingRendX / 2);
			return LastX;
		} else {
			return 28f; //Posição do SpawnPoint
		}
	}
}
