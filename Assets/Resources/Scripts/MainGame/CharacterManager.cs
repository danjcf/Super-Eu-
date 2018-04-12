using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	//current level
	public int vLevel = 1;
	//current exp amount
	public int vCurrExp = 0;
	//exp amount needed for lvl 1
	public int vExpBase = 50;
	//exp amount left to next levelup
	public int vExpLeft = 50;
	//modifier that increases needed exp each level
	public float vExpMod = 1.15f;

	//leveling methods
	public void GainExp(int e)
	{
		vCurrExp += e;
		if(vCurrExp >= vExpLeft)
		{
			LvlUp();
		}
	}
	void LvlUp()
	{
		vCurrExp -= vExpLeft;
		vLevel++;
		float t = Mathf.Pow(vExpMod, vLevel);
		vExpLeft = (int)Mathf.Floor(vExpBase * t);
	}

}
