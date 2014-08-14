using UnityEngine;
using System.Collections;

public enum GameState
{
	MENU,
	IN_LEVEL,
	PAUSED,
	NONE
};

public class GameManager : Singleton<GameManager>
{
	public static int numberOfLives;

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
	void PerformInitialValueSet()
	{
    	numberOfLives = 5;
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