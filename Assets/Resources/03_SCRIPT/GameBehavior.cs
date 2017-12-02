using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour {

    // Use this for initialization
    public List<Toy> ToyList;
	void Start () {
        ToyList = new List<Toy>
        {
            new Toy("Croque monsieur","this sandwish is already bitted","small","toy_croq"),
            new Toy("Wooden horse","fun toy !","big","toy_horse"),
            new Toy("mommy's duck","how did this arrived here","","toy_momysduck")
        };
        InvokeRepeating("CreateToy", 2.0f, 3.0f);
    }
	
	// Update is called once per frame
	void Update () {
      
    }


    void CreateToy()
    {
       int index =(int) Mathf.Floor( Random.Range(0, ToyList.Count));
        

        GameObject toyObject = GameObject.Instantiate(Resources.Load("10_PREFABS/croque"), this.transform.position, this.transform.rotation) as GameObject;
        Toy toy = ToyList[index];
        toyObject.GetComponent<ToyBehaviour>().name = toy.name;
        toyObject.GetComponent<ToyBehaviour>().size = toy.size;
        toyObject.GetComponent<ToyBehaviour>().description = toy.description;
        toyObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("09_TEXTURE/"+ toy.spriteName);

    }

}
