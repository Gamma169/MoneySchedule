using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class MYear : MonoBehaviour {

	public int amountForYear;
	public int activeWeeks = 52;

	public OverallCalculator ovc;
	public YearScrollController ysc;


	// Use this for initialization
	void Start () {
		LoadData();
		StartCoroutine(ContinualSave());
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


	private IEnumerator ContinualSave() {
		yield return new WaitForSeconds(2);
		while (true) {
			yield return new WaitForSeconds(1);
			if (!ovc.GetError()) {
				SaveData();
				print("saved");
			}
		}
	}


	private void SaveData() {
		string dataPath = string.Format("{0}/YearData.dat", Application.persistentDataPath);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream;

		/*==== The following lines are where I update what things are to be saved ====*/
		YearData yData = new YearData();

		if (ovc.inputAmt.text.Length != 0)
			yData.overallMoney = ovc.GetAmount();
		else
			yData.overallMoney = int.MinValue;

		yData.activeWeeks = ysc.activeWeeks;

		yData.moneyEachWeek = new int[52];
		for (int i = 0; i < 52; i++) {
			if (ysc.weekControllers[i].GoodInput())
				yData.moneyEachWeek[i] = ysc.weekControllers[i].amountMadeThisWeek;
			else 
				yData.moneyEachWeek[i] = int.MinValue;
		}

		/*==== End ====*/

		try {
			if (File.Exists(dataPath)) {
				File.WriteAllText(dataPath, string.Empty);
				fileStream = File.Open(dataPath, FileMode.Open);
			}
			else {
				fileStream = File.Create(dataPath);
			}

			binaryFormatter.Serialize(fileStream, yData);
			fileStream.Close();

			/*
			if (Application.platform == RuntimePlatform.WebGLPlayer || Application.isWebPlayer) {
				//SyncFiles();
			}
			*/
		}
		catch (Exception e) {
			//PlatformSafeMessage("Failed to Save: " + e.Message);
			Debug.Log("Failed To Save: " + e.Message);
		}
	
	}

	private bool LoadData() {
		string dataPath = string.Format("{0}/YearData.dat", Application.persistentDataPath);

		try {
			if (File.Exists(dataPath)) {
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				FileStream fileStream = File.Open(dataPath, FileMode.Open);

				/*==== The following lines are where I update what things are to be loaded ====*/
				YearData newData = (YearData)binaryFormatter.Deserialize(fileStream);

				if (newData.overallMoney != int.MinValue) {
					amountForYear = newData.overallMoney;
					ovc.inputAmt.text = "" + newData.overallMoney;
				}
					
				for (int i=0; i<52; i++) {
					if (!newData.activeWeeks[i]) {
						ysc.activeWeeks[i] = false;
						ysc.weekControllers[i].isActive = false;
					}
				}

				for (int i=0; i<52; i++) {
					if (newData.moneyEachWeek[i] != int.MinValue) 
						ysc.weekControllers[i].SetAmount(newData.moneyEachWeek[i]);
				}


				/*==== End ====*/

				fileStream.Close();
				return true;
			}
			else 
				return false;
		}
		catch (Exception e) {
			//PlatformSafeMessage("Failed to Load: " + e.Message);
			Debug.Log("Failed To Load: " + e.Message);
			return false;
		}
	}


}

[Serializable]
class YearData {

	public int overallMoney;
	public bool[] activeWeeks;
	public int[] moneyEachWeek;

}
