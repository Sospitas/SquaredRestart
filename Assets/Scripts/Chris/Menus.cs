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

	// Use this for initialization
	void Start ()
	{
		currentGUIMethod = MainMenuGUIMethod;

		NumberOfLevelPages = Mathf.FloorToInt(NumberOfLevels/LevelsPerPage);
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
	}

	void LevelSelectGUIMethod()
	{
		float LeftPosition = 0.2f;
		float TopPosition = 0.2f;

		for(int i = 0; i < LevelsPerPage; i++)
		{
			int level = (i + 1) + ((LevelPage - 1) * LevelsPerPage);
			if(GUI.Button(new Rect(Screen.width * (LeftPosition * ((i % ButtonsPerRow) + 1)), Screen.height * TopPosition, Screen.width * 0.2f, Screen.height * 0.2f), level.ToString()))
			{
//				Debug.Log (level.ToString());
				Application.LoadLevel ("Level" + level);
			}

			if((i+1) % 3 == 0)
			{
				TopPosition += 0.2f;
			}
		}

		if(LevelPage > 1)
		{
			if(GUI.Button (new Rect(Screen.width * 0.1f, Screen.height * 0.85f, Screen.width * 0.1f, Screen.height * 0.1f), "<"))
			{
				LevelPage--;
			}
		}
		
		if(LevelPage < NumberOfLevelPages)
		{
			if(GUI.Button (new Rect(Screen.width * 0.8f, Screen.height * 0.85f, Screen.width * 0.1f, Screen.height * 0.1f), ">"))
			{
				LevelPage++;
			}
		}
	}
	
	void OnGUI()
	{
		this.currentGUIMethod();
	}
}
