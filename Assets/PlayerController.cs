using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed;
    bool facingRight;

    public Animator animator;
    public SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        // "raw" means only 1, 0, or -1. no ramping
        float v_x = Input.GetAxisRaw("horizontal") * walkSpeed;
        float v_y = Input.GetAxisRaw("vertical") * walkSpeed;

        // nb: we don't change facingRight if v_x == 0
        if (v_x > 0) facingRight = true;
        else if (v_x < 0) facingRight = false;
        sprite.flipX = !facingRight;

        bool moving = v_x != 0 || v_y != 0;
        animator.SetBool("moving", moving);

        // transform.Translate(v_x, v_y, 0);
	}
}
