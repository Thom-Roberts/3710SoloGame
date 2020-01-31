// Script from https://answers.unity.com/questions/712123/draw-borders-cube.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    private Vector3 v3FrontTopLeft;
    private Vector3 v3FrontTopRight;
    private Vector3 v3FrontBottomLeft;
    private Vector3 v3FrontBottomRight;
    private Vector3 v3BackTopLeft;
    private Vector3 v3BackTopRight;
    private Vector3 v3BackBottomLeft;
    private Vector3 v3BackBottomRight;

    private Vector3[] list = null;


    void Update()
    {
        CalcPositons();
    }

    void CalcPositons()
    {
        if (list == null)
        {
            list = new Vector3[24];

            Bounds bounds;
            BoxCollider bc = GetComponent<BoxCollider>();
            if (bc != null)
                bounds = bc.bounds;
            else
                return;

            Vector3 v3Center = bounds.center;
            Vector3 v3Extents = bounds.extents;

            v3FrontTopLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);
            v3FrontTopRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);
            v3FrontBottomLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);
            v3FrontBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z); 
            v3BackTopLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top left corner
            v3BackTopRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);
            v3BackBottomLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);
            v3BackBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);

            list[0] = (v3FrontTopLeft);
            list[1] = (v3FrontTopRight);
            list[2] = (v3FrontTopRight);
            list[3] = (v3FrontBottomRight);
            list[4] = (v3FrontBottomRight);
            list[5] = (v3FrontBottomLeft);
            list[6] = (v3FrontBottomLeft);
            list[7] = (v3FrontTopLeft);
            list[8] = (v3BackTopLeft);
            list[9] = (v3BackTopRight);
            list[10] = (v3BackTopRight);
            list[11] = (v3BackBottomRight);
            list[12] = (v3BackBottomRight);
            list[13] = (v3BackBottomLeft);
            list[14] = (v3BackBottomLeft);
            list[15] = (v3BackTopLeft);
            list[16] = (v3FrontTopLeft);
            list[17] = (v3BackTopLeft);
            list[18] = (v3FrontTopRight);
            list[19] = (v3BackTopRight);
            list[20] = (v3FrontBottomRight);
            list[21] = (v3BackBottomRight);
            list[22] = (v3FrontBottomLeft);
            list[23] = (v3BackBottomLeft);
            Camera.main.GetComponent<DrawLineBounds>().cubes.Add(list);
        }
    }
}
