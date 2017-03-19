using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OverallCalculator : MonoBehaviour {

	public MYear year;
	public int activeMonths = 52;

	public InputField inputAmt;

	public Text mainTextDisplay;
	public Text overallMoneyDisplay;
	public Text secondTextDisplay;
	public Text monthlyMoneyDisplay;
	//public Text label2;

	private string enterNum = "Enter yearly amount to calculate weekly amount";
	private string errorText = "Error:  Please enter a correct number";

	private Color regCol = Color.black;
	private Color errorCol = Color.red;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		string s = inputAmt.text;

		if (s.Length == 0) {
			mainTextDisplay.color = regCol;
			mainTextDisplay.text = enterNum;
			overallMoneyDisplay.text = "";
			secondTextDisplay.text = "";
			monthlyMoneyDisplay.text = "";

			year.amountForYear = 0;
		}
		else {

			try {
				int a = int.Parse(s);
				mainTextDisplay.color = regCol;
				perWeekText.text = "If you want to make $" + a + " a year, you must make $" + (int)((float)a / 52f) + " a week";
				year.amountForYear = a;
			}
			catch  {
				perWeekText.color = errorCol;
				perWeekText.text = errorText;
				year.amountForYear = 0;
			}
		}

	}
}
