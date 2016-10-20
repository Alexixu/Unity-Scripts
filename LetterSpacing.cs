using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LetterSpacing : BaseMeshEffect
{
	public int spaceValue = 0;
	UIVertex[] m_TempVerts = new UIVertex[4];
	public override void ModifyMesh(VertexHelper vh)
	{
		if (!IsActive())
			return;

		Text text = GetComponent<Text>();
		if (text == null)
			return;

		if (spaceValue <= 0)
			return;

		TextGenerator tg = text.cachedTextGenerator;
		IList<UIVertex> verts = tg.verts;
		vh.Clear();
		for (int i = 0; i < tg.characterCountVisible; i++)
		{
			var lb = verts[i * 4 + 0];
			var lt = verts[i * 4 + 1];
			var rt = verts[i * 4 + 2];
			var rb = verts[i * 4 + 3];

			Matrix4x4 move = Matrix4x4.TRS(new Vector3(spaceValue * i, 0, 0), Quaternion.identity, Vector3.one);

			lb.position = move.MultiplyPoint(lb.position);
			lt.position = move.MultiplyPoint(lt.position);
			rt.position = move.MultiplyPoint(rt.position);
			rb.position = move.MultiplyPoint(rb.position);

			m_TempVerts[0] = lb;
			m_TempVerts[1] = lt;
			m_TempVerts[2] = rt;
			m_TempVerts[3] = rb;

			vh.AddUIVertexQuad(m_TempVerts);
		}
	}
}
