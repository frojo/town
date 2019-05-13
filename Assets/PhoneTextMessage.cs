using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTextMessage : MonoBehaviour
{

    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D o) {
        if (o.CompareTag("Player"))  {
            // display text message notifiaciton
            Debug.Log("text message!");
            player.phoneUnlocked = true;
        }
    }
}
