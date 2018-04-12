using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour {
	
	public Sprite cardBack;
	public Sprite cardFace;
	public Card[] CardList;
	private int Match_counter, Match_tries;
	private Sprite[] SpriteList,TempSpriteList;
	public Card[] FlippedCards;
	public int FlipCardCounter;
	public bool isPaused = false;
	public int points;
	public GameObject StartWindow, GameWindow, EndWindow;
	private float timeClock;
	private Text timerText;
	private bool isOnDelay;

	// Use this for initialization
	void Awake () {
		string folderN = PlayerPrefs.GetString ("MemoryGameFolder");
		print ("Folder Name - " + folderN);
		FlippedCards = new Card[2];
		FlipCardCounter = 0;
		timeClock = 0;
		Match_counter = 0;
		Match_tries = 0;
		points = 0;
		isOnDelay = false;


	}

	void Update(){
		if (Match_counter == 5) {
			
			Invoke ("WinCondition", 1f);
		}
		if (GameWindow.activeSelf && !isOnDelay) {
			
			timeClock += Time.deltaTime;
			timerText.text = "Tempo - " + (int)timeClock + " Segundos";

		}	
	}

	public void StartGame(){
		StartWindow.SetActive (false);
		GameWindow.SetActive (true);
		Insert_images (PlayerPrefs.GetString ("MemoryGameFolder"));

		DeactivateItems ();
		UpdatePoints (0);
		timerText = GameObject.Find ("Time").GetComponent<Text> ();
		Time.timeScale = 0;
		StartCoroutine (DelayAllCards ());
		Time.timeScale = 1;
	}

	void WinCondition(){
		
		//Activa uma janela de parabéns e pontos de experiência com botão para voltar para a casa
		GameWindow.SetActive(false);
		EndWindow.SetActive (true);
		int bonus = 0;
		if (timeClock >= 0 && timeClock <= 30) {
			bonus = 20;
		}
		if (timeClock >= 31 && timeClock <= 40) {
			bonus = 15;
		}
		if (timeClock >= 41 && timeClock <= 50) {
			bonus = 10;
		}
		if (timeClock >= 51 && timeClock <= 60) {
			bonus = 5;
		}
		if (timeClock > 61) {
			bonus = 0;
		}

		int FinalPoints = points + bonus;
		Text GamePoint = GameObject.Find ("GamePoints").GetComponent<Text> ();
		Text BonusPoint = GameObject.Find ("BonusPoints").GetComponent<Text> ();
		Text FinalPoint = GameObject.Find ("FinalPoints").GetComponent<Text> ();
		GamePoint.text 	= " " + points;
		BonusPoint.text = "+" + bonus;
		FinalPoint.text = " " + FinalPoints;

		int stars = 0;

		if (FinalPoints >= 0 && FinalPoints <= 30) {
			//Uma estrela
			stars = 1;
			GameObject.Find("Star1").SetActive(false);
			GameObject.Find("Star2").SetActive(false);
		}
		if (FinalPoints >= 31 && FinalPoints <= 60) {
			//Duas Estrelas
			stars = 2;
			GameObject.Find("Star2").SetActive(false);
		}
		if (61 < FinalPoints) {
			//Tres estrelas
			stars = 3;
		} 

	}

	public void ReturnMainGame()
	{
		SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Match_minigame").buildIndex);
	}

	public Image GetItem(Card CardItem){
		Image[] cardItem = CardItem.GetComponentsInChildren<Image> (true);
		Image Item = cardItem [1];
		return Item;

	}

	void DeactivateItems(){
		
		foreach(Card CurrentCard in CardList) {
			Image Item = GetItem (CurrentCard);
			Item.gameObject.SetActive (false);
		}
	}

	int [] StringToInt(string [] stringToConvert){
		int counter = 0;
		int[] result = new int[stringToConvert.Length];
		foreach (string teststring in stringToConvert) {
			int.TryParse (teststring, out result [counter]);
			print (result [counter]);
			counter++;
		}
		return result;
	}

	void Insert_images(string folder_name){																	//Vai buscar as imagens para o minijogo ao banco de imagens

		string directory = null;
		if(Application.platform == RuntimePlatform.Android)
		{
			directory = "ImageBank\\" + folder_name + "\\";
		}
		if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor){
			directory = "ImageBank/" + folder_name + "/";
		}
		Image ItemImage;
		TempSpriteList = Resources.LoadAll <Sprite> (directory);
		if (PlayerPrefs.GetInt("HasSpritesheet") == 1) {
			print ("entrou if spritesheet");
			SpriteList = new Sprite[5];
			string temp = PlayerPrefs.GetString ("MemoryGameSprites");
			string[] IndexToChoose = temp.Split ('\t');
			int [] FinalIndex = StringToInt (IndexToChoose);
			int stringLength = IndexToChoose.Length;
			int temp_counter = 0;
			while (temp_counter < stringLength) {
				SpriteList [temp_counter] = TempSpriteList [FinalIndex[temp_counter]];
				temp_counter++;
			}
		} else {
			SpriteList = TempSpriteList;
		}
		PlayerPrefs.SetInt("HasSpritesheet", 0);
		List<int> ImagePlace = new List<int>(){0,1,2,3,4,5,6,7,8,9};
		foreach (Card cardtemp in CardList) {																//Ciclo que percorre as cartas (neste caso 10) e atribui uma imagem aleatória à carta
			ItemImage = GetItem(cardtemp);
			int Image_index = Randomizer (ImagePlace);
			switch (Image_index) {
				case 0:
				case 5:
					ItemImage.sprite = SpriteList [0];
					break;
				case 1:
				case 6:
					ItemImage.sprite = SpriteList [1];
					break;
				case 2:
				case 7:
					ItemImage.sprite = SpriteList [2];
					break;
				case 3:
				case 8:
					ItemImage.sprite = SpriteList [3];
					break;
				case 4:
				case 9:
					ItemImage.sprite = SpriteList [4];
					break;
			}
				
		}
	}

	int Randomizer(List<int> listRandom)
	{
		int index = Random.Range (0, listRandom.Count);
		int random_value = listRandom [index];
		listRandom.RemoveAt (index);
		return random_value;
	}
		
	public void ShowImage(Card FlippedCard)
	{
		
		Image tempItem = GetItem (FlippedCard);
		tempItem.gameObject.SetActive (true);

	}

	void ShowAllCards(Card[] Cards){
		foreach (Card card in Cards) {
			card.ShowCardFace ();
		}
	}

	public void HideImage(Card FlippedCard){
		Image tempItem = GetItem (FlippedCard);
		tempItem.gameObject.SetActive (false);
	}

	void HideAllCards(Card[] Cards){
		foreach (Card card in Cards) {
			card.FlipBack ();
		}
	}

	public void CheckFlippedCards()
	{
		Image FirstCard = GetItem (FlippedCards [0]);
		Image SecondCard = GetItem (FlippedCards [1]);
		//print ("Nome primeira carta: " + FirstCard.sprite.name + "Nome segunda carta: " + SecondCard.sprite.name);
		if (FirstCard.sprite == SecondCard.sprite) {
			print ("entrou if certo");
			FlippedCards [0].isMatched = true;
			FlippedCards [1].isMatched = true;
			FlippedCards [0] = null;
			FlippedCards [1] = null;
			Match_counter++;
			Match_tries++;
			UpdatePoints (Match_tries);
			Match_tries = 0;

		} else {
			print ("entrou if errado");
			FlippedCards [0].FlipBack ();
			FlippedCards [1].FlipBack ();
			FlippedCards [0] = null;
			FlippedCards [1] = null;
			Match_tries++;
		}
		FlipCardCounter = 0;
	}

	void UpdatePoints(int tries)
	{
		int temp_points = 0;					//Cada cena correcta vale 10 pontos 
		switch(tries)
		{
		case 0:
			temp_points = 0;
			break;
		case 1:
		case 2:
			temp_points = 15;
			break;
		case 3:
			temp_points = 10;
			break;
		case 4:
			temp_points = 7;
			break;
		case 5:
		case 6:
			temp_points = 5;
			break;
		default:
			temp_points = 1;
			break;
		}
		points += temp_points;
		Text UpdateText = GameObject.Find ("Points").GetComponent<Text> ();
		print ("Entrou update points - " + temp_points);
		print ("Pontos - " + points); 
		UpdateText.text = "Pontuação - " + points;
	}

	public IEnumerator DelayCheck()
	{
		
		isPaused = true;
		yield return new WaitForSeconds (1.5f);
		CheckFlippedCards ();

		isPaused = false;
	}

	IEnumerator DelayAllCards()
	{
		isPaused = true;
		isOnDelay = true;
		ShowAllCards(CardList);
		yield return new WaitForSeconds (2f);
		HideAllCards(CardList);
		isOnDelay = false;
		isPaused = false;
	}

//-----------------------------LEGACY FUNCTIONS-------------------------------
	void CheckCardEqual(){
		foreach (Card co in CardList) {
			if (co.isFlipped) {
				foreach(Card co_other in CardList)
				{
					Sprite first_image = co.GetComponentInChildren<Image> ().sprite;
					Sprite second_image = co_other.GetComponentInChildren<Image> ().sprite;
					if (co.name == co_other.name) {
						continue;
					} else if(first_image==second_image){
						co.isMatched = true;
						co_other.isMatched = true;
						//Match_counter++;
					}
				}
			}
		}
	}


}
