using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : Yarn.Unity.DialogueUIBehaviour {
	
	// UI element that displays lines
	public Text displayedText;

	public override IEnumerator RunLine(Yarn.Line line) {
		Debug.Log(line.text);
		displayedText.text = line.text;

		// Wait for any user input
        while (Input.anyKeyDown == false) {
            yield return null;
        }
        // yield return null;
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
