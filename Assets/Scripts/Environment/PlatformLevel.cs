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
}
