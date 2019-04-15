using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : Yarn.Unity.DialogueUIBehaviour {
	
	// UI element that displays lines
	public Text displayedText;

    // UI element that represents the character currently talking
    public Image currentCharacter;

    // pairs of (nameOfCharacter, characterPortraitSprite)
    // public Dictionary<string, Sprite> portraitSprites;
    public Sprite coachPortrait;
    public Sprite playerPortrait;

	public override IEnumerator RunLine(Yarn.Line line) {
		Debug.Log(line.text);
		displayedText.text = line.text;

        string[] nameDialogue = line.text.Split(':');
        if (nameDialogue.Length != 2)
        {
            Debug.Log("Not properly formatted line (split)");
        }

        string name = nameDialogue[0];
        if (name == "Player")
        {
            currentCharacter.sprite = playerPortrait;
        }
        else if (name == "Coach")
        {
            currentCharacter.sprite = coachPortrait;
        }
        else
        {
            Debug.Log("Not properly formatted line (character name)");
        }

        string dialogue = nameDialogue[1];
        displayedText.text = dialogue.TrimStart(' ');
      

        // Wait for any user input
        while (Input.anyKeyDown == false) {
            yield return null;
        }
        yield return null;
	}

	public override IEnumerator RunOptions(Yarn.Options optionsCollection, Yarn.OptionChooser optionChooser) {
        Debug.Log("run options!");
        yield return null;
	}

	public override IEnumerator RunCommand(Yarn.Command command) {
        Debug.Log("run command: " + command.text);
        yield return null;
	}

    public override IEnumerator DialogueStarted()
    {
        Debug.Log("dialogue started");
        yield return null;
    }

    public override IEnumerator DialogueComplete() {
        Debug.Log("dialogue completed");
        yield return null;
	}
}
