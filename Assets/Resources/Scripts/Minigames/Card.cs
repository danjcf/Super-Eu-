using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    
    //public int numTurns;

	private Card thisCard;
	private CardManager _Manager;
	public bool isFlipped = false;
	public bool isMatched = false;
	public int CardNumber;

	
    // Use this for initialization
	void Awake () {
		thisCard = GetComponent<Card> ();
		_Manager = GameObject.Find("CardManager").GetComponent<CardManager>(); 
    }

	void Update(){
		if (isMatched == true) {
			GetComponent<Button> ().enabled = false;
		}
	}
	public void FlipCard()
	{
		if (!_Manager.isPaused) {
			if (GetComponent<Image> ().sprite.name.Equals ("Card_back")) {
			
				GetComponent<Image> ().sprite = _Manager.cardFace;
				_Manager.ShowImage (thisCard);

			}
			isFlipped = true;
			print ("Contador cartas viradas: " + _Manager.FlipCardCounter);
			if (_Manager.FlippedCards [0] == thisCard || _Manager.FlippedCards [1] == thisCard) {
				return;
			} else 
			{
				_Manager.FlippedCards [_Manager.FlipCardCounter] = thisCard;
				_Manager.FlipCardCounter++;
			}

		}

		if (_Manager.FlipCardCounter == 2) {
			print ("entrou if update");
			Time.timeScale = 0;
			StartCoroutine (_Manager.DelayCheck ());
			print ("Numero Flipcard: " + _Manager.FlipCardCounter);
			Time.timeScale = 1;
		}

	}

	public void ShowCardFace(){
		if (GetComponent<Image> ().sprite.name.Equals ("Card_back")) {

			GetComponent<Image> ().sprite = _Manager.cardFace;
			_Manager.ShowImage (thisCard);

		}
	}
		

	public void FlipBack(){											//PASSAR FLIPCARD DA CARTA FACE PARA A BACK
		

		if (GetComponent<Image> ().sprite.name.Equals ("Card_face")) {
			
			GetComponent<Image> ().sprite = _Manager.cardBack;
			_Manager.HideImage (thisCard);

		}	
	}
		


}

