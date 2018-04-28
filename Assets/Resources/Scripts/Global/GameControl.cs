using UnityEngine;
using System.IO;
using System;

public class GameControl : MonoBehaviour {

	SaveData data;


	/// <summary>
	/// Gets a value indicating whether this <see cref="GameControl"/> data loaded.
	/// </summary>
	/// <value><c>true</c> if data loaded; otherwise, <c>false</c>.</value>
	public bool DataLoaded { get; private set; }

	/// <summary>
	/// Gets a value indicating whether this <see cref="GameControl"/> data saved.
	/// </summary>
	/// <value><c>true</c> if data saved; otherwise, <c>false</c>.</value>
	public bool DataSaved { get; private set; }


	/// <summary>
	/// Gets the data.
	/// </summary>
	/// <value>The data.</value>
	public SaveData Data { get { return data; } }




	void Awake () {
		DontDestroyOnLoad (gameObject);
	}

	public void LoadGame(){

		//Local Save
		string filepath = Path.Combine (Application.persistentDataPath, "SuperSave.json");

		if (File.Exists (filepath)) {
			//Load the content from storage into memory
			string allText = File.ReadAllText (filepath);

			data = JsonUtility.FromJson<SaveData> (allText);

			DataLoaded = true;
		} else {
			Debug.Log ("Não existe nenhuma gravação!!!");
			//Volta ao menu em que estava
		}

	
	}

	public bool SaveGame(){
		string filepath = Path.Combine (Application.persistentDataPath, "SuperSave.json");
		string replacepath = Path.Combine (Application.persistentDataPath, "SuperSaveBackup.json");
		Data.SaveDate = DateTime.Now.ToString();
		print (Data.SaveDate);
		if (!File.Exists (filepath)) {
			string json = JsonUtility.ToJson (data);
			File.WriteAllText (filepath, json);
		} else {
			//REVER ISTO
			if(File.Exists(replacepath))
			{
				File.Delete (replacepath);
			}
			File.Copy (filepath, replacepath);
			string json = JsonUtility.ToJson (data);
			File.WriteAllText (filepath, json);

		}
		return true;
	}

	public void CreateGame(){
		data = new SaveData ();
		SaveGame ();
	}

	public void DeleteGame(){
		string filepath = Path.Combine (Application.persistentDataPath, "SuperSave.json");
		if (File.Exists (filepath)) {
			File.Delete (filepath);
		}
		data = null;
	}
		
}
