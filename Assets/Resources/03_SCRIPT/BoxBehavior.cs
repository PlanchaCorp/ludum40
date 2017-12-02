using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string spriteName = GetComponent<SpriteRenderer>().sprite.name;
        if (spriteName.Contains("small"))
        {
            size = 1;
        } else if (spriteName.Contains("medium"))
        {
            size = 2;
        } else if (spriteName.Contains("big"))
        {
            size = 3;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "PlayerHand")
        {
            playerInReach = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "PlayerHand")
        {
            playerInReach = false;
        }
    }

    public bool playerInReach = false;
    public int size = 0;
    public bool isClosed = false;

    public void closeOrOpenBox(bool closing)
    {
        string boxState = (closing) ? "full" : "empty";
        switch (size)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("09_TEXTURE/" + "props_box_small_" + boxState);
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("09_TEXTURE/" + "props_box_medium_" + boxState);
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("09_TEXTURE/" + "props_box_big_" + boxState);
                break;
        }
    }
}
