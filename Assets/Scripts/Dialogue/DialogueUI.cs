﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : Yarn.Unity.DialogueUIBehaviour {

    public GameManager gameManager;

    // UI element of the entire displayed dialogue
    public GameObject dialogueUIFrame;

    // UI element that displays lines
	public Text displayedText;

    // UI element that represents the character currently talking
    public Image currentCharacter;

    public Sprite coachPortrait;
    public Sprite playerPortrait;

    public Character[] characters;
    Dictionary<string, Character> charDict = new Dictionary<string, Character>();

    // hacky. probaby want to do this differently later
    public PlayerController player;

    bool lineFullyShown = false;
    bool newSegment = false;
    bool charsCounted = false;
    int numCharsShownSoFar = 0;
    string currentFullLine;

    void Start() {
        foreach (Character character in characters) {
            charDict.Add(character.name, character);
        }
    }

	public override IEnumerator RunLine(Yarn.Line line) {
		Debug.Log(line.text);
        dialogueUIFrame.SetActive(true);
        displayedText.text = line.text;

        string[] nameLine = line.text.Split(':');
        if (nameLine.Length != 2)
        {
            Debug.Log("Not properly formatted line (split)");
        }

        string name = nameLine[0];
        currentCharacter.sprite = charDict[name].portrait;

        currentFullLine = nameLine[1].TrimStart(' ');
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
                    while (!Input.GetButtonDown("interact"))
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

      

        // wait for user input to display next line
        while (!Input.GetButtonDown("interact")) {
            Debug.Log("waiting for user input to show next line");
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

        string[] commandSplit = command.text.Split(' ');
        Debug.Log("first split : " + commandSplit[0]);

        if (commandSplit[0] == "hideui")
        {
            dialogueUIFrame.SetActive(false);
            yield return null;
        }
        else if (commandSplit[0] == "wait")
        {
            float seconds = 0.0f;
            float.TryParse(commandSplit[1], out seconds);
            yield return new WaitForSeconds(seconds);
        }
        else if (command.text == "protag_passed_out_eyes_closed")
        {
            player.AnimatePassedOut();
            yield return null;
        }
        else if (command.text == "protag_passed_out_eyes_open")
        {
            player.AnimatePassedOutEyesOpen();
            yield return null;
        }
        else if (command.text == "protag_idle")
        {
            player.AnimateIdle();
            yield return null;
        }
        else if (command.text == "protag_still")
        {
            player.AnimateStill();
            yield return null;
        }
        else if (command.text == "protag_vom")
        {
            yield return player.AnimateVom();
        }
    }

    public override IEnumerator DialogueStarted()
    {
        Debug.Log("dialogue started!");
        dialogueUIFrame.SetActive(true);
        yield return null;
    }

    public override IEnumerator DialogueComplete() {
        Debug.Log("dialogue completed");
        dialogueUIFrame.SetActive(false);
        gameManager.FinishedFirstCutscene();

        yield return null;
	}
}
