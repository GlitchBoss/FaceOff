using UnityEngine;
using System.Collections;

public class Deactivator : MonoBehaviour {

	public GameObject[] deactivate;
	public float delay;

	public void OnStart()
	{
		StartCoroutine(Deactivate(delay));
	}

	IEnumerator Deactivate(float delay)
	{
		yield return new WaitForSeconds(delay);
		foreach(GameObject go in deactivate)
		{
			go.SetActive(false);
		}
	}
}
