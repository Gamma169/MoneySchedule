using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeekInputController : MonoBehaviour {

	public const int NO_INPUT = 0;
	public const int GOOD_INPUT = 1;
	public const int ERROR_INPUT = -1;


	public int weekNumber;
	public bool isActive = true;

	public int amountMadeThisWeek;
	public int weeklyVariance;
	public int yearlyVariance;

	public Text thisWeekDisplay;
	public Text weeklyVarianceAmount;
	public Text yearlyVarianceAmount;

	private Text weekLabel;

	private InputField input;

	private int inputType;

	// Use this for initialization
	void Start () {
		weekLabel = GetComponentInChildren<Text>();
		input = GetComponent<InputField>();

		weekLabel.text = "Week " + weekNumber;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateAmount();
		UpdateDisplays();
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
	}


	public void UpdateDisplays() {
		thisWeekDisplay.text = OverallCalculator.NumToMoneyString(amountMadeThisWeek);
		if (inputType == GOOD_INPUT) {
			weeklyVarianceAmount.text = OverallCalculator.NumToMoneyString(weeklyVariance);
			yearlyVarianceAmount.text = OverallCalculator.NumToMoneyString(yearlyVariance);
		}
		else if (inputType == NO_INPUT) {
			weeklyVarianceAmount.text = "";
			yearlyVarianceAmount.text = "";
		}
		else {
		
		}
	}

	public void ToggleActivate() {
		isActive = !isActive;
	}
}
