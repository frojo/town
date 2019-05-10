using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : Yarn.Unity.DialogueUIBehaviour {

    public GameCoordinator gameCoordinator;

    // parent ui dialogue element
    public GameObject dialogueUIFrame;

    // box for the main dialogue
    public GameObject main;

    // boxes for the optoins
    public DialogueOptionsUI options;

    // UI element that displays lines
	public Text displayedText;

    // UI element that represents the character currently talking
    public Image currentCharacter;

    // unity can't display dictionaries in the inspector
    // so we keep a dict, but also a list to be edited in the inspector
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
            yield return null;
        }
        yield return null;
	}
    
	public override IEnumerator RunOptions(Yarn.Options optionsCollection, 
        Yarn.OptionChooser optionChooser) {
        options.gameObject.SetActive(true);

        // todo: make sure that the options aren't too big to fit in the box
        // i don't really know how to check this without actually blitting the
        // text (if i figure that out, change the convoluted logic in RunCommand)

        uint numOptions = (uint)optionsCollection.options.Count;
        if (numOptions > options.Count())
        {
            Debug.LogWarning("Too many options");
        }

        // display all needed options        
        for (int i = 0; i < options.Count(); i++)
        {
            DialogueOption option = options.GetOption(i);
            if (i < numOptions)
            {
                option.gameObject.SetActive(true);
                option.SetText(optionsCollection.options[i]);
            }
            else
            {
                option.gameObject.SetActive(false);
            }
        }

        // quick explanation of how cursor math works. bottom-most option is
        // option 0. the one above is option 1 and so on. so with 3 options
        // where the cursor is currently on the the top-most option:
        // -> [option 2]
        //    [option 1]
        //    [option 0]

        // enable and put cursor next to topmost option
        uint currCursorOption = numOptions - 1;
        options.PlaceCursor(currCursorOption);

        while (!Input.GetButtonDown("interact"))
        {
            // move cursor if we get an up or down input
            bool moveCursor = Input.GetButtonDown("vertical");
            if (moveCursor)
            {
                float updown = Input.GetAxisRaw("vertical");
                if (updown == 1)
                {
                    currCursorOption = (currCursorOption + 1) % numOptions;
                }
                else { // updown == -1 (can't be 0 if button was pressed)
                    currCursorOption = (currCursorOption - 1) % numOptions;
                }
                options.PlaceCursor(currCursorOption);
            }
            yield return null;
        }

        options.gameObject.SetActive(false);
        optionChooser((int)currCursorOption);
        yield return null;
	}

	public override IEnumerator RunCommand(Yarn.Command command) {
        string[] commandSplit = command.text.Split(' ');

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
        dialogueUIFrame.SetActive(true);
        main.SetActive(true);
        options.gameObject.SetActive(false);
        player.inputEnabled = false;
        yield return null;
    }

    public override IEnumerator DialogueComplete() {
        dialogueUIFrame.SetActive(false);
        player.inputEnabled = true;

        yield return null;
	}
}
