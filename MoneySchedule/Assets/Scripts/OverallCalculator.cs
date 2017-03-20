using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OverallCalculator : MonoBehaviour {

	public MYear year;
	public int weeksToCalculate = 52;

	public InputField inputAmt;

	public Text mainTextDisplay;
	public Text overallMoneyDisplay;
	public Text secondTextDisplay;
	public Text monthlyMoneyDisplay;
	public Text weekWordsDisplay;
	public Text numWeeksDisplay;

	//public Text label2;

	private string enterNum = "Enter yearly amount to calculate weekly amount";
	private string errorText = "Error:  Please enter a correct number";

	private Color regCol = Color.black;
	private Color errorCol = Color.red;

	private int amount;

	// Use this for initialization
	void Start () {
			
		//print(NumToString(3500000));


	}

	// Update is called once per frame
	void Update () {

		UpdateTexts();

	}

	private void UpdateTexts() {
		string s = inputAmt.text;

		if (s.Length == 0) {
			mainTextDisplay.color = regCol;
			mainTextDisplay.text = enterNum;
			overallMoneyDisplay.text = "";
			secondTextDisplay.text = "";
			monthlyMoneyDisplay.text = "";
			weekWordsDisplay.text = "";
			numWeeksDisplay.text = "";

			amount = 0;
		}
		else {

			try {
				int a = int.Parse(s);
				mainTextDisplay.color = regCol;
				string spaces1 = "                          ";
				string spaces2 = "                                 ";
				string spaces3 = "                                        ";
				mainTextDisplay.text = "If you want to make:" + spaces1 + "a year";
				overallMoneyDisplay.text = NumToMoneyString(a);

				if (weeksToCalculate > 0) {
					secondTextDisplay.color = regCol;
					secondTextDisplay.text = "You must make:" + spaces2 + "a week";
					monthlyMoneyDisplay.text = NumToMoneyString(a/weeksToCalculate);

					weekWordsDisplay.color = regCol;
					weekWordsDisplay.text = "If you work:" + spaces3 + "weeks a year";
					numWeeksDisplay.text = "" + weeksToCalculate;
				}
				else {
					secondTextDisplay.color = errorCol;
					secondTextDisplay.text = "Error:  Must have at least one active week";
					monthlyMoneyDisplay.text = "";


					weekWordsDisplay.color = errorCol;
					weekWordsDisplay.text = "Please check at least one active week box to fix this error";
					numWeeksDisplay.text = "";
				}

				amount = a;
			}
			catch  {
				mainTextDisplay.color = errorCol;
				mainTextDisplay.text = errorText;
				overallMoneyDisplay.text = "";
				secondTextDisplay.text = "";
				monthlyMoneyDisplay.text = "";
				weekWordsDisplay.text = "";
				numWeeksDisplay.text = "";

				amount = 0;
			}
		}		
	}

	public int GetAmount() {
		return amount;
	}


	public static string NumToMoneyString(int num) {
		string s = "";
		int number = num;
		if (num < 0) 
			number = -number;
		if (num == 0)
			return "0";
		int place = 0;
		int i = number;
		while (Mathf.Pow(10, place) <= number) {

			int next = i % 10;
			s = next + s;

			i = i / 10;

			if ((place + 1) % 3 == 0 && Mathf.Pow(10, place + 1) <= number)
				s = "," + s;

			place++;
		}
		s = "$" + s;
		if (num < 0)
			s = "-" + s;
		return s;
	}
}
