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


    public void SetInfobulle()
    {
        popInfo.transform.Find("txt_title").gameObject.GetComponent<TextMesh>().text = toy.name;
        popInfo.transform.Find("txt_size").gameObject.GetComponent<TextMesh>().text = toy.GetLabelSize();
        popInfo.transform.Find("txt_description").gameObject.GetComponent<TextMesh>().text = toy.description;
        SpriteRenderer sr = popInfo.transform.Find("small_image").gameObject.GetComponent<SpriteRenderer>();
        sr.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        sr.sprite = Resources.Load<Sprite>("09_TEXTURE/" + toy.spriteName);
    }

    public void DisplayInfo()
    {
        if (!popInfo.activeSelf)
        {
            popInfo.SetActive(true);
            SetInfobulle();
        }
       
       
    }
    public void HideInfo()
    {
        if (popInfo.activeSelf)
        {
            popInfo.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!takenByPlayer)
        {
            slide();
        } else
        {
            positionOverPlayer();
        }

        infoBulleBehaviour();
    }

    public bool displayInfoEnabled = false;


    public void infoBulleBehaviour()
    {
        if (displayInfoEnabled)
        {
            DisplayInfo();
        }
        else
        {
            HideInfo();
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "toy" || collider.tag == "wall")
        {
            collidingObjects++;
        } else if (collider.tag == "Player" || collider.tag == "PlayerHand")
        {
            if (!takenByPlayer)
            {
                displayInfoEnabled = true;
            }
            
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
            displayInfoEnabled = false;
            playerInReach--;
        }
        if (takenByPlayer)
        {
            HideInfo();
        }
    }

    static float speed = 2.10f;
    int collidingObjects = 0;
    public int playerInReach = 0;
    public bool takenByPlayer = false;
    public RaycastHit2D raycast;
    
    void slide()
    {
        raycast = Physics2D.Raycast(this.transform.Find("raycastStart").gameObject.transform.position, Vector2.left, 1.3f);

            if (raycast.collider != null && ( raycast.collider.name == "Wall" || raycast.collider.tag == "toy"))
            {

            } else
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
            
    }

    void positionOverPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
       
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -5f);
    }
}
