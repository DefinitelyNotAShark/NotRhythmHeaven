using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEndText : MonoBehaviour {

    Text text;
    string responseText;
    string gradeText;
    AudioSource audioSource;

    [SerializeField]
    AudioClip feelDaBeat;

	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        GetResponseText();
        GetGradeText();
        text = GetComponent<Text>();
        StartCoroutine(WaitToShowText());
	}

    private void GetGradeText()
    {
        if (CheckInput.points <= 0)//if your score is 0 or negative
        {
            gradeText = "F";
        }
        else if (CheckInput.points > 0 && CheckInput.points <= 4)//if your score is 1 to 4
        {
            gradeText = "D";
        }
        else if (CheckInput.points > 4 && CheckInput.points <= 6)//if your score is 5 - 6
        {
            gradeText = "C";
        }
        else if (CheckInput.points > 6 && CheckInput.points <= 8)//if your score is 7 - 8
        {
            gradeText = "B";
        }
        else if (CheckInput.points > 8 && CheckInput.points <= 10)//if your score is 9 - 10
        {
            gradeText = "A";
        }
        else if (CheckInput.points > 8)//if your score is 11 - 12
        {
            gradeText = "A+";
        }
    }

    // Update is called once per frame
    void GetResponseText()
    {
        if (CheckInput.points <= 0)//if your score is 0 or negative
        {
            responseText = "You have no sense of rhythm.";
        }
        else if (CheckInput.points > 0 && CheckInput.points <= 4)//if your score is 1 to 4
        {
            responseText = "That was OK, I guess.";
        }
        else if (CheckInput.points > 4 && CheckInput.points <= 6)//if your score is 5 - 6
        {
            responseText = "You're a good dancer.";
        }
        else if (CheckInput.points > 6 && CheckInput.points <= 8)//if your score is 7 - 8
        {
            responseText = "You were very graceful.";
        }
        else if (CheckInput.points > 8 && CheckInput.points <= 10)//if your score is 9 - 10
        {
            responseText = "I think you could go pro.";
        }
        else if (CheckInput.points > 8 && CheckInput.points <= 10)//if your score is 11 - 12
        {
            responseText = "You were wonderful. You all were definitely in sync.";
        }
    }

    IEnumerator WaitToShowText()
    {
        yield return new WaitForSeconds(2);
        audioSource.Play();
        text.text = text.text + "\n\n" + responseText;
        yield return new WaitForSeconds(2);
        audioSource.Play();
        text.text = text.text + "\n\n" + gradeText;
        yield return new WaitForSeconds(1);
        audioSource.clip = feelDaBeat;
        audioSource.Play();
    }
}
