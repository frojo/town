using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed;
    public bool facingRight;

    public bool inputEnabled = false;

    public Animator animator;
    public SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        // take input and calc internal state
        float v_x = 0;
        float v_y = 0;
        if (inputEnabled)
        {
            // "raw" means only 1, 0, or -1. no ramping
            v_x = Input.GetAxisRaw("horizontal") * walkSpeed;
            v_y = Input.GetAxisRaw("vertical") * walkSpeed;

            // nb: we don't change facingRight if v_x == 0
            if (v_x > 0) facingRight = true;
            else if (v_x < 0) facingRight = false;

        }
        bool moving = v_x != 0 || v_y != 0;
        transform.Translate(v_x, v_y, 0);

        // draw player
        sprite.flipX = !facingRight;
        animator.SetBool("moving", moving);
	}

    public void AnimatePassedOut()
    {
        Debug.Log("player passing out");
        animator.SetTrigger("passed_out_eyes_closed");
    }

    public void AnimatePassedOutEyesOpen()
    {
        animator.SetTrigger("passed_out_eyes_open");
    }

    public void AnimateIdle()
    {
        animator.SetTrigger("idle");
    }

    public void AnimateVomit()
    {
        animator.SetTrigger("vomit");
    }




}
