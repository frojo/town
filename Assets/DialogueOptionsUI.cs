using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// manipulating/displaying dialogue options is complicated enough that

public class DialogueOptionsUI : MonoBehaviour {

    
    public DialogueOption[] options;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int Count()
    {
        return options.Length;
    }

    public DialogueOption GetOption(int i)
    {
        if (i > options.Length)
        {
            Debug.LogWarning("error: options index out of range");
        }
        return options[i];
    }

    public void PlaceCursor(uint optionNum)
    {
        if (optionNum > options.Length)
        {
            Debug.LogWarning("error: options index out of range");
        }

        // todo: make this more performant (would probably need to 
        // have a MoveCursor())
        for (int i = 0; i < options.Length; i++)
        {
            if (i == optionNum)
            {
                options[i].cursor.gameObject.SetActive(true);
            } else
            {
                options[i].cursor.gameObject.SetActive(false);
            }
        }

    }
}
