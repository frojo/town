using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject {
	public string name;

	// for use in dialogue
	public Sprite portrait;
}
