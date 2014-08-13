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
        //Move Left
        if (Input.GetKey(KeyCode.A))
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
        //Move the velocity back to 0.
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            speedUp = false; 
            velocity.x = 0;
        }
    }
}
