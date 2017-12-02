using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
            
	}
	
	// Update is called once per frame
	void Update () {
        move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!takingObject)
            {
                tryTakingToy();
            }
        }
    }
    
    static float speed = 20.00f;
    bool takingObject = false;

    void move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            if (moveHorizontal != 0 && moveVertical != 0)
            {
                moveHorizontal *= Mathf.Sqrt(2) / 2;
                moveVertical *= Mathf.Sqrt(2) / 2;
            }
            float deltaTime = Time.deltaTime;

            Vector2 moveVector = new Vector2(moveHorizontal * deltaTime * speed, moveVertical * deltaTime * speed);
            gameObject.transform.Translate(moveVector, Space.World);
        }
    }

    void tryTakingToy()
    {
        GameObject[] toys = GameObject.FindGameObjectsWithTag("toy");
        foreach (GameObject toy in toys)
        {
            ToyBehaviour toyBehaviour = toy.GetComponent<ToyBehaviour>();
            if (toyBehaviour != null && toyBehaviour.playerInReach && !takingObject)
            {
                toyBehaviour.takenByPlayer = true;
                takingObject = true;
            }
        }
    }
}
