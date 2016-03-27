using UnityEngine;
using System.Collections;

public class EnemyFight : MonoBehaviour {

	public bool background = false;

	Enemy enemy;
	BackgroundFace face;

	void Start()
	{
		if (background)
		{
			face = GetComponentInParent<BackgroundFace>();
		}
		else
		{
			enemy = GetComponentInParent<Enemy>();
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if((col.tag == "Player" || col.tag == "BackgroundFace"))
		{
			if (background)
			{
				if(!face.shouldAttack)
					face.shouldAttack = true;
			}
			else
			{
				if(!enemy.shouldAttack)
					enemy.shouldAttack = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player" || col.tag == "BackgroundFace")
		{
			if (background)
			{
				face.shouldAttack = false;
			}
			else
			{
				enemy.shouldAttack = false;
			}
		}
	}
}
