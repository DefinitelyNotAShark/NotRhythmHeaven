using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePoseOverTime : MonoBehaviour {

    [SerializeField]
    private float timeToWait;

    [SerializeField]
    private float timeToReact;

    [SerializeField]
    private float timeBetweenAiChange;

    [SerializeField]
    private float timeBetweenAiAndPlayer;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(MainCoroutine());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator MainCoroutine()//this is the big function that will set all the moves for the game!
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(ChangeEachAIStateOverTime(timeBetweenAiChange, SetSprite.SpriteState.pose1));
        yield return new WaitForSeconds(3);
        StartCoroutine(ChangeEachAIStateOverTime(timeBetweenAiChange, SetSprite.SpriteState.pose2));
        yield return new WaitForSeconds(3);
        StartCoroutine(ChangeEachAIStateOverTime(timeBetweenAiChange, SetSprite.SpriteState.pose3));
    }

    IEnumerator ChangeEachAIStateOverTime(float timeBetweenStates, SetSprite.SpriteState state)
    {
        for (int i = 0; i < AiStateChange.AiGameObjects.Count; i++)//for each sprite, change pose, then wait then change pose then wait
        {
            AiStateChange.AiGameObjects[i].GetComponent<SetSprite>().State = state;
            yield return new WaitForSeconds(timeBetweenStates);
            AiStateChange.AiGameObjects[i].GetComponent<SetSprite>().State = SetSprite.SpriteState.normal;
        }
        StartCoroutine(TimeToReact(timeToReact, timeBetweenAiAndPlayer, state));
    }

    IEnumerator TimeToReact(float timeforreact, float timeBetweenAiAndPlayer ,SetSprite.SpriteState aiState)
    {
        SetSprite.AiState = aiState;
        yield return new WaitForSeconds(timeBetweenAiAndPlayer);//this is short, but it acts as kind of a buffer to get the rhythm right
        spriteRenderer.color = Color.yellow;//this is the time you have to react!!!
        CheckInput.check = true;
        yield return new WaitForSeconds(timeforreact);
        CheckInput.check = false;
        CheckInput.didPoints = false;
        spriteRenderer.color = Color.white;
    }
}
