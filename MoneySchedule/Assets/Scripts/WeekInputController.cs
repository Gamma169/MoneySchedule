using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeekInputController : MonoBehaviour {

	public const int NO_INPUT = 0;
	public const int GOOD_INPUT = 1;
	public const int ERROR_INPUT = -1;


	public int weekNumber;
	public bool isActive;

	public int amountMadeThisWeek;
	public int weeklyVariance;
	public int yearlyVariance;

	public Text thisWeekDisplay;
	public Text weeklyVarianceDisplay;
	public Text yearlyVarianceDisplay;

	private Text weekLabel;

	private InputField input;
	private Toggle tog;

	private int inputType;

	private bool firstUpdate = true;
	private string loadValString = "";

	private int weeklyVarianceAmount;

	// Use this for initialization
	void Start () {
		weekLabel = GetComponentInChildren<Text>();
		input = GetComponent<InputField>();
		tog = GetComponentInChildren<Toggle>();

		weekLabel.text = "Week " + weekNumber;
		tog.isOn = isActive;

		firstUpdate = true;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateAmount();
		UpdateDisplays();
		if (firstUpdate) {
			input.text = loadValString;
			firstUpdate = false;
		}
	}


	public void UpdateAmount() {
		string s = input.text;
		if (s.Length == 0) {
			amountMadeThisWeek = 0;
			inputType = NO_INPUT;
		}
		else {
			try {
				amountMadeThisWeek = int.Parse(s);
				inputType = GOOD_INPUT;

			}
			catch {
				amountMadeThisWeek = 0;
				inputType = ERROR_INPUT;
			}
		}
		weeklyVarianceAmount = amountMadeThisWeek - weeklyVariance;
	}


	public void UpdateDisplays() {
		thisWeekDisplay.text = OverallCalculator.NumToMoneyString(amountMadeThisWeek);
		if (inputType == GOOD_INPUT) {

			if (weeklyVariance < 0) {
				// pivot must be less than 1
				float pivotLocation = 0.65f;
				float slope = (1f / ((1f - pivotLocation) * (float)weeklyVarianceAmount));
				weeklyVarianceDisplay.color = Color.Lerp(Color.red, Color.black, (slope * amountMadeThisWeek) - (pivotLocation / (1f - pivotLocation)));
				//print((slope * amountMadeThisWeek) - (pivotLocation / (1f - pivotLocation)));
			}
			else if (weeklyVariance == 0)
				weeklyVarianceDisplay.color = Color.black;
			else {
				// pivot must be less than 1
				float pivotLocation = 0.35f;
				float slope = (1f / ((1f - pivotLocation) * (float)weeklyVarianceAmount));
				weeklyVarianceDisplay.color = Color.Lerp(Color.black, Color.green, (slope * (amountMadeThisWeek - weeklyVarianceAmount)) - (pivotLocation / (1f - pivotLocation)));
			}
			weeklyVarianceDisplay.text = OverallCalculator.NumToMoneyString(weeklyVariance);


			if (yearlyVariance < 0) {
				yearlyVarianceDisplay.color = Color.Lerp(Color.black, Color.red, -yearlyVariance / (0.75f * weeklyVarianceAmount));
			}
			else if (yearlyVariance == 0) {
				yearlyVarianceDisplay.color = Color.black;
			}
			else {
				yearlyVarianceDisplay.color = Color.Lerp(Color.black, Color.green, yearlyVariance / (0.75f * weeklyVarianceAmount));
			}

			yearlyVarianceDisplay.text = OverallCalculator.NumToMoneyString(yearlyVariance);
		}
		else if (inputType == NO_INPUT) {
			weeklyVarianceDisplay.color = Color.black;
			yearlyVarianceDisplay.color = Color.black;
			weeklyVarianceDisplay.text = "";
			yearlyVarianceDisplay.text = "";
		}
		else {
			weeklyVarianceDisplay.color = Color.red;
			yearlyVarianceDisplay.color = Color.red;
			weeklyVarianceDisplay.text = "Error:";
			yearlyVarianceDisplay.text = "Bad Input";
		}
	}

	public void ToggleActivate() {
		if (!firstUpdate)
			isActive = !isActive;
	}

	public bool GoodInput() {
		return inputType == GOOD_INPUT;
	}

	public void SetAmount(int i) {
		amountMadeThisWeek = i;
		if (input != null)
			input.text = "" + i;
		else
			loadValString = "" + i;
	}
}
