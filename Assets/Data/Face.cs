using UnityEngine;
using System.Collections;
using GLITCH.Helpers;

namespace Data
{

	[System.Serializable]
	public class Face {

		public string name;
		public int ID;
		public Rigidbody2D prefab;
		public RangeFloat health;
		public RangeFloat power;
		public float speed;
		public float jumpForce;
		public int powerDecrease;
		public int powerIncrease;
		public bool useSecondaryControls;
		public bool facingRight;
		public bool lost;
		public bool engaged;
	}
}
