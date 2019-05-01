using UnityEngine;

public class Door : MonoBehaviour {

    public GameObject open;
    public GameObject closed;

    public bool isOpen = false;

	// Use this for initialization
	void Start () {
		if (isOpen)
        {
            Open();
        } else
        {
            Close();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Toggle()
    {
        if (isOpen)
        {
            Close();
        } else
        {
            Open();
        }
    }

    public void Open()
    {
        open.SetActive(true);
        closed.SetActive(false);
        isOpen = true;
    }

    public void Close()
    {
        closed.SetActive(true);
        open.SetActive(false);
        isOpen = false;
    }
}
