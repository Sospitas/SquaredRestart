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
        else if(this.rigidbody2D.velocity.y < maxFallSpeed && Physics2D.gravity.y > 0)
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

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(IsGrounded())
			{
				SwitchGravity2D();
			}
		}
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
