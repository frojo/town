using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


// WARNING: THIS CLASS IS PRETTY MISNAMED. IT REALLY ONLY CONTROLS THE
// ANIMATION FOR LOGAN. PLAN IS TO REFACTOR IT MAYBE
public class ActorAnimationController : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
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

