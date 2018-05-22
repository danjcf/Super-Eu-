using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPooler : MonoBehaviour {

	public Transform[] CoinSpawnGround;
	public Transform[] CoinSpawnAir;
	public float scrollSpeed;
	private ObjectPooler OP;
	private GameObject currentChild;
	float CoinCounter = 0.0f;
	public float CoinFrequency;

	void Start () {
		OP = ObjectPooler.SharedInstance;
		currentChild = null;
		FindCoinPool ();
	}

	void Update(){
		if (CoinCounter <= 0.0f) {
			GenerateCoin ();
		} else {
			CoinCounter -= Time.deltaTime * CoinFrequency;
		}

		for (int i = 0; i < transform.childCount; i++) {
			currentChild = transform.GetChild (i).gameObject;
			ScrollCoin (currentChild);
			if (currentChild.transform.position.x <= -25f) {
				currentChild.SetActive (false);
			}
		}
		if(Input.GetKeyDown(KeyCode.O))
		{
			GenerateCoin ();			
		}
	}
		
	int[] FindCoinPool(){
		int counter = 0;
		int insert = 0;
		int[] coinIndex = new int[2];
		foreach(ObjectPoolItem OPI in OP.itemsToPool){
			if (OPI.objectToPool.name == "GoldCoin") {
				coinIndex [insert] = counter;
				insert++;
			}
			if (OPI.objectToPool.name == "SilverCoin") {
				coinIndex [insert] = counter;
				insert++;
			}
			counter++;
		}
		return coinIndex;
	}

	void ScrollCoin( GameObject CurrentCoin){
		CurrentCoin.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
	}

	void GenerateCoin(){
		int[] coinIndex = FindCoinPool ();
		int ChosenPos;
		int ChosenCoin = Random.Range (0, coinIndex.Length);
		GameObject Coin = OP.GetPooledObject (coinIndex[ChosenCoin]);
		if (Coin.name == "GoldCoin") {
			ChosenPos = Random.Range (0, CoinSpawnGround.Length);
			Coin.transform.position = new Vector3 (CoinSpawnGround[ChosenPos].position.x, CoinSpawnGround[ChosenPos].position.y, 0);
		}
		if (Coin.name == "SilverCoin") {
			ChosenPos = Random.Range (0, CoinSpawnAir.Length);
			Coin.transform.position = new Vector3 (CoinSpawnAir[ChosenPos].position.x, CoinSpawnAir[ChosenPos].position.y, 0);
		}
		print (Coin.name);
		Coin.SetActive (true);
		CoinCounter = 1.0f;
	}
}
