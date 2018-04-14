using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScore : MonoBehaviour {

    [SerializeField]
    private Text pointsText;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        pointsText.text = "Rating: " + CheckInput.points;
	}
}
