using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInput : MonoBehaviour
{
    public static bool check = false;

    private SetSprite setSprite;


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
    }

    void GetInput()
    {
        if(this.setSprite.State == SetSprite.SpriteState.normal)
        {
            Debug.Log("neutral");
        }
        else if (this.setSprite.State == SetSprite.AiState)
        {
            Debug.Log("CORRECT BUTTON");
        } //check if my state is equal to the ai state call
        else
            Debug.Log("WRONG BUTTON");
    }

    void CompareStatesBetweenPlayerAndAI()
    {

    }
	
}
