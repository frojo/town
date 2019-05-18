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

    public Animator shootAnim;
    public bool startedFirstCutscene = false;

    public bool debug = false;

	// Use this for initialization
	void Start () {
        if (!debug)
        {

            RestartGame();
        }
        if (debug)
        {
            startedFirstCutscene = true;
            player.inputEnabled = true;
        }

        
	}
	
	// Update is called once per frame
	void Update () {
        // first scene
        // coach watches as player is dead on the ground

        if (!startedFirstCutscene && Input.GetButtonDown("interact")) {
            startedFirstCutscene = true;
            Debug.Log("starting the first convo, fool");
            dialogue.StartDialogue("start");
        }
    }

    void RestartGame()
    {
        player.transform.position = playerStart.position;
        player.inputEnabled = false;
        player.AnimatePassedOut();
        player.facingRight = false;
        startedFirstCutscene = false;
    }

    public IEnumerator ShootAndRestart()
    {
        // play shooting animation
        shootAnim.gameObject.SetActive(true);
        shootAnim.SetTrigger("shoot");

        // wait while the animation finishes (hacky)
        yield return new WaitForSeconds(4);

        
        shootAnim.gameObject.SetActive(false);
        cam.transform.position = camStart.position;

        RestartGame();
    }



}
