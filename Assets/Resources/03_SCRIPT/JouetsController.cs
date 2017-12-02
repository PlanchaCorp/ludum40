using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JouetsController : MonoBehaviour {


    SpriteRenderer sp;
     void Start()
    {
        Debug.Log("start");

       // GameObject gameObject = Instantiate
        sp = this.gameObject.GetComponent<SpriteRenderer>();
        Sprite o = Resources.Load<Sprite>("09_TEXTURE/croque");

        sp.sprite = o;
        if (o == null) Debug.Log("Load failed");
    }
    private void Update()
    {
      //  sp.sprite = Sprite.Create("croque.png");
    }


}
