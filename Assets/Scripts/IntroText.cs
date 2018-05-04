using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour
{
    [SerializeField]
    Text spaceText;

    [SerializeField]
    Button startButton;

    Text introText;
    float input;
    bool coolDown = true;
    bool allowNotes = false;

    int inputNum = 0;

    AudioSource buttonClick;

    // Use this for initialization
    void Start()
    {
        introText = GetComponent<Text>();
        buttonClick = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetButtonUp("IntroAdvance"))
            input = 1;
  
        else
            input = 0;
  
        if (input > 0 && coolDown == true)
        {
            buttonClick.Play();
            coolDown = false;
            inputNum++;            
            ChangeText();
            StartCoroutine(Cooldown());
        }
    }

    void ChangeText()
    {
        switch (inputNum)
        {
            case 0:
                introText.text = "It's almost time to perform.";
                break;
            case 1:
                introText.text = "This is a special performance with all your friends.";
                break;
            case 2:
                allowNotes = true;
                introText.text = "Press the arrow keys to strike a pose.";
                break;
            case 3:
                introText.text = "Your friends will show you what dance moves you need to use.";
                break;
            case 4:
                introText.text = "Press start when you're ready!";
                spaceText.enabled = false;
                startButton.GetComponentInChildren<Text>().text = "Start";
                break;

        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        coolDown = true;
    }

    public void StartIsPressed()
    {
        Debug.Log("You clicked");
        PlayerStateChange.canDoFourthPose = true;
        SceneManager.LoadScene("GameScene");
    }
}
