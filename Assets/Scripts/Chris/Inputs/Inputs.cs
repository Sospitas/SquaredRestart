using UnityEngine;
using System.Collections;

public class Inputs : MonoBehaviour
{
	public bool _isInputLocked = false;
	public float _movementSpeed = 10.0f;
	public bool _isGravityFlipped = false;
	public MovementScript _player;

	// Use this for initialization
	void Start ()
	{
		if(!_player)
			_isInputLocked = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_isInputLocked)
			return;
	}

	public void InputLeft()
	{
		// Set movement to -movementSpeed
	}

	public void InputRight()
	{
		// Set movement to +movementSpeed
	}

	public void FlipGravity()
	{
		_isGravityFlipped = !_isGravityFlipped;
	}

	public void EndLevel()
	{
		// Call the end level function
	}
}