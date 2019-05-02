﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
    
    // can assign in inspector to any function
    public UnityEvent triggeredEvent;

	// sign for instructions to display for interacting
	public GameObject sign;

	// Use this for initialization
	void Start () {
		sign.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D o) {
		if (o.CompareTag("Player")) {
            Debug.Log("display!");
			sign.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D o) {
		if (o.CompareTag("Player")) {
            Debug.Log("hide!");
            sign.SetActive(false);
		}
	}
	
	void OnTriggerStay2D(Collider2D o) {
		if (o.CompareTag("Player")) {
			if (Input.GetButtonDown("interact")) {
                triggeredEvent.Invoke();
			}
		}
	}
	
}
