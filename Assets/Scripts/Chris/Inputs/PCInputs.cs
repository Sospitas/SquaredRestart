using UnityEngine;
using System.Collections;

public class PCInputs : Inputs
{
	void Start ()
	{
#if UNITY_EDITOR || UNITY_STANDALONE
		this.enabled = true;
#else
		Destroy(GetComponent<PCInputs>());
#endif
	}

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A))
		{
			base.InputLeft();
		}
		else if(Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D))
		{
			base.InputRight ();
		}

		if(Input.GetKeyDown (KeyCode.Space))
		{
			base.FlipGravity();
		}
	}
}
