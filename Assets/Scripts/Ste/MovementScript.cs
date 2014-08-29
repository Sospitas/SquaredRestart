using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour 
{
	public Vector2 topVel;
    public float maxFallSpeed;
    private Vector2 velocity;
    private Vector2 targetVel;
    private bool speedUp;

	// CHRIS
	public bool _isGrounded = false;

	// Use this for initialization
	void Start () 
	{
		velocity = new Vector2(0.0f, 0.0f);
        targetVel = new Vector2(0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateInputs ();
//		_isGrounded = IsGrounded();

		if(Physics2D.gravity.y > 0)
		{
			this.transform.FindChild("BottomCollision").GetComponent<BoxCollider2D>().enabled = false;
			this.transform.FindChild("TopCollision").GetComponent<BoxCollider2D>().enabled = true;
		}
		else if(Physics2D.gravity.y < 0)
		{
			this.transform.FindChild("BottomCollision").GetComponent<BoxCollider2D>().enabled = true;
			this.transform.FindChild("TopCollision").GetComponent<BoxCollider2D>().enabled = false;
		}
	}

    void LateUpdate()
    {
        UpdateMovement();
        LimitFallVelocity();
    }

    void UpdateMovement()
    {
        if(speedUp)
        {
            targetVel = new Vector2(velocity.x, this.rigidbody2D.velocity.y);
            this.rigidbody2D.velocity = Vector2.Lerp(this.rigidbody2D.velocity, targetVel, 120 * Time.deltaTime);
        }
        else
        {
            targetVel = new Vector2(0.0f, this.rigidbody2D.velocity.y);
            this.rigidbody2D.velocity = Vector2.Lerp(this.rigidbody2D.velocity, targetVel, 2 * Time.deltaTime);
        }
    }

    void LimitFallVelocity()
    {
        if(this.rigidbody2D.velocity.y < -maxFallSpeed && Physics2D.gravity.y < 0)
        {
            this.rigidbody2D.velocity = new Vector2(this.rigidbody2D.velocity.x, -maxFallSpeed);
        }
        else if(this.rigidbody2D.velocity.y > maxFallSpeed && Physics2D.gravity.y > 0)
        {
            this.rigidbody2D.velocity = new Vector2(this.rigidbody2D.velocity.x, -maxFallSpeed);
        }
    }

	void UpdateInputs()
	{
        //These are the controls for PC
        //MoveRight
        if(Input.GetKey(KeyCode.D))
        {
            if(velocity.x < topVel.x)
            {
                velocity.x += 10 * Time.deltaTime;
                speedUp = true;
                if (velocity.x > topVel.x)
                {
                    velocity.x = topVel.x;
                }
            }
        }
        //Move Left
        if(Input.GetKey(KeyCode.A))
        {
            if(velocity.x > -topVel.x)
            {
                velocity.x -= 10 * Time.deltaTime;
                speedUp = true;
                if (velocity.x < -topVel.x)
                {
                    velocity.x = -topVel.x; 
                }
            }
        }
        //Move the velocity back to 0.
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            speedUp = false; 
            velocity.x = 0;
        }

//		if(Input.GetKeyDown(KeyCode.Space))
//		{
//			if(IsGrounded())
//			{
////				SwitchGravity2D();
//			}
//		}
    }

	// CHRIS
	bool IsGrounded()
	{
		RaycastHit2D hitLeft, hitRight;
		float halfPlayerHeight = transform.collider2D.bounds.extents.y;

		// If gravity == down
		if(Physics2D.gravity.y < 0)
		{
			Ray2D left = new Ray2D(new Vector2(transform.position.x - transform.collider2D.bounds.extents.x, transform.position.y - halfPlayerHeight), -Vector2.up);
			Ray2D right = new Ray2D(new Vector2(transform.position.x + transform.collider2D.bounds.extents.x, transform.position.y - halfPlayerHeight), -Vector2.up);

			Debug.DrawRay(left.origin, left.direction, Color.red, 0.1f);
			Debug.DrawRay (right.origin, right.direction, Color.red, 0.1f);

			hitLeft = Physics2D.Raycast(new Vector2(transform.position.x - transform.collider2D.bounds.extents.x, transform.position.y - halfPlayerHeight), -Vector2.up,
			                            0.1f);
			hitRight = Physics2D.Raycast(new Vector2(transform.position.x + transform.collider2D.bounds.extents.x, transform.position.y - halfPlayerHeight), -Vector2.up,
			                             0.1f);

			if(hitLeft)
				Debug.Log ("Left: " + hitLeft.transform.name);
			if(hitRight)
				Debug.Log ("Right: " + hitRight.transform.name);

			if(hitLeft || hitRight)
			{
				return true;
			}
		}
		// If gravity == up
		else if(Physics2D.gravity.y > 0)
		{
			hitLeft = Physics2D.Raycast (new Vector2(transform.position.x - transform.collider2D.bounds.extents.x, transform.position.y + halfPlayerHeight), Vector2.up,
			                             0.1f);
			hitRight = Physics2D.Raycast (new Vector2(transform.position.x + transform.collider2D.bounds.extents.x, transform.position.y + halfPlayerHeight), Vector2.up,
			                              0.1f);

			if(hitLeft)
				Debug.Log ("Left: " + hitLeft.transform.name);
			if(hitRight)
				Debug.Log ("Right: " + hitRight.transform.name);

			if(hitLeft || hitRight)
			{
				return true;
			}
		}

		return false;
	}

	public void SwitchGravity2D()
	{
		Debug.Log ("Switching Gravity");
		Physics2D.gravity = -Physics2D.gravity;
		StartCoroutine("FlipPlayer", 0.5f);
	}

	IEnumerator FlipPlayer(float flipDuration)
	{
		float startTime = Time.time;

		Quaternion startRotation = this.transform.rotation;
		Quaternion targetRotation = Quaternion.Euler(180, 0, 0) * this.transform.rotation;

		while(Time.time < startTime + flipDuration)
		{
			this.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, (Time.time - startTime)/flipDuration);
			yield return null;
		}

		this.transform.rotation = startRotation;
	}

	void OnGUI()
	{
		GUI.Label (new Rect(10, 10, 200, 100), Physics2D.gravity.y.ToString());

		GUI.Label (new Rect(110, 10, 200, 100), this.rigidbody2D.velocity.y.ToString());
	}
}
