using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : Yarn.Unity.DialogueUIBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override IEnumerator RunLine(Yarn.Line line) {
		yield return null;

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
