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
            } else
            {
                tryFeedingSleigh();
                tryThrowingToChimney();
            }
            tryInteractingWithBox();
            tryRope();
        }
    }
    
    static float speed = 20.00f;
    bool takingObject = false;
    GameObject carriedToy = null;
    GameObject carriedBox = null;

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
        carriedToy = null;
        float minDistance = 0;
        foreach (GameObject toy in toys)
        {
            float distance = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x - toy.gameObject.transform.position.x, 2) + Mathf.Pow(gameObject.transform.position.y - toy.gameObject.transform.position.y, 2));
            if (carriedToy == null || distance < minDistance)
            {
                minDistance = distance;
                carriedToy = toy;
            }
        }
        if (carriedToy != null && !takingObject)
        {
            ToyBehaviour toyBehaviour = carriedToy.GetComponent<ToyBehaviour>();
            if (toyBehaviour.playerInReach > 0)
            {
                toyBehaviour.takenByPlayer = true;
                takingObject = true;
            } else
            {
                carriedToy = null;
            }
        }
    }

    void tryInteractingWithBox()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("box");
        foreach (GameObject box in boxes)
        {
            BoxBehaviour boxBehavior = box.GetComponent<BoxBehaviour>();
            if (boxBehavior != null && boxBehavior.playerInReach)
            {
                if (!boxBehavior.isClosed && !boxBehavior.isClosing && takingObject && carriedToy != null)
                {
                    boxBehavior.packBox();
                    //boxBehavior.closeOrOpenBox(true);
                    takingObject = false;
                    Destroy(carriedToy);
                    carriedToy = null;
                } else if (boxBehavior.isClosed && !takingObject)
                {
                    takingObject = true;
                    carriedBox = box;
                    boxBehavior.takenByPlayer = true;
                }
            }
        }
    }

    void tryFeedingSleigh()
    {
        GameObject sleigh = GameObject.FindGameObjectWithTag("Sleigh");
        ActionnableBehaviour sleighBehaviour = sleigh.GetComponent<ActionnableBehaviour>();
        if (sleighBehaviour != null && sleighBehaviour.playerInReach && takingObject && carriedBox != null) {
            takingObject = false;
            Destroy(carriedBox);
            carriedBox = null;
        }
    }

    void tryRope()
    {
        GameObject rope = GameObject.FindGameObjectWithTag("Rope");
        ActionnableBehaviour ropeBehaviour = rope.GetComponent<ActionnableBehaviour>();
        if (carriedBox == null && ropeBehaviour != null && ropeBehaviour.playerInReach)
        {
            bool smallBoxPresent = false, mediumBoxPresent = false, bigBoxPresent = false;
            GameObject[] boxes = GameObject.FindGameObjectsWithTag("box");
            foreach(GameObject box in boxes)
            {
                BoxBehaviour boxBehaviour = box.GetComponent<BoxBehaviour>();
                if (boxBehaviour != null)
                {
                    switch (boxBehaviour.size)
                    {
                        case 1:
                            smallBoxPresent = true;
                            break;
                        case 2:
                            mediumBoxPresent = true;
                            break;
                        case 3:
                            bigBoxPresent = true;
                            break;
                    }
                }
            }
            if (!smallBoxPresent)
            {
                GameObject.Instantiate(Resources.Load("10_PREFABS/SmallBox"), BoxBehaviour.smallBoxPosition, new Quaternion());
            }
            if (!mediumBoxPresent)
            {
                GameObject.Instantiate(Resources.Load("10_PREFABS/MediumBox"), BoxBehaviour.mediumBoxPosition, new Quaternion());
            }
            if (!bigBoxPresent)
            {
                GameObject.Instantiate(Resources.Load("10_PREFABS/BigBox"), BoxBehaviour.bigBoxPosition, new Quaternion());
            }
        }
    }

    void tryThrowingToChimney()
    {
        GameObject fire = GameObject.FindGameObjectWithTag("Fire");
        ActionnableBehaviour fireBehaviour = fire.GetComponent<ActionnableBehaviour>();
        if (fireBehaviour != null && fireBehaviour.playerInReach)
        {
            if (carriedToy != null)
            {
                Destroy(carriedToy);
                carriedToy = null;
                takingObject = false;
            }
            if (carriedBox != null)
            {
                Destroy(carriedBox);
                carriedBox = null;
                takingObject = false;
            }
        }
    }
}
