using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour 
{

	public Vector2 topVel;
	private Vector2 velocity;
    private Vector2 targetVel;
    private bool speedUp;

	// CHRIS
	private bool _isGrounded = false;

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
	}

    void LateUpdate()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        if (speedUp)
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

	void UpdateInputs()
	{
        //These are the controls for PC
        //MoveRight
        if (Input.GetKey(KeyCode.D))
        {
			if(!CheckForWalls(false))
			{
	            if (velocity.x < topVel.x)
	            {
	                velocity.x += 10 * Time.deltaTime;
	                speedUp = true;
	                if (velocity.x > topVel.x)
	                {
	                    velocity.x = topVel.x;
	                }
	            }
			}
        }
        //Move Left
        if (Input.GetKey(KeyCode.A))
        {
			if(!CheckForWalls(true))
			{
	            if (velocity.x > -topVel.x)
	            {
	                velocity.x -= 10 * Time.deltaTime;
	                speedUp = true;
	                if (velocity.x < -topVel.x)
	                {
	                    velocity.x = -topVel.x; 
	                }
	            }
			}
        }
        //Move the velocity back to 0.
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            speedUp = false; 
            velocity.x = 0;
        }

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(IsGrounded())
			{
				SwitchGravity2D();
			}
		}
    }

	bool CheckForWalls(bool left)
	{
		Vector2 dir;
		Vector2 myPos;

		if(left)
		{
			dir = new Vector2(-1.0f, 0.0f);
			myPos = new Vector2(this.transform.position.x - this.collider2D.bounds.extents.x -0.05f, this.transform.position.y);
		}
		else 
		{
			dir = new Vector2(1.0f, 0.0f);
			myPos = new Vector2(this.transform.position.x + this.collider2D.bounds.extents.x +0.05f, this.transform.position.y);
		}
		
		RaycastHit2D hit = Physics2D.Raycast(myPos, dir - myPos, 0.1f);
		
		if(hit != null && hit.collider != null)
		{
			if(hit.collider.gameObject.tag == "Wall")
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		return false;
	}

	// CHRIS
	bool IsGrounded()
	{
		RaycastHit2D hitLeft, hitRight;

		// If gravity == down
		if(Physics2D.gravity.y < 0)
		{
			hitLeft = Physics2D.Raycast(new Vector2(transform.position.x - transform.collider2D.bounds.extents.x, transform.position.y), -Vector2.up,
			                            transform.collider2D.bounds.extents.y + 0.1f);
			hitRight = Physics2D.Raycast(new Vector2(transform.position.x + transform.collider2D.bounds.extents.x, transform.position.y), -Vector2.up,
			                             transform.collider2D.bounds.extents.y + 0.1f);

			if(hitLeft)
			{
				Debug.Log ("Hit Left - Down");
			}

			if(hitRight)
			{
				Debug.Log ("Hit Right - Down");
			}

			if(hitLeft || hitRight)
			{
				return true;
			}
		}
		else if(Physics2D.gravity.y > 0)
		{
			hitLeft = Physics2D.Raycast (new Vector2(transform.position.x - transform.collider2D.bounds.extents.x, transform.position.y), Vector2.up,
			                             transform.collider2D.bounds.extents.y + 0.1f);
			hitRight = Physics2D.Raycast (new Vector2(transform.position.x + transform.collider2D.bounds.extents.x, transform.position.y), Vector2.up,
			                              transform.collider2D.bounds.extents.y + 0.1f);

			if(hitLeft)
			{
				Debug.Log ("Hit Left - Up");
			}
			
			if(hitRight)
			{
				Debug.Log ("Hit Right - Up");
			}

			if(hitLeft || hitRight)
			{
				return true;
			}
		}

		return false;
	}

	void SwitchGravity2D()
	{
		Physics2D.gravity = -Physics2D.gravity;
	}

	void OnGUI()
	{
		GUI.Label (new Rect(10, 10, 200, 100), Physics2D.gravity.y.ToString());

		GUI.Label (new Rect(110, 10, 200, 100), this.rigidbody2D.velocity.y.ToString());
	}
}
