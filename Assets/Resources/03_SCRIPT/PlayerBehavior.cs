using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
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
    Animator animator = null;

    void move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            if (animator != null)
            {
                animator.SetBool("move", false);
            }
            if (moveHorizontal != 0 && moveVertical != 0)
            {
                moveHorizontal *= Mathf.Sqrt(2) / 2;
                moveVertical *= Mathf.Sqrt(2) / 2;
            }
            float deltaTime = Time.deltaTime;

            Vector2 moveVector = new Vector2(moveHorizontal * deltaTime * speed, moveVertical * deltaTime * speed);
            gameObject.transform.Translate(moveVector, Space.World);
        } else if (animator != null)
        {
            animator.SetBool("move", true);
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
        Debug.Log(carriedToy);
        if (carriedToy != null && !takingObject)
        {
            ToyBehaviour toyBehaviour = carriedToy.GetComponent<ToyBehaviour>();
            toyBehaviour.displayInfoEnabled = false;
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
                    Toy toy = carriedToy.GetComponent<ToyBehaviour>().toy;
                    if (toy.size == boxBehavior.size)
                    {
                        boxBehavior.toy = toy;
                        boxBehavior.packBox();
                        takingObject = false;
                        Destroy(carriedToy);
                        carriedToy = null;
                    }
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
            ScoreBehaviour score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreBehaviour>();
            BoxBehaviour box = carriedBox.GetComponent<BoxBehaviour>();
            if (box.toy.broken)
            {
                score.incrementScore(-100);
            } else
            {
                score.incrementScore(box.size * 100);
                AudioSource audio = sleigh.GetComponent<AudioSource>();
                if (audio != null)
                {
                    audio.Play();
                }
            }
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
            AudioSource audio = rope.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }
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
            Debug.Log("chimney");
            AudioSource audio = fire.GetComponent<AudioSource>();
            if (audio != null && (carriedToy != null || carriedBox != null))
            {
                audio.Play();
            }
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
