using UnityEngine;
using System.Collections;

public class ExitLevel : MonoBehaviour 
{
    
	// Use this for initialization
	void Start () 
    {
	
	}

    //This will need have input connected to it so that they don't just go straight to the next level.
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameManager.Instance.loadNextLevel = true;               
        }
     }
}
