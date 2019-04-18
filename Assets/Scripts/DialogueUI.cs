using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : Yarn.Unity.DialogueUIBehaviour {

    // UI element of the entire displayed dialogue
    public GameObject dialogueUIFrame;

    // UI element that displays lines
	public Text displayedText;

    // UI element that represents the character currently talking
    public Image currentCharacter;

    // pairs of (nameOfCharacter, characterPortraitSprite)
    // public Dictionary<string, Sprite> portraitSprites;
    public Sprite coachPortrait;
    public Sprite playerPortrait;

    // hacky. probaby want to do this differently later
    public Animator protagAnimator;

    bool lineFullyShown = false;
    bool newSegment = false;
    bool charsCounted = false;
    int numCharsShownSoFar = 0;
    string currentFullLine;

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

        currentFullLine = nameDialogue[1].TrimStart(' ');
        displayedText.text = currentFullLine;
 
        // These next few lines of bullshit let the player click through
        // the same line
        lineFullyShown = false;
        newSegment = true;
        charsCounted = false;
        numCharsShownSoFar = 0;
        while (!lineFullyShown)
        {
            if (newSegment) {
                // when we show a new segment, we need to wait a frame to be
                // able to count the characters displaued
                newSegment = false;
                yield return null;
            } else if (!charsCounted) {
                int numCharsVisibleNow = displayedText.cachedTextGenerator.characterCountVisible;
                numCharsShownSoFar += numCharsVisibleNow;
                charsCounted = true;
                if (numCharsShownSoFar == currentFullLine.Length) {
                    lineFullyShown = true;
                } else { // still haven't shown the full line
                    // wait for player input to show the next line
                    while (Input.anyKeyDown == false)
                    {
                        Debug.Log("waiting for user input to show rest of line");
                        yield return null;
                    }
                    charsCounted = false;
                    newSegment = true;
                    displayedText.text = currentFullLine.Substring(numCharsShownSoFar);
                }
            }
        }
        // if (displayedText.cachedTextGenerator.characterCountVisible != fullLine.Length)
        // {
            // line too long to fully display
            
        // }
      

        // Wait for any user input
        while ( Input.anyKeyDown == false) {
            Debug.Log("waiting for user input");
            yield return null;
        }
        Debug.Log("user input!");
        yield return null;
	}

	public override IEnumerator RunOptions(Yarn.Options optionsCollection, Yarn.OptionChooser optionChooser) {
        Debug.Log("run options!");
        yield return null;
	}

	public override IEnumerator RunCommand(Yarn.Command command) {
        Debug.Log("run command: " + command.text);
        if (command.text == "protag_get_up")
        {
            Debug.Log("animate protag getting up here");
            protagAnimator.SetTrigger("get_up");
            yield return new WaitForSeconds(
                protagAnimator.GetNextAnimatorStateInfo(0).length +
                protagAnimator.GetNextAnimatorStateInfo(0).normalizedTime);
        }
        // yield return null;
	}

    public override IEnumerator DialogueStarted()
    {
        Debug.Log("dialogue started");
        dialogueUIFrame.SetActive(true);
        yield return null;
    }

    public override IEnumerator DialogueComplete() {
        Debug.Log("dialogue completed");
        dialogueUIFrame.SetActive(false);
        yield return null;
	}
}
