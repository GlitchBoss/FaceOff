using UnityEngine;
using System.Collections.Generic;

namespace Data
{
	[CreateAssetMenu(fileName = "Faces", menuName = "Face List", order = 1)]
	public class FaceData : ScriptableObject
	{
		public List<Face> faces = new List<Face>();
	}
}
