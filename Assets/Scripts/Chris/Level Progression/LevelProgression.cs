using UnityEngine;
using System.Collections;

public class LevelProgression : Singleton<LevelProgression>
{
	public bool GetLevelUnlockStatus(int levelNumber)
	{
		if(string.Equals(SecurePlayerPrefs.GetString("Level"+levelNumber.ToString(), "Test1"), "true"))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}