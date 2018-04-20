using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInput : MonoBehaviour
{
    public static bool check = false;

    private SetSprite setSprite;

    public static int points;
    public static bool didPoints = false;
    public static int InputIsCorrect;//0 for if you didnt do anything or did 2 inputs//1 for correct//2 for incorrect
    public static bool restingAfterCorrect = false;//this makes sure it grades you as correct even if you dont hold down the button the entire time.

    private void Start()
    {
        setSprite = GetComponent<SetSprite>();
    }


    private void Update()
    {
        if (check)
        {
            GetInput();
        }
        else
        {
            //this is going to get u set back if u do things when it's not checking for input.
            GetIncorrectInput();
        }
    }

    private void GetIncorrectInput()
    {
        if (this.setSprite.State != SetSprite.SpriteState.normal)
        {
            
        }
    }

    void GetInput()
    {
        if (this.setSprite.State == SetSprite.SpriteState.normal)
        {
            Debug.Log("neutral");

            if(!restingAfterCorrect)//only false if you already didnt click the correct button
            InputIsCorrect = 0;
        }
        else if (this.setSprite.State == SetSprite.AiState)
        {
            if (!didPoints)
            {
                AddPoints();
                InputIsCorrect = 1;
            }
            restingAfterCorrect = true;
            Debug.Log("CORRECT BUTTON");

        } //check if my state is equal to the ai state call
        else
        {
            if (!didPoints)
            {
                RemovePoints();
                InputIsCorrect = 2;
            }

            Debug.Log("WRONG BUTTON");
        }
    }

    void AddPoints()
    {
        points++;
        didPoints = true;
    }

    void RemovePoints()
    {
        points--;
        didPoints = true;
    }

    
}
