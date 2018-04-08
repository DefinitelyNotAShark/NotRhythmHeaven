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
        if(this.setSprite.State == )//check if my state is equal to the ai state call
    }

    void CompareStatesBetweenPlayerAndAI()
    {

    }
	
}
