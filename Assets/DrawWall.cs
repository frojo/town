using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWall : MonoBehaviour {

    // this only works for drawing behind or in front of protagonist
    // maybe there's a "in front of protag" in "behind protag" 

    // i think it would be a lot harder to figure out a system
    // for dynamically drawing walls behind/in front of 
    // arbitrary people in the world. Don't need that right now

    // hardcode player to be at z = 0 (bg/"floor" at 10 
    BoxCollider2D player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
        float newDepth;
        if (player.bounds.center.y > 
            GetComponent<BoxCollider2D>().bounds.center.y) {
            newDepth = -5;
        } else
        {
            newDepth = 5;
        }
        

        transform.position = new Vector3(transform.position.x,
                transform.position.y, newDepth);

    }


}
