using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        // these built in axes have a ramp up to full
        float v_x = Input.GetAxis("horizontal") * walkSpeed;
        float v_y = Input.GetAxis("vertical") * walkSpeed;


        transform.Translate(v_x, v_y, 0);


        //}
		
	}
}
