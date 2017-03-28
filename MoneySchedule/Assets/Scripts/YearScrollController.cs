using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YearScrollController : MonoBehaviour {

	public GameObject weekInput;
	public GameObject[] quarterHolders;
	public GameObject[] quarterFullSummaries;
	public QuarterDisplay[] quarterEndlineSummaries;
	public QuarterDisplay[] quarterFullSummariesDisplays;
	public WeekBreakdownController[] weekBreakdownControllers;
	public Text[] buttonTexts;
	public Text[] variancesTexts;

	public GameObject[] weeks;
	public WeekInputController[] weekControllers;
	public bool[] activeWeeks;

	private RectTransform rt;

	private int yearAmount;
	private int numWeeks;

	private int weeklyAmount;

	private ScrollRect[] scrollRects;
	private bool[] quarterExpanded;
	private bool[] quarterExpanding;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();

		weeks = new GameObject[52];
		weekControllers = new WeekInputController[52];
		activeWeeks = new bool[52];

		scrollRects = gameObject.GetComponentsInChildren<ScrollRect>();
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
		UpdateQuarterSummary();

		UpdateWeekBreakdowns();
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
			rect.anchorMin = new Vector2((((float)(i % 13) / 13f) + (1f / 26f) ) * 0.9f, .61f);
			rect.anchorMax = new Vector2((((float)(i % 13) / 13f) + (1f / 26f) ) * 0.9f, .61f);
			rect.anchoredPosition = Vector3.zero;
			rect.localScale = new Vector3(1, 1, 1);
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

	public void UpdateQuarterSummary() {
		for (int i = 0; i < 4; i++) {
			int activeWeeks = 0;
			int positiveWeeks = 0;
			int quarterlyVariance = 0;
			for (int j = i * 13; j < (i * 13) + 13; j++) {
				if (weekControllers[j].isActive)
					activeWeeks++;

				if (weekControllers[j].weeklyVariance >= 0)
					positiveWeeks++;

				quarterlyVariance += weekControllers[j].weeklyVariance;
			}

			int yearlyVariance = weekControllers[((i + 1) * 13) - 1].yearlyVariance;
			quarterEndlineSummaries[i].UpdateValues(activeWeeks, positiveWeeks, quarterlyVariance, yearlyVariance);
			quarterFullSummariesDisplays[i].UpdateValues(activeWeeks, positiveWeeks, quarterlyVariance, yearlyVariance);
		}
	}

	public void UpdateWeekBreakdowns() {
		for (int i = 0; i < 4; i++) {
			//if (weekControllers[i].isActive)
				weekBreakdownControllers[i].UpdateVariance(weeklyAmount);
			//else
				//weekBreakdownControllers[i].UpdateVariance(0);
			for (int j = 0; j < 13; j++) {
				weekBreakdownControllers[i].UpdateWeekAmount(j, weekControllers[(i * 13) + j].isActive,  weekControllers[(i * 13) + j].amountMadeThisWeek);
			}
		}
	}

	public void SetAmount(int newAmount) {
		yearAmount = newAmount;
	}

	public void ExpandContractQuarter(int quarter) {
		if (quarter >= 0 && quarter < 4) {
			if (!quarterExpanding[quarter]) {

				/*
				if (quarterExpanded[quarter])
					StartCoroutine(ContractQuarter(quarter));
				else
					StartCoroutine(ExpandQuarter(quarter));
				*/
				StartCoroutine(ExpandContractQuarterHelper(quarter, !quarterExpanded[quarter]));
			}
		}
		else {
			Debug.Log("Error, trying to expand/contract something that isn't a quarter");
		}
	}

	public IEnumerator ExpandContractQuarterHelper(int quarter, bool expand) {
		quarterExpanding[quarter] = true;

		float overTime = .3f;
		int quarterOriginalSize = 2655;
		CanvasGroup cvg = quarterHolders[quarter].GetComponent<CanvasGroup>();
		RectTransform rt = quarterHolders[quarter].GetComponent<RectTransform>();

		CanvasGroup scvg = quarterFullSummaries[quarter].GetComponent<CanvasGroup>();

		cvg.interactable = expand;
		cvg.blocksRaycasts = expand;

		scvg.interactable = !expand;
		scvg.blocksRaycasts = !expand;

		for (float i = expand ? 0 : overTime; expand ? (i < overTime) : (i > 0); i += expand ? Time.deltaTime : -Time.deltaTime) {

			float lerpVal = i / overTime;

			rt.sizeDelta = new Vector2(Mathf.Lerp(0, quarterOriginalSize, lerpVal), 0);
			rt.anchoredPosition = new Vector2(Mathf.Lerp(75, 0, lerpVal), 0);

			variancesTexts[2 * quarter].color = Color.Lerp(Color.clear, Color.black, lerpVal);
			variancesTexts[(2 * quarter) + 1].color = Color.Lerp(Color.clear, Color.black, lerpVal);

			cvg.alpha = lerpVal;
			scvg.alpha = 1 - lerpVal;

			yield return null;
		}


		rt.sizeDelta = new Vector2(expand ? quarterOriginalSize : 0, 0);
		rt.anchoredPosition = new Vector2(expand ? 0 : 75, 0);
		cvg.alpha = expand ? 1 : 0;
		scvg.alpha = expand ? 0 : 1;

		buttonTexts[quarter].text = (expand ? "Contract" : "Expand") + "\nQuarter " + (quarter + 1);

		variancesTexts[2 * quarter].color = expand ? Color.black : Color.clear;
		variancesTexts[(2 * quarter) + 1].color = expand ? Color.black : Color.clear;

		// I have this in here because it is doing a weird bouncing thing when expanded and don't want it to do that
		yield return null;
		rt.sizeDelta = new Vector2(expand ? quarterOriginalSize : 0, 0);
		rt.anchoredPosition = new Vector2(expand ? 2 : 75, 0);

		scrollRects[quarter].enabled = expand;

		quarterExpanding[quarter] = false;
		quarterExpanded[quarter] = !quarterExpanded[quarter];
	}
}
