// Script from https://answers.unity.com/questions/712123/draw-borders-cube.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineBounds : MonoBehaviour
{
    public List<Vector3[]> cubes = new List<Vector3[]>();
    void OnPostRender()
    {
        GL.PushMatrix();
        GL.Begin(GL.LINES);
        GL.Color(Color.blue);
        foreach (Vector3[] lista in cubes)
        {
            for (int i = 1; i < lista.Length; i++)
            {
                GL.Vertex(lista[i - 1]);
                GL.Vertex(lista[i]);
            }
        }
        GL.End();
        GL.PopMatrix();
    }
}
