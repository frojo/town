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
	}

	public override IEnumerator RunOptions(Yarn.Options optionsCollection, Yarn.OptionChooser optionChooser) {	
		yield return null;
	}

	public override IEnumerator RunCommand(Yarn.Command command) {
		yield return null;
	}

	public override IEnumerator DialogueComplete() {
		yield return null;
	}
}
