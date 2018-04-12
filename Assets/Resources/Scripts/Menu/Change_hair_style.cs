using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_hair_style : MonoBehaviour {

    Sprite sprite_hair1, sprite_hair2, sprite_hair3, sprite_hair4;
    public SpriteRenderer Player;
    int currentHair;
    // Use this for initialization
    void Awake () {
        
        currentHair = 1;
        sprite_hair1 = Resources.Load<Sprite>("Sprites/Characters/Boy/Boy_hair1_spritesheet");
        sprite_hair2 = Resources.Load<Sprite>("Sprites/Characters/Boy/Boy_hair2_spritesheet");
        sprite_hair3 = Resources.Load<Sprite>("Sprites/Characters/Boy/Boy_hair3_spritesheet");
       
        

    }
    public void changeHair_right()
    {
        
        if (currentHair < 3)
        {
            currentHair++;
        }
        else if (currentHair == 3)
        {
            currentHair = 1;
        }
        Change_hair(currentHair);
    }

    public void changeHair_left()
    {
        
        if (currentHair > 1)
        {
            currentHair--;
        }
        else if (currentHair == 1)
        {
            currentHair = 3;
        }
        Change_hair(currentHair);
    }

    void Change_hair(int counter)
    {
		if (PlayerPrefs.GetString ("Sexo") == "M") {
			switch (counter) {
			case 1:
				Player.sprite = sprite_hair1;
				PlayerPrefs.SetInt ("HairStyle", 1);
				break;
			case 2:
				Player.sprite = sprite_hair2;
				PlayerPrefs.SetInt ("HairStyle", 2);
				break;
			case 3:
				PlayerPrefs.SetInt ("HairStyle", 3);
				Player.sprite = sprite_hair3;
				break;

			}
		}
    }
    
}
