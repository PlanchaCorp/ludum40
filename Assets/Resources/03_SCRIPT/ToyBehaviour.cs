using UnityEngine;

public class ToyBehaviour : MonoBehaviour {
    public Sprite sprite;
    public string spriteName;
    public void SetTexture(string name)
    {
        sprite = (Sprite)Resources.Load(name);
        this.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (!takenByPlayer)
        {
            slide();
        } else
        {
            positionOverPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "toy")
        {
            collidingObjects++;
        } else if (collider.tag == "Player")
        {
            playerInReach = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "toy")
        {
            collidingObjects--;
        }
        else if (collider.tag == "Conveyor")
        {
            collidingObjects++;
        }
        else if (collider.tag == "Player")
        {
            playerInReach = false;
        }
    }

    static float speed = 1.00f;
    int collidingObjects = 0;
    public bool playerInReach = false;
    public bool takenByPlayer = false;
    
    void slide()
    {
        if (collidingObjects == 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }

    void positionOverPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.position = player.transform.position;
    }
}
