using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomShape : BaseMeshEffect
{
	public int spaceValue = 0;

	public override void ModifyMesh(VertexHelper vh)
	{
		if (!IsActive())
			return;

		List<UIVertex> verts = new List<UIVertex>();
		vh.GetUIVertexStream(verts);
		vh.Clear();

		var a1 = verts[0];
		var a2 = verts[1];
		var a3 = verts[2];
		var b1 = verts[3];
		var b2 = verts[4];
		var b3 = verts[5];

		Matrix4x4 move = Matrix4x4.TRS(new Vector3(0, spaceValue, 0), Quaternion.identity, Vector3.one);

		a3.position = move.MultiplyPoint(a3.position);
		b1.position = move.MultiplyPoint(b1.position);
		b2.position = move.MultiplyPoint(b2.position);

		vh.AddVert(a1);
		vh.AddVert(a2);
		vh.AddVert(a3);
		vh.AddVert(b1);
		vh.AddVert(b2);
		vh.AddVert(b3);

		vh.AddTriangle(0, 1, 2);
		vh.AddTriangle(3, 4, 5);
	}
}
