using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour 
{

	public Vector2 topVel;
	private Vector2 velocity;
    private Vector2 targetVel;
    private bool speedUp;
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
			//This is a horrid code. There must be a better way of doing this. Unity...
			Debug.Log("Space");
			Vector2 dir;
			Vector2 myPos;
			if(Physics2D.gravity.y < 0)
			{
				dir = new Vector2(0.0f, -1.0f);
				myPos = new Vector2(this.transform.position.x, this.transform.position.y - this.collider2D.bounds.extents.y -0.05f);
			}
			else 
			{
				dir = new Vector2(0.0f, 1.0f);
				myPos = new Vector2(this.transform.position.x, this.transform.position.y + this.collider2D.bounds.extents.y + 0.05f);
			}

			RaycastHit2D hit = Physics2D.Raycast(myPos, dir - myPos, 0.1f);

			if(hit != null && hit.collider != null)
			{
				if(hit.collider.gameObject.tag == "Floor")
				{
					Physics2D.gravity = -Physics2D.gravity;
				}
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
}
