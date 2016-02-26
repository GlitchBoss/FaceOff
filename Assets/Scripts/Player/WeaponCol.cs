using UnityEngine;
using System.Collections;

public class WeaponCol : MonoBehaviour {

    public bool hasAttacked = false;
    public string enemyTag;

	void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != enemyTag)
            return;
        if (hasAttacked)
            return;
        transform.parent.GetComponent<Melee>().Hit(col.transform, this);
    }

	void OnTriggerExit2D(Collider2D col)
	{
		if ((col.tag == "Player" || col.tag == "Enemy") && hasAttacked)
            hasAttacked = false;
    }
}
