using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour {

    // Use this for initialization
    public List<string> ToyList;
	void Start () {
        ToyList = new List<string>();
        ToyList.Add("toy_croq");
        ToyList.Add("toy_horse");
        ToyList.Add("props_phone");
        ToyList.Add("toy_momysduck");
        InvokeRepeating("CreateToy", 2.0f, 3.0f);
    }
	
	// Update is called once per frame
	void Update () {
      
    }


    void CreateToy()
    {
       int index =(int) Mathf.Floor( Random.Range(0, ToyList.Count));
        

        GameObject toy = GameObject.Instantiate(Resources.Load("10_PREFABS/croque"), this.transform.position, this.transform.rotation) as GameObject;
        toy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("09_TEXTURE/"+ ToyList[index]);

    }

}
