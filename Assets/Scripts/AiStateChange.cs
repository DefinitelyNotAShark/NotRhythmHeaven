using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateChange : MonoBehaviour
{
    //this script should be set to the parent of all ai game objects
    [SerializeField]
    GameObject AiGameObject1;

    [SerializeField]
    GameObject AiGameObject2;

    [SerializeField]
    GameObject AiGameObject3;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float timeToWait;

    [SerializeField]
    private float timeToReact;

    List<GameObject> AiGameObjects;

    SetSprite setSprite;
    float timeBetweenSpriteState;

    private void Awake()
    {
        AiGameObjects = new List<GameObject>();//makes a list of gameObjects for each ai object
    
        AiGameObjects.Add(AiGameObject1);//adds all ai Objects to list
        AiGameObjects.Add(AiGameObject2);
        AiGameObjects.Add(AiGameObject3);

        foreach(GameObject a in AiGameObjects)
        {
            a.gameObject.AddComponent<SetSprite>();//adds a set sprite script to each spriteObject
        }

        timeToReact = .5f;
        timeToWait = .5f;
    }

    private void Start()
    {
        StartCoroutine(tempCoroutineForTimeTesting());
    }

    IEnumerator tempCoroutineForTimeTesting()//this is the big function that will set all the moves for the game!
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(ChangeEachAIStateOverTime(.5f, SetSprite.SpriteState.pose1));
        yield return new WaitForSeconds(3);
        StartCoroutine(ChangeEachAIStateOverTime(.5f, SetSprite.SpriteState.pose2));
        yield return new WaitForSeconds(3);
        StartCoroutine(ChangeEachAIStateOverTime(.5f, SetSprite.SpriteState.pose3));
    }




    IEnumerator ChangeEachAIStateOverTime(float time, SetSprite.SpriteState state)
    {
        for (int i = 0; i < AiGameObjects.Count; i++)//for each sprite, change pose, then wait then change pose then wait
        {
            AiGameObjects[i].GetComponent<SetSprite>().State = state;
            yield return new WaitForSeconds(time);
            AiGameObjects[i].GetComponent<SetSprite>().State = SetSprite.SpriteState.normal;
        }
        StartCoroutine(BufferTimeBeforeReact(timeToReact, state));
    }

    IEnumerator BufferTimeBeforeReact(float timetoreact, SetSprite.SpriteState aiState)
    {
        yield return new WaitForSeconds(.2f);//this needs to be shorter
        StartCoroutine(TimeToReact(timetoreact, aiState));
    }

    IEnumerator TimeToReact(float timeforreact, SetSprite.SpriteState aiState)
    {
        SetSprite.AiState = aiState;
        spriteRenderer.color = Color.yellow;//this is the time you have to react!!!
        CheckInput.check = true;
        yield return new WaitForSeconds(timeforreact);
        CheckInput.check = false;
        spriteRenderer.color = Color.white;
    }
}
