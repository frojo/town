using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectFonts : MonoBehaviour {

    public Font[] fonts;

	// Use this for initialization
	void Start () {
        foreach (Font font in fonts)
        {
            font.material.mainTexture.filterMode = FilterMode.Point;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
