using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calandar : MonoBehaviour {

	public const int JAN = 31;
	public const int FEB1 = 28;
	public const int FEB2 = 29;
	public const int MAR = 31;
	public const int APR = 30;
	public const int MAY = 30;
	public const int JUN = 30;
	public const int JUL = 31;
	public const int AUG = 31;
	public const int SEP = 30;
	public const int OCT = 31;
	public const int NOV = 30;
	public const int DEC = 30;


	public const int MON = 0;
	public const int TUE = 1;
	public const int WED = 2;
	public const int THU = 3;
	public const int FRI = 4;
	public const int SAT = 5;
	public const int SUN = 6;


	public static bool error;



	public static int[] cal = { JAN, FEB1, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC };

	// Jan 1 2010 = Fri = 4;

	public GameObject[] years;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
