using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

	public float timeToDeath;

	void Awake() {
		StartCoroutine(DestroyMyself());
	}
		
	private IEnumerator DestroyMyself() {
		yield return new WaitForSeconds(timeToDeath);
		Destroy(this.gameObject);
	}

}
