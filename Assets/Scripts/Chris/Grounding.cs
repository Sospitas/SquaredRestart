using UnityEngine;
using System.Collections;

public class Grounding : MonoBehaviour
{
	void Start()
	{
		Physics2D.IgnoreCollision(this.collider2D, this.transform.parent.collider2D);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("Colliding with: " + col.tag);

		if(col.tag == "Player")
			return;
		else if(col.tag == "Floor")
			this.transform.parent.GetComponent<MovementScript>()._isGrounded = true;
	}
}