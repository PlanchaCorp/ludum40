using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TextMesh text = GetComponent<TextMesh>();
        if (text != null)
        {
            text.text = "Score : " + score;
        }
	}

    public int score = 0;

    public void incrementScore(int addition)
    {
        score += addition;
    }
}
