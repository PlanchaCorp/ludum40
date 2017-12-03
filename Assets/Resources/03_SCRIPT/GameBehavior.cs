using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour {

    public int maxSpawnbound = 8;
    public int spawnrate = 3;
    // Use this for initialization
    public List<Toy> ToyList;
	void Start () {
        ToyList = new List<Toy>
        {
            new Toy("Croque monsieur","It's already bitten", 1, "toy_croq"),
            new Toy("Spinning top","Make it spin fast !", 1, "toupis"),
            new Toy("Bear","So cute !", 1, "ours"),
            new Toy("Console","Let's have a good time", 2, "console"),
            new Toy("Mommy's duck","How did this arrive here ?", 2, "toy_momysduck"),
            new Toy("Dovahkin","Fus Roh Dah !", 2, "dovahkin"),
            new Toy("Wooden horse","Wanna go for a ride ?", 3, "toy_horse")
        };
        InvokeRepeating("CreateToy", 0.0f, 2.5f);
    }
	
	// Update is called once per frame
	void Update () {
      
    }

    private int GetToyOnBelt()
    {
        GameObject belt = gameObject.transform.Find("belt").gameObject;
       return belt.transform.childCount;
    }

    void CreateToy()
    {
        if (GetToyOnBelt() <= maxSpawnbound) { 
        if (Mathf.Floor(Random.Range(0, spawnrate)) == 0) {
            int index = (int)Mathf.Floor(Random.Range(0, ToyList.Count));
            GameObject belt = gameObject.transform.Find("belt").gameObject;
            GameObject toyObject = GameObject.Instantiate(Resources.Load("10_PREFABS/toyGeneric"), belt.transform) as GameObject;
            Toy toy = ToyList[index];
            toyObject.GetComponent<ToyBehaviour>().toy = toy;
            toyObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("09_TEXTURE/" + toy.spriteName);
          //toyObject.GetComponent<SpriteRenderer>().transform.localScale.Set(0.7F, 0.7F, 1);
        }
    }
    }



}
