using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerController : MonoBehaviour {

    public float runSpeed;
    public bool inputEnabled = false;

    // drawing
    public bool facingRight;
    Animator anim;
    SpriteRenderer spr;

    // physics
    Rigidbody2D rb2d;

   public DialogueRunner dialogue;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update() {

	}

    // we're using unity's physics "just" for collision detection
    private void FixedUpdate()
    {
        Vector2 v = Vector2.zero;
        if (inputEnabled)
        {
            // "raw" means only 1, 0, or -1. no ramping
            v.x = Input.GetAxisRaw("horizontal") * runSpeed;
            v.y = Input.GetAxisRaw("vertical") * runSpeed;
        }

        // move player
        rb2d.velocity = v;

        // draw player
        // nb: we don't change facingRight if v_x == 0
        if (v.x > 0) facingRight = true;
        else if (v.x < 0) facingRight = false;
        spr.flipX = !facingRight;

        bool moving = v.x != 0 || v.y != 0;
        anim.SetBool("moving", moving);
    }

    public void AnimatePassedOut()
    {
        Debug.Log("player passing out");
        anim.SetTrigger("passed_out_eyes_closed");
    }

    public void AnimatePassedOutEyesOpen()
    {
        anim.SetTrigger("passed_out_eyes_open");
    }

    public void AnimateIdle()
    {
        anim.SetTrigger("idle");
    }

    // still is different from idle. there's no "bounce"
    public void AnimateStill()
    {
        anim.SetTrigger("still");
    }

    public IEnumerator AnimateVom()
    {
        anim.SetTrigger("vom");

        // wait for animator to get to the vom state
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("protag-vom"))
        {
            yield return null;
        }

        // wait for vom animation to end
        float waitTime = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(waitTime);
    }




}
