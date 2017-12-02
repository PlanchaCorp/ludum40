using UnityEngine;

public class ToyBehaviour : MonoBehaviour {

    public Toy toy;

    public Sprite sprite;
    public string spriteName;
    public void SetTexture(string name)
    {
        sprite = (Sprite)Resources.Load(name);
        this.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    GameObject popInfo;

    // Use this for initialization
    void Start () {
        popInfo = transform.Find("props_info").gameObject;
        
        popInfo.SetActive(false);

	}

    private void OnMouseOver()
    {
        popInfo.transform.Find("txt_title").gameObject.GetComponent<TextMesh>().text = toy.name;
        popInfo.transform.Find("txt_size").gameObject.GetComponent < TextMesh>().text = toy.GetLabelSize();
        popInfo.transform.Find("txt_description").gameObject.GetComponent<TextMesh>().text = toy.description;
        popInfo.SetActive(true);
    }
    private void OnMouseExit()
    {
        popInfo.SetActive(false);
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
        } else if (collider.tag == "Player" || collider.tag == "PlayerHand")
        {
            playerInReach++;
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
        else if (collider.tag == "Player" || collider.tag == "PlayerHand")
        {
            playerInReach--;
        }
    }

    static float speed = 1.00f;
    int collidingObjects = 0;
    public int playerInReach = 0;
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
