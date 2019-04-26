using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour {

    public DialogueRunner dialogue;
    public PlayerController player;

    bool dialogueStarted = false;

    bool startedFirstCutscene = false;
    // bool inFirstCutscene = false;

    bool debug = false;

	// Use this for initialization
	void Start () {
        if (!debug)
        {
            player.inputEnabled = false;
            player.AnimatePassedOut();
            player.facingRight = false;
        }
        if (debug) player.inputEnabled = true;
        // start first cutscene
        // player is lying on the ground
        // coach is standing over him
        
	}
	
	// Update is called once per frame
	void Update () {
        // first scene
        // coach watches as player is dead on the ground

        if (!startedFirstCutscene && Input.GetButtonDown("interact") && !debug) {
            startedFirstCutscene = true;
            dialogue.StartDialogue();
        }

    }

    public void FinishedFirstCutscene()
    {
        player.inputEnabled = true;
    }


}
