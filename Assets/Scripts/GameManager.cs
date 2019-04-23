using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour {

    public DialogueRunner dialogue;

    bool dialogueStarted = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!dialogueStarted && Input.anyKeyDown)
        {
            dialogueStarted = true;
            dialogue.StartDialogue();
        }
	}
}
