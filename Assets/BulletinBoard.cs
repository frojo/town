using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletinBoard : MonoBehaviour
{
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void EarlyUpdate()
    // {
    //     // VERY HACKY. FIX THIS ONCE DONE DEMOING TO FINN
    //     if (Input.GetButtonDown("interact")) {
    //             if (gameObject.activeSelf) {
    //                 Debug.Log("toggle toggle");
    //                 ToggleActive();
    //             }
	// 	}
        
    // }

    public void ToggleActive() {

        // this is hacky and fragile, i know
        // player.inputEnabled = !player.inputEnabled;
        player.bulletinboard = !player.bulletinboard;
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
