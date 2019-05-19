using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public Sprite title;
    public Sprite credits;

    SpriteRenderer sprite;

    bool inTitleScreen = false;
    bool inCreditsScreen = false;

    public SpriteRenderer[] options;
    uint currCursorOption = 0;
    uint numOptions = 2;

    public GameCoordinator gameCoordinator;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inTitleScreen)
        {
            if (Input.GetButtonDown("interact"))
            {
                // cursor options are currently hardcoded
                if (currCursorOption == 0)
                {
                    // start game
                    gameCoordinator.RestartLoop();
                    CloseMenu();
                } else if (currCursorOption == 1)
                {
                    GoToCredits();
                }
            }
            // otherwise move cursor if player presses up or down
            else
            {
                bool moveCursor = Input.GetButtonDown("vertical");
                if (moveCursor)
                {
                    float updown = Input.GetAxisRaw("vertical");
                    if (updown == 1)
                    {
                        currCursorOption = (currCursorOption + 1) % numOptions;
                    }
                    else
                    { // updown == -1 (can't be 0 if button was pressed)
                        currCursorOption = (currCursorOption - 1) % numOptions;
                    }
                    PlaceCursor(currCursorOption);
                }
            }
            

        } else if (inCreditsScreen)
        {
            if (Input.GetButtonDown("interact") || Input.GetButtonDown("back"))
            {
                GoToTitle();
            }
        }
    }

    public void GoToTitle()
    {
        gameObject.SetActive(true);
        sprite.sprite = title;
        inTitleScreen = true;
        inCreditsScreen = false;
        PlaceCursor(currCursorOption);
    }

    public void CloseMenu()
    {
        inTitleScreen = false;
        inCreditsScreen = false;
        gameObject.SetActive(false);
    }

    void GoToCredits()
    {
        sprite.sprite = credits;
        inTitleScreen = false;
        inCreditsScreen = true;
        ClearCursor();
    }

    void PlaceCursor(uint optionNum)
    {
        if (optionNum >= numOptions)
        {
            Debug.LogWarning("error: options index out of range");
        }

        // todo: make this more performant (would probably need to 
        // have a MoveCursor())
        for (int i = 0; i < options.Length; i++)
        {
            if (i == optionNum)
            {
                options[i].gameObject.SetActive(true);
            }
            else
            {
                options[i].gameObject.SetActive(false);
            }
        }

    }

    void ClearCursor()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].gameObject.SetActive(false);
        }
    }




}
