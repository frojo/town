using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameCoordinator : MonoBehaviour {

    public DialogueRunner dialogue;
    public DialogueVariableStorage dialogueVars;

    public PlayerController player;
    public ActorAnimationController logan;
    public Camera cam;

    public Transform playerStart;
    public Transform camStart;

    public MenuController menu;

    public Animator shootAnim;
    public Animator endAnim;

    bool inMenu = true;
    public bool startedFirstCutscene = false;
    public bool inEndCredits = false;


    public bool debug = false;


	// Use this for initialization
	void Start () {

        if (debug)
        {
            startedFirstCutscene = true;
            player.inputEnabled = true;
        } else {
            inMenu = true;
            menu.GoToTitle();
        }
	}
	
	// Update is called once per frame
	void Update () {

        // honestly don't think there's a less hacky way of starting the game
        if (!inMenu && !startedFirstCutscene && Input.GetButtonDown("interact")) {
            startedFirstCutscene = true;
            dialogue.StartDialogue("start");
        }
        else if (inEndCredits) {
            if (Input.GetButtonDown("interact")) {
                endAnim.gameObject.SetActive(false);
                inEndCredits = false;
                menu.GoToTitle();
            }
        }
    }

    public void RestartLoop()
    {
        player.transform.position = playerStart.position;
        cam.transform.position = camStart.position;
        player.inputEnabled = false;
        player.AnimatePassedOut();
        player.facingRight = false;
        startedFirstCutscene = false;
        dialogueVars.ResetToDefaults();

        // hacky. should find better way of dealing with this
        logan.AnimateSmoking();

        StartCoroutine(WaitAFrameThenMarkInMenuFalse());
    }

    public IEnumerator WaitAFrameThenMarkInMenuFalse()
    {
        // this was entirely added because i don't want initial convo
        // to happen until player presses x
        yield return new WaitForEndOfFrame();
        inMenu = false;
    }

    public IEnumerator ShootAndLoop()
    {
        // play shooting animation
        shootAnim.gameObject.SetActive(true);
        shootAnim.SetTrigger("shoot");

        // wait while the animation finishes (hacky)
        yield return new WaitForSeconds(4);
        
        shootAnim.gameObject.SetActive(false);

        RestartLoop();
    }

    public IEnumerator Win()
    {
        // play shooting animation
        endAnim.gameObject.SetActive(true);
        endAnim.SetTrigger("end");

        // wait while the animation finishes (hacky)
        yield return new WaitForSeconds(4);

        player.inputEnabled = false;
        // let player exit back to the title screen
        inEndCredits = true;
    }



}
