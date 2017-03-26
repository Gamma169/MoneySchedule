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

	public GameObject NZHead;
	public GameObject NZHeadCont;
	//public Text label2;

	private string enterNum = "Enter yearly amount to calculate weekly amount";
	private string errorText = "Error:  Please enter a correct number";

	private Color regCol = Color.black;
	private Color errorCol = Color.red;

	private int amount;
	private bool error;

	// For the heads Easter Egg
	private ArrayList headsList;

	// Use this for initialization
	void Start () {

		headsList = new ArrayList();
	}

	// Update is called once per frame
	void Update () {

		UpdateTexts();

		// This is for the heads easter egg
		if (Input.GetKeyDown(KeyCode.Escape)) {
			while (headsList.Count > 0) {
				Destroy((GameObject)headsList[0]);
				headsList.RemoveAt(0);
			}
		}
		// End Easter Egg

	}

	private void UpdateTexts() {
		string s = inputAmt.text;

		// These are a heads easter egg.  Should probably be removed in production
		if (s.Equals("poop")) {
			if (Input.GetMouseButtonDown(1)) {
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Instantiate(NZHead, pos, Quaternion.identity);
			}
		}
		else if (s.Equals("poopies")) {
			if (Input.GetMouseButtonDown(1)) {
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				headsList.Add(Instantiate(NZHeadCont, pos, Quaternion.identity));
			}
		}
		// End of EE
		else if (s.Length == 0) {
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

					error = false;
				}
				else {
					secondTextDisplay.color = errorCol;
					secondTextDisplay.text = "Error:  Must have at least one active week";
					monthlyMoneyDisplay.text = "";


					weekWordsDisplay.color = errorCol;
					weekWordsDisplay.text = "Please check at least one active week box to fix this error";
					numWeeksDisplay.text = "";

					error = true;
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
				error = true;
			}
		}		
	}

	public int GetAmount() {
		return amount;
	}

	public bool GetError() {
		return error;
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
