using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuarterDisplay : MonoBehaviour {

	private int quarterlyVariance;
	private int yearlyVariance;

	private int numActiveWeeks;
	private int numPositiveWeeks;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void UpdateValues(int numWeeks, int numPositive, int qv, int yv) {
		quarterlyVariance = qv;
		yearlyVariance = yv;
		numActiveWeeks = numWeeks;
		numPositiveWeeks = numPositive;
	}



}
