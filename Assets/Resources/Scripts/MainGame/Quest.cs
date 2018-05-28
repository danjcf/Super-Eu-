using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour {
	
	public int questNumber;
	public string questName;
	public string questDescription;
	public string questPeriod;
	public string questObject;
	public bool questCompleted;
	public int questXP;
	public int questTime;
	public GameObject QuestMark;
	public GameObject QuestButton;
	public GameObject QuestStart;
	//private QuestManager theQM;
	public PlayerController player;

	void Start(){
		questCompleted = false;
		player = GameObject.Find ("Player").GetComponent<PlayerController> ();
		//theQM = GameObject.Find ("Quest Box").GetComponent<QuestManager> ();
	}


//----------------------------Legacy functions---------------------------------
	public void QuestTextButton()
	{
		QuestStart.SetActive (true);
		player.isPaused = true;
		QuestButton.gameObject.SetActive (false);												//Desativar botão para não aparecer no fundo
		QuestStart.transform.TransformPoint(new Vector3((2*Screen.width) / 3, Screen.height / 2, 0));
		Text[] QuestTexts = QuestStart.GetComponentsInChildren<Text>();
        foreach (Text to in QuestTexts)
        {
            if (to.name == "MissionText")
            {
                to.text = questName;
            }
            if(to.name == "DescriptionText")
            {
                to.text = questDescription;
            }
        }
        
	}
}
