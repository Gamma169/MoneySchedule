using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MYear : MonoBehaviour {


	public InputField inputAmt;

	public Text perWeekText;
	//public Text label2;

	//public 

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
			perWeekText.color = regCol;
			perWeekText.text = enterNum;
		}
		else {

			try {
				int a = int.Parse(s);
				perWeekText.color = regCol;
				perWeekText.text = "If you want to make $" + a + " a year, you must make $" + (int)((float)a / 52f) + " a week";
			}
			catch  {
				perWeekText.color = errorCol;
				perWeekText.text = errorText;
			}
		}

	}
}
