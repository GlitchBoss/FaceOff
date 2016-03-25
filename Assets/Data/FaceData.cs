using UnityEngine;
using System.Collections.Generic;

namespace Data
{
	[CreateAssetMenu(fileName = "Faces", menuName = "Face List", order = 1)]
	public class FaceData : ScriptableObject
	{
		public List<Face> faces = new List<Face>();

		int nextId;

		public void AddFace()
		{
			Face face = new Face();
			face.ID = nextId;
			face.name = "Face " + face.ID.ToString();
			if (nextId != faces.Count)
			{
				nextId = faces.Count;
			}
			nextId++;
			faces.Add(face);
		}

		public void RemoveLastFace()
		{
			if(faces.Count <= 0)
			{
				return;
			}
			int i = faces.Count - 1;
			faces.Remove(faces[i]);
			if (nextId > 0)
			{
				nextId--;
			}
			else
			{
				nextId = 0;
			}
		}
	}
}
