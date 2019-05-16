using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletinBoard : MonoBehaviour
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

    public void ToggleActive() {

        // this is hacky and fragile, i know
        player.inputEnabled = !player.inputEnabled;
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
