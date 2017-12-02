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
        ToyBehaviour toyBehaviour = null;
        float minDistance = 0;
        foreach (GameObject toy in toys)
        {
            float distance = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x - toy.gameObject.transform.position.x, 2) + Mathf.Pow(gameObject.transform.position.y - toy.gameObject.transform.position.y, 2));
            if (toyBehaviour == null || distance < minDistance)
            {
                minDistance = distance;
                toyBehaviour = toy.GetComponent<ToyBehaviour>();
            }
        }
        if (toyBehaviour != null && toyBehaviour.playerInReach > 0 && !takingObject)
        {
            toyBehaviour.takenByPlayer = true;
            takingObject = true;
        }
    }
}
