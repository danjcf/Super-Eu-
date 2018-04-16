using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class QuestManager : MonoBehaviour {

    
	public Quest prefab;

    public bool[] questCompleted;
	private Quest[] questsList;
	private Text QuestBox;
	public Button[] InactiveButtons;
	public GameObject[] QuestMarkers;
	public GameObject QuestWindow;
	public PlayerController player;
	public bool inMinigame;

	// Use this for initialization
	void Awake () {
		inMinigame = false;
		QuestWindow.SetActive(false);
		QuestBox = GameObject.Find ("Quest Box").GetComponentInChildren<Text>();
		CreateListOfQuests ();
		PlayerPrefs.SetInt ("inMinigame", 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt("inMinigame") == 0) {
			SetButtonCurrentQuest ();
			print ("Quest Completed - " + PlayerPrefs.GetInt ("QuestCompleted"));
	
		}
		//if(SceneManager.activeSceneChanged

	}

	

	void CreateListOfQuests(){ 																				//função que lê o ficheiro com a lista de tarefas e transforma em quests (usando o prefab da quest)
		PlayerPrefs.SetInt("QuestCompleted", 0);
		List<string> quests_name = AlternativeReadFile("Lista de Tarefas");
		if (quests_name.Find (x => x.Contains ("Manhã")) != null) 											//Formato do ficheiro txt: Nome da quest - Descrição da quest - Duração da quest - Objecto onde vai estar a quest 
		{
			int morning_index = quests_name.FindIndex (x => x.Contains ("Manhã"));							//Index da palavra Manhã no txt, que representa a divisão das tarefas da manhã
			int lastIndex = quests_name.Count;
			int afternoon_index = quests_name.FindIndex (x => x.Contains ("Tarde"));
			string[] tasks = new string[lastIndex];
			quests_name.CopyTo (morning_index+1,tasks,0,lastIndex-1);			//Retira só as tarefas do periodo da manhã
			int tasks_afternoonIndex = afternoon_index-1;
			int counter = 0;
			int morningcounter = 1;
			int afternooncounter = 1;
			string[] foo = new string[4];
			questsList = new Quest[tasks.Length];
			char[] charsToTrim = {'\r'};
			while(counter<(tasks.Length-1))														//Ciclo para criar as quests, retira-se 1 ao tasks.length por causa da string "Tarde" a meio do ficheiro
			{
				
				if (counter < tasks_afternoonIndex && tasks [counter].Split ('\t').Length == 4) {
					Quest quest = (Instantiate (prefab) as Quest);												//Cria as várias quests
					quest.transform.SetParent (GameObject.Find ("Quest Box").transform);						//Define as quests como children do questmanager
					quest.questNumber = counter + 1;
					foo = tasks [counter].Split ('\t');													//Divide o txt através dos tabs e separa com o formato dito em cima
					foo[3] = foo [3].Trim (charsToTrim);
					quest.questName = foo [0];
					quest.questDescription = foo [1];
					quest.questPeriod = foo [2];
					quest.questObject = foo [3];
					quest.gameObject.name = "QuestMorning" + morningcounter;
					quest.gameObject.SetActive (false);
					questsList [counter] = quest;
					foreach (Button bo in InactiveButtons) {
						if (bo.name == (foo [3] + "_Button")) {                  
							bo.onClick.AddListener (quest.QuestTextButton);
						}
                        bo.gameObject.SetActive (false);
					}
					foreach (GameObject go in QuestMarkers) {
						go.SetActive (false);
					}
					morningcounter++;								
				}

				if(counter > tasks_afternoonIndex && tasks [counter].Split ('\t').Length == 1){					//ALTERAR PARA 4 QUANDO  O TXT TIVER COMPLETO
					Quest quest = (Instantiate (prefab) as Quest);												//Cria as várias quests
					quest.transform.SetParent (GameObject.Find ("Quest Box").transform);						//Define as quests como children do questmanager
					quest.questNumber = counter + 1;
					quest.questName = tasks [counter];

					quest.gameObject.name = "QuestAfternoon" + afternooncounter;
					quest.gameObject.SetActive (false);															//Desativa o objecto, só a primeira quest da lista fica activa
					questsList [counter] = quest;
					afternooncounter++;
				}

				counter++;
			}


		}

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
				break;
			case "Kid Bedside Table":
				break;
			case "Desk with laptop":
				break;
		}
		if (InactiveButtons [button_counter].gameObject.activeSelf || (!InactiveButtons [button_counter].gameObject.activeSelf && QuestWindow.activeSelf)) {
			QuestMarkers [quest_counter].SetActive (false);
		} 
		if(!InactiveButtons [button_counter].gameObject.activeSelf && !QuestWindow.activeSelf){
			QuestMarkers [quest_counter].SetActive (true);
		}
		
	
	}

	Quest CheckCurrentQuest()																			//Função que diz qual é a quest atual (quest a ser apresentada na GUI)
	{																									//Experimentar ver primeira quest da lista de quests que tenha isCompleted=false
		int quest_length = questsList.Length;
		int cycle_counter = 0;

		while(cycle_counter<quest_length)
		{
			if (PlayerPrefs.GetInt("QuestCompleted") == 0) {												//Verifica a primeira quest que não foi completada. Ativa essa quest, apresenta na GUI e sai do ciclo
				Quest CurrentQuest = questsList [cycle_counter];
				questsList [cycle_counter].gameObject.SetActive (true);
				QuestBox.text = "Próxima Missão:\n" + questsList[cycle_counter].questName;

				return CurrentQuest;
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
		switch(quest_started.questObject)
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

		case "Cenas":
			
			PlayerPrefs.SetString ("MemoryGameFolder", "Teeth");
			PlayerPrefs.SetInt ("HasSpritesheet", 1);
			PlayerPrefs.SetString ("MemoryGameSprites", "0\t1\t2\t3\t4");
			break;
		}
		QuestWindow.SetActive (false);
		player.isPaused = false;
		PlayerPrefs.SetInt ("inMinigame", 1);
		//PlayerPrefs.SetInt ("QuestCompleted", 1);
		//quest_started.QuestCompleted ();
		SceneManager.LoadScene ("Match_minigame",LoadSceneMode.Additive);


	}

	int Randomizer(List<int> listRandom)
	{
		int index = Random.Range (0, listRandom.Count);
		int random_value = listRandom [index];
		listRandom.RemoveAt (index);
		return random_value;
	}

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
}
