using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuarterDisplay : MonoBehaviour {

	public Text activeWeeksDisplay;
	public Text postiveWeeksDisplay;

	public Text quarterlyVarianceDisplay;
	public Text yearlyVarianceDisplay;


	private int quarterlyVariance;
	private int yearlyVariance;

	private int numActiveWeeks;
	private int numPositiveWeeks;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateDisplays();
	}


	public void UpdateValues(int numWeeks, int numPositive, int qv, int yv) {
		quarterlyVariance = qv;
		yearlyVariance = yv;
		numActiveWeeks = numWeeks;
		numPositiveWeeks = numPositive;
	}


	private void UpdateDisplays() {
		activeWeeksDisplay.text = "" + numActiveWeeks;
		postiveWeeksDisplay.text = "" + numPositiveWeeks;

		quarterlyVarianceDisplay.text = OverallCalculator.NumToMoneyString(quarterlyVariance);
		if (quarterlyVariance < 0)
			quarterlyVarianceDisplay.color = Color.red;
		else if (quarterlyVariance == 0)
			quarterlyVarianceDisplay.color = Color.black;
		else 
			quarterlyVarianceDisplay.color = Color.green;

		yearlyVarianceDisplay.text = OverallCalculator.NumToMoneyString(yearlyVariance);
		if (yearlyVariance < 0)
			yearlyVarianceDisplay.color = Color.red;
		else if (yearlyVariance == 0)
			yearlyVarianceDisplay.color = Color.black;
		else
			yearlyVarianceDisplay.color = Color.green;
				
	}


}
