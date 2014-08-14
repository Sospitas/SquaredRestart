using UnityEngine;
using System.Collections;

public class Menus : MonoBehaviour
{
	public int NumberOfLevels;

	public int LevelPage = 1;
	public int LevelsPerPage = 9;

	public int ButtonsPerRow = 3;

	private int NumberOfLevelPages;

	private delegate void GUIMethod();
	private GUIMethod currentGUIMethod;

	private bool[] LevelUnlockStatus;

	// Use this for initialization
	void Start ()
	{
		LevelUnlockStatus = new bool[NumberOfLevels];

		currentGUIMethod = MainMenuGUIMethod;

		NumberOfLevelPages = Mathf.FloorToInt(NumberOfLevels/LevelsPerPage);

		for(int i = 0; i < NumberOfLevels; i++)
		{
			if(string.Equals(SecurePlayerPrefs.GetString("Level" + i.ToString(), "Test1"), "true"))
			{
				LevelUnlockStatus[i] = true;
			}
			else
			{
				LevelUnlockStatus[i] = false;
			}
		}
	}

	void MainMenuGUIMethod()
	{
		if(GUI.Button (new Rect(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.8f, Screen.height * 0.2f), "Play"))
		{
			Application.LoadLevel ("Level1");
		}
		else if(GUI.Button (new Rect(Screen.width * 0.1f, Screen.height * 0.4f, Screen.width * 0.8f, Screen.height * 0.2f), "Level Select"))
		{
			currentGUIMethod = LevelSelectGUIMethod;
		}
		else if(GUI.Button (new Rect(Screen.width * 0.1f, Screen.height * 0.7f, Screen.width * 0.8f, Screen.height * 0.2f), "Quit"))
		{
			Application.Quit ();
		}

		if(GUI.Button (new Rect(Screen.width * 0.9f, 0.0f, Screen.width * 0.1f, Screen.height * 0.1f), "+ +"))
		{
			for(int i = 0; i < NumberOfLevels; i++)
			{
				LevelUnlockStatus[i] = true;
			}
		}
		else if(GUI.Button (new Rect(Screen.width * 0.9f, Screen.height * 0.1f, Screen.width * 0.1f, Screen.height * 0.1f), "- -"))
		{
			for(int i = 0; i < NumberOfLevels; i++)
			{
				LevelUnlockStatus[i] = false;
			}

			LevelUnlockStatus[0] = true;
		}
	}

	void LevelSelectGUIMethod()
	{
		float LeftPosition = 0.2f;
		float TopPosition = 0.2f;

		for(int i = 0; i < LevelsPerPage; i++)
		{
			int level = (i + 1) + ((LevelPage - 1) * LevelsPerPage);

			// [level - 1] to reset it back to array index from actual value
			if(LevelUnlockStatus[level - 1] == false)
			{
				GUI.enabled = false;
			}
				
			if(GUI.Button(new Rect(Screen.width * (LeftPosition * ((i % ButtonsPerRow) + 1)), Screen.height * TopPosition, Screen.width * 0.2f, Screen.height * 0.2f), level.ToString()))
			{
//				Debug.Log (level.ToString());
				Application.LoadLevel ("Level" + level);
			}

			GUI.enabled = true;

			if((i+1) % 3 == 0)
			{
				TopPosition += 0.2f;
			}
		}

		if(LevelPage > 1)
		{
			if(GUI.Button (new Rect(Screen.width * 0.05f, Screen.height * 0.85f, Screen.width * 0.1f, Screen.height * 0.05f), "<"))
			{
				LevelPage--;
			}
		}
		
		if(LevelPage < NumberOfLevelPages)
		{
			if(GUI.Button (new Rect(Screen.width * 0.85f, Screen.height * 0.85f, Screen.width * 0.1f, Screen.height * 0.05f), ">"))
			{
				LevelPage++;
			}
		}

		if(GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.05f, Screen.width * 0.05f, Screen.height * 0.05f), "Back"))
		{
			Application.LoadLevel("Menu");
		}
	}
	
	void OnGUI()
	{
		this.currentGUIMethod();
	}
}
