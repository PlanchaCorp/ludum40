using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToysInformationPanel : MonoBehaviour {
    public Text text_size;
    public Text text_description;
    public Text text_title;
    public Image img_item;
    // Use this for initialization
    void Start () {
         text_title = transform.Find("txt_title_value").gameObject.GetComponent<Text>();
         text_size =  transform.Find("txt_size_value").gameObject.GetComponent<Text>();     
         text_description = gameObject.transform.Find("txt_description_value").gameObject.GetComponent<Text>();
         img_item = gameObject.transform.Find("img_item").gameObject.GetComponent<Image>();
         FillInformation("Croq\'", "Small", "il manque un morceau mais il est quand meme mangeable");
         
	}

	
	// Update is called once per frame
	void Update () {
		
	}

    private void FillInformation(string title,string size, string description)
    {
        text_title.text = title;
        text_size.text = size;
        text_description.text = description;
        
    }
}
