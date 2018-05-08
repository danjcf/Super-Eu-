using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class QuestManager : MonoBehaviour {

	public Quest prefab;
	public GameObject Match;
	GameControl Controller;
	public GameObject TaskStarted;
	public Text MissionName;
	public GameObject PinOption;
	public GameObject ButtonOption;
	public GameObject Joystick;
	public GameObject QuestShow;
	public GameObject QuestShowBox;
    public bool[] questCompleted;
	public Quest[] questsList;
	private Text QuestBox;
	private Button QBoxButton;
	public Button[] InactiveButtons;
	public GameObject[] QuestMarkers;
	public GameObject[] QuestItems;
	public GameObject QuestWindow;
	public PlayerController player;
	public CharacterManager LvlManager;
	public bool inMinigame,CurrentQuestCompleted;
	public Image FadeImage;
	public Animator anim;
	public Quest CurrentQuest,CurrentSavedQuest;

	// Use this for initialization
	void Awake () {
		inMinigame = false;
		Controller = FindObjectOfType<GameControl> ();
		CurrentQuestCompleted = false;
		QuestWindow.SetActive(false);
		QuestBox = GameObject.Find ("Quest Box").GetComponentInChildren<Text>();
		QBoxButton = QuestBox.gameObject.GetComponentInParent<Button> ();
		CreateListOfQuests ();
		if(PlayerPrefs.GetInt("FirstTime") == 0){
			LoadQuestData ();
		}
		if (PlayerPrefs.GetInt ("DayCompleted") == 0) {
			InitializeQuestMarker ();
		}
		PlayerPrefs.SetInt ("inMinigame", 0);
	}


	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt("inMinigame") == 0) {
			//SetButtonCurrentQuest ();
			CheckCurrentQuest ();
		}
		/*
		if (CurrentQuestCompleted) {
			Quest currQuest = CheckCurrentQuest ();
			//currQuest.questCompleted = true;
			SetQuestToNext (currQuest);
			//LvlManager.GainExp (10);
			CurrentQuestCompleted = false;
		}
		*/
	}

	void LoadQuestData(){
		print ("Current task number - " + Controller.Data.CurrentTaskNumber);
		if (!Controller.Data.DayFinished) {
			PlayerPrefs.SetInt ("DayCompleted", 0);
			foreach (Quest qo in questsList) {
				if (qo.questNumber < Controller.Data.CurrentTaskNumber) {
					qo.questCompleted = true;
				} else {
					break;
				}
			}
		} else {
			foreach (Quest qo in questsList) {
				if (qo.questNumber != questsList.Length) {
					qo.questCompleted = true;
				}
				if (qo.questNumber == questsList.Length) {
					CurrentQuest = qo;
				}
			}
			CurrentQuest.questCompleted = true;
			SetQuestToNext (CurrentQuest);
		}
	}

	public void SaveQuestData(){
		if (PlayerPrefs.GetInt ("DayCompleted") != 1) {
			Controller.Data.CurrentTaskNumber = CheckCurrentQuest ().questNumber;
			Controller.Data.DayFinished = false;
		} else {
			Controller.Data.CurrentTaskNumber = questsList.Length + 1;
			Controller.Data.DayFinished = true;
		}
	}

	void InitializeQuestMarker (){
		Quest FirstQuest = CheckCurrentQuest ();
		FirstQuest.QuestMark.SetActive (true);
	}

	public void SetQuestToNext(Quest QuestFinished){
		if (QuestFinished.questNumber < questsList.Length) {
			int numberOfQuest = QuestFinished.questNumber;													//Os numeros das quests começam em 1 e o vector começa em 0 portanto a proxima quest é num da quest - 1
			Quest NewQuest = questsList [numberOfQuest];
			NewQuest.gameObject.SetActive (true);
			print ("Quest Info - Num: " + NewQuest.questNumber + " Nome: " + NewQuest.questName);
			HideQuestButtons (QuestFinished);
			QuestFinished.gameObject.SetActive (false);
			NewQuest.QuestMark.SetActive (true);
			CheckCurrentQuest ();
		} else {
			HideQuestButtons (QuestFinished);
			Button QBox = QuestBox.gameObject.GetComponentInParent<Button> ();
			QBox.enabled = false;
			PlayerPrefs.SetInt ("DayCompleted", 1);
			QuestFinished.gameObject.SetActive (false);
			QuestBox.text = "Completaste todas as missões!";
		}
	}

	void HideQuestButtons(Quest QuestObj){
		
		string QuestMark = QuestObj.questObject + "_quest";
		string QuestB = QuestObj.questObject + "_Button";
		foreach (GameObject go in QuestMarkers) {
			if ((go.name == QuestMark))
			{
				go.SetActive (false);
				break;
			}
		}

		foreach (Button bo in InactiveButtons) {
			if ((bo.name == QuestB))
			{
				bo.gameObject.SetActive (false);
				break;
			}
		}
	}

	void CreateListOfQuests(){ 																				//função que lê o ficheiro com a lista de tarefas e transforma em quests (usando o prefab da quest)
		
		List<string> quests_name = AlternativeReadFile("Lista de Tarefas");									//Formato do ficheiro txt: Nome da quest - Descrição da quest - Duração da quest - Objecto onde vai estar a quest
																											//Index da palavra Manhã no txt, que representa a divisão das tarefas da manhã
		int lastIndex = quests_name.Count;
		string[] tasks = new string[lastIndex];
		quests_name.CopyTo (0,tasks,0,lastIndex);			
		int counter = 0;
		int morningcounter = 1;
		int afternooncounter = 1;
		int nightcounter = 1;
		string[] foo = new string[4];
		questsList = new Quest[tasks.Length];
		char[] charsToTrim = {'\r'};
		while(counter<tasks.Length)														//Ciclo para criar as quests, retira-se 1 ao tasks.length por causa da string "Tarde" a meio do ficheiro
		{
			Quest quest = (Instantiate (prefab) as Quest);												//Cria as várias quests
			quest.transform.SetParent (GameObject.Find ("Quest Box").transform);						//Define as quests como children do questmanager
			quest.questNumber = counter + 1;
			foo = tasks [counter].Split (';');													//Divide o txt através dos tabs e separa com o formato dito em cima
			foo[3] = foo [3].Trim (charsToTrim);
			quest.questName = foo [0];
			quest.questDescription = foo [1];
			quest.questPeriod = foo [2];
			quest.questObject = foo [3];
			int.TryParse (foo [4], out quest.questXP);
			quest.player = player;
			switch (quest.questPeriod) {
			case "Manhã":
				quest.gameObject.name = "QuestMorning" + morningcounter;
				morningcounter++;
				break;
			case "Tarde":
				quest.gameObject.name = "QuestAfternoon" + afternooncounter;
				afternooncounter++;
				break;
			case "Noite":
				quest.gameObject.name = "QuestNight" + nightcounter;
				nightcounter++;
				break;
			}

			quest.QuestStart = QuestWindow;
			quest.gameObject.SetActive (false);
			questsList [counter] = quest;
			foreach (Button bo in InactiveButtons) {
				if (bo.name == (foo [3] + "_Button")) {
					quest.QuestButton = bo.gameObject;
					//bo.onClick.AddListener (quest.QuestTextButton);
				}
				bo.gameObject.SetActive (false);
			}

			foreach (GameObject go in QuestMarkers) {
				if (go.name == (foo [3] + "_quest")) {
					quest.QuestMark = go;
				}
				go.SetActive (false);
			}
			counter++;
		}

	}

	public Quest CheckCurrentQuest()																			//Função que diz qual é a quest atual (quest a ser apresentada na GUI)
	{																									//Experimentar ver primeira quest da lista de quests que tenha isCompleted=false
		int quest_length = questsList.Length;

		foreach(Quest qo in questsList){
			if (!qo.questCompleted) {												//Verifica a primeira quest que não foi completada. Ativa essa quest, apresenta na GUI e sai do ciclo
				qo.gameObject.SetActive(true);
				QuestBox.text = "Próxima Missão:\n" + qo.questName;
				CurrentQuest = qo;
				PlayerPrefs.SetInt ("DayCompleted", 0);
				return qo;
			}

		}
		return null;
	}

	public void StartButton()                                                       					//Iniciar o minijogo (Match 2 images)
	{
		
		//Aqui provavelmente poe-se um change scene
		//Por nos playerPrefs a info da tarefa
		//Fazer scene load
		Quest quest_started = CheckCurrentQuest();

		//PlayerPrefs.SetString("MemoryGameFolder", /*quest_started.questName*/"TestImages");										//Tipo da Tarefa
		int SpriteCounter = 0;
		List<int> RandomNumbers;
		string SpritesToUse = null;
		switch(quest_started.questObject)																						//Quando houver mais minigames, reformular isto
		{
		case "Fridge":
			
			RandomNumbers = new List<int> () {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24};

			while (SpriteCounter < 5) {
				int ChosenSprite = Randomizer (RandomNumbers);
				if (SpriteCounter == 4) {
					SpritesToUse += ChosenSprite.ToString ();
				} else
					SpritesToUse += ChosenSprite.ToString () + '\t';
				SpriteCounter++;
			}
			PlayerPrefs.SetString ("MemoryGameFolder", "Food");
			PlayerPrefs.SetInt ("HasSpritesheet", 1);
			PlayerPrefs.SetString ("MemoryGameSprites", SpritesToUse);
			break;

		case "Kitchen table":
			RandomNumbers = new List<int> () {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24};

			while (SpriteCounter < 5) {
				int ChosenSprite = Randomizer (RandomNumbers);
				if (SpriteCounter == 4) {
					SpritesToUse += ChosenSprite.ToString ();
				} else
					SpritesToUse += ChosenSprite.ToString () + '\t';
				SpriteCounter++;
			}
			PlayerPrefs.SetString ("MemoryGameFolder", "Food");
			PlayerPrefs.SetInt ("HasSpritesheet", 1);
			PlayerPrefs.SetString ("MemoryGameSprites", SpritesToUse);
			break;

		case "Shower":
			PlayerPrefs.SetString ("MemoryGameFolder", "Teeth");
			PlayerPrefs.SetInt ("HasSpritesheet", 1);
			PlayerPrefs.SetString ("MemoryGameSprites", "0\t1\t2\t3\t4");
			break;

		case "Bathroom Sink":
			
			PlayerPrefs.SetString ("MemoryGameFolder", "Teeth");
			PlayerPrefs.SetInt ("HasSpritesheet", 1);
			PlayerPrefs.SetString ("MemoryGameSprites", "0\t1\t2\t3\t4");
			break;
		
		case "Small Bed":

			RandomNumbers = new List<int> () {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24};

			while (SpriteCounter < 5) {
				int ChosenSprite = Randomizer (RandomNumbers);
				if (SpriteCounter == 4) {
					SpritesToUse += ChosenSprite.ToString ();
				} else
					SpritesToUse += ChosenSprite.ToString () + '\t';
				SpriteCounter++;
			}
			PlayerPrefs.SetString ("MemoryGameFolder", "Food");
			PlayerPrefs.SetInt ("HasSpritesheet", 1);
			PlayerPrefs.SetString ("MemoryGameSprites", SpritesToUse);
			break;
		default:
			RandomNumbers = new List<int> () {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24};

			while (SpriteCounter < 5) {
				int ChosenSprite = Randomizer (RandomNumbers);
				if (SpriteCounter == 4) {
					SpritesToUse += ChosenSprite.ToString ();
				} else
					SpritesToUse += ChosenSprite.ToString () + '\t';
				SpriteCounter++;
			}
			PlayerPrefs.SetString ("MemoryGameFolder", "Food");
			PlayerPrefs.SetInt ("HasSpritesheet", 1);
			PlayerPrefs.SetString ("MemoryGameSprites", SpritesToUse);
			break;
		}
		QuestWindow.SetActive (false);
		player.isPaused = false;
		PlayerPrefs.SetInt ("inMinigame", 1);
		CurrentQuestCompleted = true;

		//Initiate.Fade("Match_minigame", Color.black, 0.5f);

		SceneManager.LoadScene ("Match_minigame",LoadSceneMode.Additive);
	}

	Quest FindQuestwithIndex (int questListsIndex){
		foreach (Quest qo in questsList) {
			if (qo.questNumber - 1 == questListsIndex) {
				return qo;
			} 
		}
		return null;
	}

	public void ShowCurrentQuest(){
		Quest quest = CheckCurrentQuest ();
		player.isPaused = true;
		QuestShowBox.SetActive (true);
		QuestShowBox.transform.TransformPoint(new Vector3((2*Screen.width) / 3, Screen.height / 2, 0));
		Text[] QuestTexts = QuestShowBox.GetComponentsInChildren<Text>();
		foreach (Text to in QuestTexts)
		{
			if (to.name == "MissionText")
			{
				to.text = quest.questName;
			}
			if(to.name == "DescriptionText")
			{
				to.text = quest.questDescription;
			}
		}
	}

	public void CloseCurrentQuestWindow(){
		player.isPaused = false;
		QuestShowBox.SetActive (false);
	}

	public void ShowStartQuest(){
		QBoxButton.interactable = false;
		Quest quest = CheckCurrentQuest ();
		player.isPaused = true;
		QuestShow.SetActive (true);
		Joystick.SetActive (false);
		QuestShow.transform.TransformPoint(new Vector3((2*Screen.width) / 3, Screen.height / 2, 0));
		Text[] QuestTexts = QuestShow.GetComponentsInChildren<Text>();
		foreach (Text to in QuestTexts)
		{
			if (to.name == "MissionText")
			{
				to.text = quest.questName;
			}
			if(to.name == "DescriptionText")
			{
				to.text = quest.questDescription;
			}
		}
	}

	public void CloseStartQuest(){
		QBoxButton.interactable = true;
		player.isPaused = false;
		QuestShow.SetActive (false);
		Joystick.SetActive (true);
	}


	public void StartQuest(){
		TaskStarted.SetActive (true);
		QuestShow.SetActive (false);
		//fazer switch com a escolha dos pais - botão ou pin		Default: botão
		Quest quest = CheckCurrentQuest();
		MissionName.text = quest.questName;
	}

	IEnumerator FadeIn(){
		anim.SetBool ("OnWait", false);
		anim.SetBool ("FadeIn", true);

		yield return new WaitUntil (() => FadeImage.color.a == 0);
		anim.SetBool ("OnWait", true);
	}

	IEnumerator FadeOut(){
		anim.SetBool ("OnWait", false);
		anim.SetBool ("FadeIn", false);

		yield return new WaitUntil (() => FadeImage.color.a == 1);
		anim.SetBool ("OnWait", true);
	}

	int Randomizer(List<int> listRandom)
	{
		int index = Random.Range (0, listRandom.Count);
		int random_value = listRandom [index];
		listRandom.RemoveAt (index);
		return random_value;
	}

	List<string> AlternativeReadFile(string File_name){
		string directory = null;

		if(Application.platform == RuntimePlatform.Android)
		{
			
			directory = "Lists\\" + File_name;
		}
		if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor){
			
			directory = "Lists/" + File_name;
		}

		TextAsset reader = Resources.Load<TextAsset>(directory); 

		List<string> arrayoflines = new List<string> ();

		string[] linesFromfile = reader.text.Split('\n');
		foreach (string line in linesFromfile)
		{
			arrayoflines.Add (line);
		}
		return arrayoflines;
	}

//-----------------------Legacy Functions-------------------------------

	List<string> ReadTextfile(string File_name)
	{
		string path = "Assets/Resources/Lists/" + File_name + ".txt";

		//Read the text directly from the txt file
		StreamReader reader = new StreamReader(path,System.Text.Encoding.GetEncoding("iso-8859-1")); 
		string line;
		List<string> arrayoflines = new List<string> ();
		while((line = reader.ReadLine ())!= null ) {
			arrayoflines.Add (line);
		}
		reader.Close ();
		return arrayoflines;
	}


	void SetButtonCurrentQuest(){
		Quest Current = CheckCurrentQuest ();
		int button_counter = 0, quest_counter = 0;
		foreach (GameObject go in QuestMarkers) {
			if ((go.name == Current.questObject + "_quest")) {
				break;
			}
			quest_counter++;
		}
		foreach (Button bo in InactiveButtons) {
			if ((bo.name == Current.questObject + "_Button")) {
				break;
			}
			button_counter++;
		}
		//GameObject.Find (Current.questObject).GetComponent<Activate_button> ().enabled = true;
		//print("Current obj name: " + Current.questObject);

		switch (Current.questObject) {																//Switch que verifica qual o objecto a que o jogador se deve deslocar e activa a tarefa.
		case "Fridge":
			GameObject.Find ("Fridge").GetComponent<Activate_button> ().enabled = true;
			break;
		case "Kitchen table":
			GameObject.Find ("Kitchen table").GetComponent<Activate_button> ().enabled = true;
			break;
		case "Shower":
			GameObject.Find ("Shower").GetComponent<Activate_button> ().enabled = true;
			break;
		case "Bathroom Sink":
			print ("Entrou bathroom sink");

			Activate_button test = GameObject.Find ("Bathroom Sink").GetComponent<Activate_button> ();
			print (test.isActiveAndEnabled);
			test.enabled = true;
			break;
		case "Kid Bedside Table":
			GameObject.Find ("Kid Bedside Table").GetComponent<Activate_button> ().enabled = true;
			break;
		case "Desk with laptop":
			GameObject.Find ("Desk with laptop").GetComponent<Activate_button> ().enabled = true;
			break;
		}
		if (!questsList [quest_counter].questCompleted) {
			if (InactiveButtons [button_counter].gameObject.activeSelf || (!InactiveButtons [button_counter].gameObject.activeSelf && QuestWindow.activeSelf)) {
				QuestMarkers [quest_counter].SetActive (false);
			} 
			if (!InactiveButtons [button_counter].gameObject.activeSelf && !QuestWindow.activeSelf) {
				QuestMarkers [quest_counter].SetActive (true);
			}
		}


	}
}
