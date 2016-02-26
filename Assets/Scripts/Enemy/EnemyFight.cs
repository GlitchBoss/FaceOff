using UnityEngine;
using System.Collections;

public class EnemyFight : MonoBehaviour {

	Enemy enemy;

	void Start()
	{
		enemy = GetComponentInParent<Enemy>();
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if(col.tag == "Player" && !enemy.shouldAttack)
		{
			enemy.shouldAttack = true;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			enemy.shouldAttack = false;
		}
	}
}
