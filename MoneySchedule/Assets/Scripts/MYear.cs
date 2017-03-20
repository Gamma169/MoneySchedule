using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MYear : MonoBehaviour {

	public int amountForYear;
	public int activeWeeks = 52;

	public OverallCalculator ovc;
	public YearScrollController ysc;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		UpdateActiveWeeks();
		ovc.weeksToCalculate = activeWeeks;

		amountForYear = ovc.GetAmount();
		ysc.SetAmount(amountForYear);
	}



	public void UpdateActiveWeeks() {
		activeWeeks = 0;
		for (int i = 0; i < 52; i++) {
			if (ysc.activeWeeks[i])
				activeWeeks++;
			}
	}
}
