using UnityEngine;

public class ToyBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        slide();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        collidingObjects++;
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        collidingObjects--;
    }

    static float speed = 1.00f;
    int collidingObjects = 0;
    
    void slide()
    {
        if (collidingObjects == 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }
}
