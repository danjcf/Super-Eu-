using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour {

	public QuestManager theQM;


	private void OnTriggerStay2D(Collider2D target){
		
		Quest CurrentQ = theQM.CheckCurrentQuest ();
		if (target.name == "Player" && gameObject.name.Contains(CurrentQ.questObject)) {
			CurrentQ.QuestMark.SetActive (false);
			CurrentQ.QuestButton.SetActive (true);
			//GameObject.Find (CurrentQ.questObject).GetComponent<Activate_button> ().enabled = true;

		}
	}

	private void OnTriggerExit2D(Collider2D target){

		Quest CurrentQ = theQM.CheckCurrentQuest ();

		if (target.name == "Player" && gameObject.name.Contains(CurrentQ.questObject)) {
			//GameObject.Find (CurrentQ.questObject).GetComponent<Activate_button> ().enabled = false;
			CurrentQ.QuestButton.SetActive (false);
			CurrentQ.QuestMark.SetActive (true);

		}
	}
}
