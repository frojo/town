using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class LoganController : MonoBehaviour {


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

    public IEnumerator AnimatePutOutCig()
    {
        anim.SetTrigger("put_out_cig");

        // wait for animator to get to the vom state
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("logan-put-out-cig"))
        {
            yield return null;
        }

        // wait for vom animation to end
        float waitTime = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator AnimatePullOutGun()
    {
        anim.SetTrigger("pull_out_gun");

        // wait for animator to get to the vom state
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("logan-pull-out-gun"))
        {
            yield return null;
        }

        // wait for vom animation to end
        float waitTime = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(waitTime);
    }
}

