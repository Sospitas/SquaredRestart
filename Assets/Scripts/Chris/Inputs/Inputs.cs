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
//		_player = this.GetComponent<MovementScript>();

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
		if(_player._isGrounded)
		{
			_isGravityFlipped = !_isGravityFlipped;
			_player.SwitchGravity2D();
			_player._isGrounded = false;
		}
	}

	public void EndLevel()
	{
		// Call the end level function
		if(GameManager.Instance.loadNextLevel)
			GameManager.Instance.LoadNextLevel();
	}
}