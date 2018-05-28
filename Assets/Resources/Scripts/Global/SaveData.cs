using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData {

	//Data of save
	public string SaveDate;

	//Account info
	public string email;
	public string pass;
	public string pin;

	//Player personal info
	public string playerName;
	public string age;
	public char sex;

	//Player Character info
		//Character colors
	public int Hairstyle;
	public int Hair;
	public int Skin;
	public int Eyes;
	public int Shirt;
	public int Pants;
	public int Shoes;

		//Player Level
	public int level;
	public int power_level;
	public int experience;
	public int XpLeft;

		//Player last position
	public Vector3 playerPos;
	public Quaternion playerRotation;

	//Quests
	//public List<Quest> TotalTasks;
	public int CurrentTaskNumber;
	public bool DayFinished;
	public bool SleepMode;

	//Statistics
	public Quest[] TotalTasksCompleted;
	public Quest[] TasksFailed;

	//Rewards
	public List<string> ChosenRewardNames;

	//Match Minigame
	public int MatchGamePlayTimes;
	public int MatchGameHighScore;
	public int MatchGameBestTime;

	public string hashOfContents;
}
