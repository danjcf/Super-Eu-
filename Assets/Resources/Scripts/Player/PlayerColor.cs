using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour {

    Sprite Hair1, Hair2, Hair3;
    SpriteRenderer PlayerSprite;
    SwapColor ColorSwapper;
    // Use this for initialization
    void Start () {
        PlayerSprite = GetComponent<SpriteRenderer>();
        Hair1 = Resources.Load<Sprite>("Sprites/Characters/Boy/Boy_hair1_spritesheet");
        Hair2 = Resources.Load<Sprite>("Sprites/Characters/Boy/Boy_hair2_spritesheet");
        Hair3 = Resources.Load<Sprite>("Sprites/Characters/Boy/Boy_hair3_spritesheet");

        switch (PlayerPrefs.GetInt("HairStyle"))
        {
            case 1:
                PlayerSprite.sprite = Hair1;
                break;
            case 2:
                PlayerSprite.sprite = Hair2;
                break;
            case 3:
                PlayerSprite.sprite = Hair3;
                break;
        }
        SwapColor ColorSwapper = GetComponent<SwapColor>();
        ColorSwapper.SwapHairColor(PlayerPrefs.GetInt("HairColor"));
        ColorSwapper.SwapEyesColor(PlayerPrefs.GetInt("EyesColor"));
        ColorSwapper.SwapBodyColor(PlayerPrefs.GetInt("BodyColor"));
        ColorSwapper.SwapShirtColor(PlayerPrefs.GetInt("ShirtColor"));
        ColorSwapper.SwapPantsColor(PlayerPrefs.GetInt("PantsColor"));
        ColorSwapper.SwapShoesColor(PlayerPrefs.GetInt("ShoesColor"));
    }
	

}
