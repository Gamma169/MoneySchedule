using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeekBreakdownController : MonoBehaviour {

	public GameObject[] weekBars;

	private RectTransform[] weekTransforms;
	private Image[] weekImages;

	private int weeklyVarianceAmount;

	private int[] weeklyVariances;
	private bool[] activeWeeks;

	// Use this for initialization
	void Start () {

		weekTransforms = new RectTransform[weekBars.Length];
		weeklyVariances = new int[weekBars.Length];
		weekImages = new Image[weekBars.Length];
		activeWeeks = new bool[weekBars.Length];

		for (int i = 0; i < weekBars.Length; i++) {
			weekTransforms[i] = weekBars[i].GetComponent<RectTransform>();
			weekImages[i] = weekBars[i].GetComponent<Image>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateBars();
	}

	public void UpdateVariance(int newAmount) {
		weeklyVarianceAmount = newAmount;
	}

	public void UpdateWeekAmount(int week, bool active, int newAmount) {
		if (week < 0 || week > weeklyVariances.Length)
			Debug.Log("Error, not valid week to Update for WeekBreakdownController");
		else {
			activeWeeks[week] = active;
			weeklyVariances[week] = newAmount;
		}
	}

	private void UpdateBars() {
		for (int i = 0; i < weekBars.Length; i++) {

			float lerpVal;

			if (weeklyVarianceAmount == 0)
				lerpVal = 0;
			else
				lerpVal = (float)weeklyVariances[i] / (float)weeklyVarianceAmount; 


			weekTransforms[i].localScale = new Vector3(1, lerpVal, 1);

			if (activeWeeks[i]) {
				if (weeklyVariances[i] < weeklyVarianceAmount / 2) {
					weekImages[i].color = Color.red;
				}
				else if (weeklyVariances[i] <= weeklyVarianceAmount) {
					weekImages[i].color = Color.Lerp(Color.red, Color.black, (lerpVal - 0.5f) * 2);
				}
				else if (weeklyVariances[i] < weeklyVarianceAmount * 2) {
					weekImages[i].color = Color.Lerp(Color.black, Color.green, (lerpVal - 1));
				}
				else {
					weekImages[i].color = Color.green;
				}
			}
			else {
				weekImages[i].color = Color.gray;
			}
				
			
		}
	}
}
