using UnityEngine;
using System.Collections;

public class PlatformLevel : MonoBehaviour {

    public Level level;

	public enum Level
    {
        GroundLevel = 0,
        FirstLevel = 1,
        SecondLevel = 2,
        TopLevel = 3
    }

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			if(col.GetComponent<Player>().level.level != level)
				col.GetComponent<Player>().level.level = level;
		}
		else if (col.tag == "Enemy")
		{
			if (col.GetComponent<Enemy>().level.level != level)
				col.GetComponent<Enemy>().level.level = level;
		}
	}
}
