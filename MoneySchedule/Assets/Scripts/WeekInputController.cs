using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeekInputController : MonoBehaviour {

	public int weekNumber;


	private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponentInChildren<Text>();

		text.text = "Week " + weekNumber;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
