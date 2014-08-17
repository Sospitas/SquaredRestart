using UnityEngine;
using System.Collections;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public enum GameState
{
	MENU,
	IN_LEVEL,
	PAUSED,
	NONE
};

[System.Serializable]
public class GameManager
{
	private GameManager() {} // Private class constructor

	static GameManager _instance = new GameManager();

	public static GameManager Instance
	{
		get { return _instance; }
	}

	public int currentLevel = 1;

	static public string saveFileName = "savegame";

	private GameState _currentState = GameState.NONE;

	// Current Game State Value
	public GameState CurrentState
	{
		get
		{
			return _currentState;
		}
		set
		{
			_currentState = value;
		}
	}

	bool hasPerformedInitialValueSet = false;

	// This function will run once the first time the game is opened on the device.
	// It is used to set up initial values for the GameManager, such as the number of lives
	// and the amount of virtual currency that the player has.
	// It will not run at any point ever again unless the app is installed, as the value
	// of hasPerformedInitialValueSet will be set to true;
	void InitFromSettings(GameSettings settings)
	{
		if(hasPerformedInitialValueSet)
			return;

		// Set variables from the settings file

		// Disabled so that it can be reset while developing/testing
//		hasPerformedInitialValueSet = true;
	}

	public static bool SaveGameState()
	{
		bool saveSucceeded = true;

		string filename = Application.dataPath + "/" + saveFileName;
		FileStream stream = new FileStream(filename, FileMode.Create);

		try
		{
			BinaryFormatter serializer = new BinaryFormatter();
			serializer.Serialize(stream, Instance);
		}
		catch(SerializationException e)
		{
			Debug.Log ("Serialization failed. Error: " + e.Message);
			saveSucceeded = false;
		}
		finally
		{
			stream.Close();
		}

		return saveSucceeded;
	}

	static public bool LoadGameState()
	{
		bool loadSucceeded = false;

		string filename = Application.dataPath + "/" + saveFileName;
		if(File.Exists(filename))
		{
			loadSucceeded = true;
			FileStream stream = File.Open(filename, FileMode.Open);

			try
			{
				BinaryFormatter deserializer = new BinaryFormatter();
				_instance = (GameManager)deserializer.Deserialize(stream);
			}
			catch(SerializationException e)
			{
				Debug.Log ("Deserialization failed. Error: " + e.Message);
				loadSucceeded = false;
			}
			finally
			{
				stream.Close();
			}
		}

		return loadSucceeded;
	}

	public void SetState(GameState newState)
	{
		// Handle exiting of current/old state
		switch(CurrentState)
		{
		case GameState.MENU:
			break;
		case GameState.IN_LEVEL:
			break;
		case GameState.PAUSED:
			break;
		case GameState.NONE:
			break;
		default:
			break;
		}

		// Actually set the new state (set previous state too if needed)
		CurrentState = newState;

		// Handle entering the new state
		switch(CurrentState)
		{
		case GameState.MENU:
			break;
		case GameState.IN_LEVEL:
			break;
		case GameState.PAUSED:
			break;
		case GameState.NONE:
			break;
		default:
			break;
		}
	}
}