using UnityEngine;
using System.Collections;
using UnityEngineInternal;

public class BorderScript : MonoBehaviour 
{
    public GameObject LeftBorder, RightBorder, TopBorder, BottomBorder;
    private float offsetValue = 2.0f;  
    //When an object enters the border it will be moved to the other side of the screen
    //respectively.
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            if(this.tag == "LeftBorder")
            {
                float x = RightBorder.transform.position.x - RightBorder.collider2D.bounds.extents.x/2 - offsetValue;
                col.transform.position = new Vector2(x, col.transform.position.y);
            }
            else if(this.tag == "RightBorder")
            {
                float x = LeftBorder.transform.position.x + LeftBorder.collider2D.bounds.extents.x/2 + offsetValue;
                col.transform.position = new Vector2(x, col.transform.position.y);
            }
            else if(this.tag == "TopBorder")
            {
                float y = BottomBorder.transform.position.y + BottomBorder.collider2D.bounds.extents.y/2 + offsetValue;
                col.transform.position = new Vector2(col.transform.position.x, y);
            }
            else if(this.tag == "BottomBorder")
            {
                float y = TopBorder.transform.position.y - TopBorder.collider2D.bounds.extents.y/2 - offsetValue;
                col.transform.position = new Vector2(col.transform.position.x, y);
            }
        }
    }
}
