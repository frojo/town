using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOption : MonoBehaviour {

    public Text text;
    public Image cursor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetText(string s)
    {
        text.text = s;
    }
}
