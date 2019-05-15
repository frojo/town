using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneNotificationIcon : MonoBehaviour
{
    // pretty hacky but:
    // notification of 1 message
    public Sprite notification1;
    // notification of 2 messages
    public Sprite notification2;

    public Image shownNotification;

    public RectTransform shownEndPosition;
    public RectTransform hiddenStartPosition;
    // Transform goalPosition

    // start it hidden
    public bool showing = false;
    public float showSpeed = 10;
    public float hideSpeed = 3;
    public float notificationDuration = 10;

    // rectTransform of GameObject this script is attached to
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = transform.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (showing)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(
                rectTransform.anchoredPosition,
                shownEndPosition.anchoredPosition, showSpeed);
        } else
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(
                rectTransform.anchoredPosition,
                hiddenStartPosition.anchoredPosition, hideSpeed);
        }

    }

    public void ShowNotification(uint numMessages)
    {
        if (numMessages > 2 || numMessages < 1)
        {
            // i'll write a better error message later. sue me.
            Debug.Log("nah");
            numMessages = 1;
        }

        // set the correct notification
        if (numMessages == 1) shownNotification.sprite = notification1;
        else if (numMessages == 2) shownNotification.sprite = notification2;

        showing = true;

        // todo: set a timer to hide notification after x seconds
        StartCoroutine(HideAfterSeconds(notificationDuration));
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        showing = false;
    }
}
