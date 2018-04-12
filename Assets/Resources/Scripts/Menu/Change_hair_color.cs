using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_hair_color : MonoBehaviour {

	public GameObject character;

	void On_Click (){
		SpriteRenderer char_color = character.GetComponent<SpriteRenderer> ();
		Color new_color = new Color(0.2F, 0.3F, 0.4F, 0.5F);
		/*
		new_color.r = 170;
		new_color.g = 100;
		new_color.b = 50;
		new_color.a = 255;*/
		char_color.color = new_color;
	}

}
