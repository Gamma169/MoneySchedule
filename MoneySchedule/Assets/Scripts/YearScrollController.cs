using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YearScrollController : MonoBehaviour {

	public GameObject weekInput;

	public GameObject[] weeks;
	public WeekInputController[] weekControllers;
	public bool[] activeWeeks;

	private RectTransform rt;

	private int yearAmount;
	private int numWeeks;

	private int weeklyAmount;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();

		weeks = new GameObject[52];
		weekControllers = new WeekInputController[52];
		activeWeeks = new bool[52];

		for (int i = 0; i < 52; i++) {
			GameObject go = Instantiate(weekInput, rt);
			RectTransform rect = go.GetComponent<RectTransform>();
			rect.anchoredPosition = new Vector2(i * 195 + 175, 16);

			WeekInputController wic = go.GetComponent<WeekInputController>();

			wic.weekNumber = i + 1;

			weeks[i] = go;
			weekControllers[i] = wic;
			activeWeeks[i] = true;
		}
	}
	
	// Update is called once per frame
	void Update () {

		UpdateActiveWeeks();

		if (numWeeks <= 0) {
			numWeeks = 1;
		}

		weeklyAmount = yearAmount / numWeeks;

		UpdateWeeklyYearlyVariance();

	}

	public void UpdateActiveWeeks() {
		numWeeks = 0;
		for (int i = 0; i < activeWeeks.Length; i++) {
			activeWeeks[i] = weekControllers[i].isActive;
			if (activeWeeks[i])
				numWeeks++;
		}
	}

	public void UpdateWeeklyYearlyVariance() {
		int currentYearVariance = 0;
		for (int i = 0; i < weekControllers.Length; i++) {
			int madeWeek = weekControllers[i].amountMadeThisWeek;
			if (weekControllers[i].isActive) 
				weekControllers[i].weeklyVariance = madeWeek - weeklyAmount;
			else 
				weekControllers[i].weeklyVariance = madeWeek;

			currentYearVariance += weekControllers[i].weeklyVariance;
			weekControllers[i].yearlyVariance = currentYearVariance;
		
		}
	}

	public void SetAmount(int newAmount) {
		yearAmount = newAmount;
	}
}
