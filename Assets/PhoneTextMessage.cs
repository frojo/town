using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTextMessage : MonoBehaviour
{

    public PlayerController player;
    public PhoneNotificationIcon notification;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowNotifications()
    {
        notification.ShowNotification(1);
        yield return new WaitForSeconds(3);
        notification.ShowNotification(2);
    }

    void OnTriggerEnter2D(Collider2D o) {
        if (o.CompareTag("Player"))  {
            // display scripted text message notifications
            StartCoroutine(ShowNotifications());    
            player.phoneUnlocked = true;
        }

    }

    
}
