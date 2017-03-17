using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YearScrollController : MonoBehaviour {

	public GameObject weekInput;

	public GameObject[] weeks;
	public WeekInputController[] weekControllers;
	public bool[] activeWeeks;

	private RectTransform rt;


	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();

		weeks = new GameObject[52];
		weekControllers = new WeekInputController[52];
		activeWeeks = new bool[52];

		for (int i = 0; i < 52; i++) {
			GameObject go = Instantiate(weekInput, rt);
			RectTransform rect = go.GetComponent<RectTransform>();
			rect.anchoredPosition = new Vector2(i * 200 + 100, 0);

			WeekInputController wic = go.GetComponent<WeekInputController>();

			weeks[i] = go;
			weekControllers[i] = wic;
			activeWeeks[i] = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
