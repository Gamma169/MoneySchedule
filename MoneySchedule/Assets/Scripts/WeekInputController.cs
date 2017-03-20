using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeekInputController : MonoBehaviour {

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
		}
		else {
			try {
				amountMadeThisWeek = int.Parse(s);

			}
			catch {
				amountMadeThisWeek = 0;
			}
		}
	}


	public void UpdateDisplays() {
		thisWeekDisplay.text = OverallCalculator.NumToString(amountMadeThisWeek);
	
	}

	public void ToggleActivate() {
		isActive = !isActive;
	}
}
