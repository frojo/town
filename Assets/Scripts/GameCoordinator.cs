using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameCoordinator : MonoBehaviour {

    public DialogueRunner dialogue;
    public PlayerController player;
    public Camera cam;

    public Transform playerStart;
    public Transform camStart;

    public MenuController menu;

    public Animator shootAnim;
    bool inMenu = true;
    public bool startedFirstCutscene = false;


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
    }

    public void RestartLoop()
    {
        player.transform.position = playerStart.position;
        cam.transform.position = camStart.position;
        player.inputEnabled = false;
        player.AnimatePassedOut();
        player.facingRight = false;
        startedFirstCutscene = false;

        Debug.Log("restarting loop");

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

        // wait while the animation finishes (hacky)
        yield return new WaitForSeconds(4);
        
        shootAnim.gameObject.SetActive(false);

        RestartLoop();
    }



}
