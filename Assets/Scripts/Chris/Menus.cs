using UnityEngine;
using System.Collections;

public class Menus : MonoBehaviour
{
	public int NumberOfLevels;

	public int LevelPage = 1;
	public int LevelsPerPage = 9;

	private delegate void GUIMethod();
	private GUIMethod currentGUIMethod;

	// Use this for initialization
	void Start ()
	{
		currentGUIMethod = MainMenuGUIMethod;
	}

	void MainMenuGUIMethod()
	{
		if(GUI.Button (new Rect(10, Screen.height * 0.1f, Screen.width - 20, Screen.height * 0.2f), "Play"))
		{
			Application.LoadLevel ("Level1");
		}
		else if(GUI.Button (new Rect(10, Screen.height * 0.4f, Screen.width - 20, Screen.height * 0.2f), "Level Select"))
		{
			currentGUIMethod = LevelSelectGUIMethod;
		}
		else if(GUI.Button (new Rect(10, Screen.height * 0.7f, Screen.width - 20, Screen.height * 0.2f), "Quit"))
		{
			Application.Quit ();
		}
	}

	void LevelSelectGUIMethod()
	{
		for(int i = 1; i <= 3; i++)
		{
			for(int j = 1; j <= 3; j++)
			{
				if(GUI.Button (new Rect(Screen.width * (i * 0.2f), Screen.height * (j * 0.2f), Screen.width * 0.2f, Screen.height * 0.2f), 
				               "Level " + (((LevelPage - 1) * LevelsPerPage) + (i * j))))
				{
					Debug.Log (((LevelPage - 1) * LevelsPerPage) + (i * j).ToString());
				}
			}
		}
	}
	
	void OnGUI()
	{
		this.currentGUIMethod();
	}
}
