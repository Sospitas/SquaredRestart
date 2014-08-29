using UnityEngine;
using System.Collections;

public class MobileInputs : Inputs
{

	// Use this for initialization
	void Start ()
	{
#if UNITY_IPHONE || UNITY_ANDROID
		this.enabled = true;
#else
		Destroy (GetComponent<MobileInputs>());
#endif
	}
	
	// Update is called once per frame
//	void Update ()
//	{
//		// Touch Inputs
//	}
}
