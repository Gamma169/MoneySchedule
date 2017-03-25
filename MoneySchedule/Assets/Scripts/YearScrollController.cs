using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YearScrollController : MonoBehaviour {

	public GameObject weekInput;
	public GameObject[] quarterHolders;

	public GameObject[] weeks;
	public WeekInputController[] weekControllers;
	public bool[] activeWeeks;

	private RectTransform rt;

	private int yearAmount;
	private int numWeeks;

	private int weeklyAmount;

	private bool[] quarterExpanded;
	private bool[] quarterExpanding;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();

		weeks = new GameObject[52];
		weekControllers = new WeekInputController[52];
		activeWeeks = new bool[52];

		quarterExpanded = new bool[4] {true, true, true, true};
		quarterExpanding = new bool[4];

		//OriginalSetup();
		QuarterSetup();
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

	private void OriginalSetup() {
		for (int i = 0; i < 52; i++) {
			GameObject go = Instantiate(weekInput, rt);
			RectTransform rect = go.GetComponent<RectTransform>();
			rect.anchoredPosition = new Vector2(i * 195 + 225, 16);

			WeekInputController wic = go.GetComponent<WeekInputController>();

			wic.weekNumber = i + 1;

			weeks[i] = go;
			weekControllers[i] = wic;
			activeWeeks[i] = true;
		}
	}

	private void QuarterSetup() {
		for (int i = 0; i < 52; i++) {
			RectTransform quarterRT = quarterHolders[i / 13].GetComponent<RectTransform>();
			GameObject go = Instantiate(weekInput, quarterRT);
			RectTransform rect = go.GetComponent<RectTransform>();

			//if (i / 13 == 0)
			//	rect.anchoredPosition = new Vector2(((i % 13) * 195) + 150, 16);
			//else {
			rect.anchorMin = new Vector2(((float)(i % 13) / 13f) + (1f / 26f), .61f);
			rect.anchorMax = new Vector2(((float)(i % 13) / 13f) + (1f / 26f), .61f);
			rect.anchoredPosition = Vector2.zero;
			//}

			WeekInputController wic = go.GetComponent<WeekInputController>();

			wic.weekNumber = i + 1;

			weeks[i] = go;
			weekControllers[i] = wic;
			activeWeeks[i] = true;
		}
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

	public void ExpandContractQuarter(int quarter) {
		if (quarter >= 0 && quarter < 4) {
			if (!quarterExpanding[quarter]) {
				if (quarterExpanded[quarter])
					StartCoroutine(ContractQuarter(quarter));
				else
					StartCoroutine(ExpandQuarter(quarter));
			}
		}
		else {
			Debug.Log("Error, trying to expand/contract something that isn't a quarter");
		}
	}

	public IEnumerator ExpandQuarter(int quarter) {
	
		yield return null;
	}

	public IEnumerator ContractQuarter(int quarter) {

		yield return null;
	}
}
