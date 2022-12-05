using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Quadrilateral : MonoBehaviour
{
    [SerializeField] private GameObject original;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] endPoints;

    [SerializeField] private Vector2[] intersection = new Vector2[6];
    [SerializeField] private float quadArea;

    // Start is called before the first frame update
    public struct Line
    {
        public Vector2 startPoint;
        public Vector2 endPoint;
        public int colCounter;
        public int id;
        public int collId1;
        public int collId2;
        public int collId3;
        public int colNum1;
        public int colNum2;
        public int colNum3;
    }

    public Line line1;
    public Line line2;
    public Line line3;
    public Line line4;
    public int[] point = new int[4];

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        line1.id = 1;
        line2.id = 2;
        line3.id = 3;
        line4.id = 4;
        line1.colNum1 = 0;
        line1.colNum2 = 1;
        line1.colNum3 = 2;
        line2.colNum1 = 1;
        line2.colNum2 = 3;
        line2.colNum3 = 4;
        line3.colNum1 = 3;
        line3.colNum2 = 1;
        line3.colNum3 = 5;
        line4.colNum1 = 4;
        line4.colNum2 = 5;
        line4.colNum3 = 2;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(spawnPoints[0].transform.position, endPoints[0].transform.position);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(spawnPoints[1].transform.position, endPoints[1].transform.position);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(spawnPoints[2].transform.position, endPoints[2].transform.position);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(spawnPoints[3].transform.position, endPoints[3].transform.position);

        line1.startPoint = spawnPoints[0].transform.position;
        line2.startPoint = spawnPoints[1].transform.position;
        line3.startPoint = spawnPoints[2].transform.position;
        line4.startPoint = spawnPoints[3].transform.position;

        line1.endPoint = endPoints[0].transform.position;
        line2.endPoint = endPoints[1].transform.position;
        line3.endPoint = endPoints[2].transform.position;
        line4.endPoint = endPoints[3].transform.position;

        line1.colCounter = 0;
        line2.colCounter = 0;
        line3.colCounter = 0;
        line4.colCounter = 0;

        for (int i = 0; i < intersection.Length; i++)
            intersection[i] = Vector2.zero;

        ColLineLine(ref line1, line2, 0);
        ColLineLine(ref line1, line3, 1);
        ColLineLine(ref line1, line4, 2);
        ColLineLine(ref line2, line1, 1);
        ColLineLine(ref line2, line3, 3);
        ColLineLine(ref line2, line4, 4);
        ColLineLine(ref line3, line2, 3);
        ColLineLine(ref line3, line1, 1);
        ColLineLine(ref line3, line4, 5);
        ColLineLine(ref line4, line2, 4);
        ColLineLine(ref line4, line3, 5);
        ColLineLine(ref line4, line1, 2);

        point[0] = CheckColl(ref line1, line2, line3, line4);
        point[1] = CheckColl(ref line2, line1, line3, line4);
        point[2] = CheckColl(ref line3, line2, line1, line4);
        point[3] = CheckColl(ref line4, line2, line3, line1);

        CalculateArea();
    }

    Vector2 Average()
    {
        Vector2 avg = (intersection[0] + intersection[2] + intersection[3] + intersection[5]) / 4;
        return avg;
    }

    bool ColLineLine(ref Line line1, Line line2, int i)
    {

        // calculate the distance to intersection point
        float uA =
            ((line2.endPoint.x - line2.startPoint.x) * (line1.startPoint.y - line2.startPoint.y) -
             (line2.endPoint.y - line2.startPoint.y) * (line1.startPoint.x - line2.startPoint.x)) /
            ((line2.endPoint.y - line2.startPoint.y) * (line1.endPoint.x - line1.startPoint.x) -
             (line2.endPoint.x - line2.startPoint.x) * (line1.endPoint.y - line1.startPoint.y));

        float uB =
            ((line1.endPoint.x - line1.startPoint.x) * (line1.startPoint.y - line2.startPoint.y) -
             (line1.endPoint.y - line1.startPoint.y) * (line1.startPoint.x - line2.startPoint.x)) /
            ((line2.endPoint.y - line2.startPoint.y) * (line1.endPoint.x - line1.startPoint.x) -
             (line2.endPoint.x - line2.startPoint.x) * (line1.endPoint.y - line1.startPoint.y));

        if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
        {
            float intersectionX = line1.startPoint.x + (uA * (line1.endPoint.x - line1.startPoint.x));
            float intersectionY = line1.startPoint.y + (uA * (line1.endPoint.y - line1.startPoint.y));

            intersection[i] = new Vector2(intersectionX, intersectionY);

            if (line1.collId1 == 0)
                line1.collId1 = line2.id;
            else if (line1.collId2 == 0)
                line1.collId1 = line2.id;
            else if (line1.collId3 == 0)
                line1.collId1 = line2.id;

            line1.colCounter++;

            return true;
        }
        // if uA and uB are between 0-1, lines are colliding

        return false;
    }

    int CheckColl(ref Line line, Line line2, Line line3, Line line4)
    {
        int counter = 0;
        int idNocollision = -1;

        if (line.id == line2.collId1 || line.id == line2.collId2 || line.id == line2.collId3)
            counter++;
        else
            idNocollision = line2.id;

        if (line.id == line3.collId1 || line.id == line3.collId2 || line.id == line3.collId3)
            counter++;
        else
            idNocollision = line3.id;

        if (line.id == line4.collId1 || line.id == line4.collId2 || line.id == line4.collId3)
            counter++;
        else
            idNocollision = line4.id;

        if (counter > 2)
            return -1;

        if (idNocollision == line2.id)
        {
            if (line.colNum1 == line2.colNum1)
                return line.colNum1;
            else if (line.colNum2 == line2.colNum1)
                return line.colNum2;
            else if (line.colNum3 == line2.colNum1)
                return line.colNum3;

            if (line.colNum1 == line2.colNum2)
                return line.colNum1;
            else if (line.colNum2 == line2.colNum2)
                return line.colNum2;
            else if (line.colNum3 == line2.colNum2)
                return line.colNum3;

            if (line.colNum1 == line2.colNum3)
                return line.colNum1;
            else if (line.colNum2 == line2.colNum3)
                return line.colNum2;
            else if (line.colNum3 == line2.colNum3)
                return line.colNum3;
        }

        if (idNocollision == line3.id)
        {

            if (line.colNum1 == line3.colNum1)
            {
                return line.colNum1;
            }
            else if (line.colNum2 == line3.colNum1)
            {
                return line.colNum2;
            }
            else if (line.colNum3 == line3.colNum1)
            {
                return line.colNum3;
            }

            if (line.colNum1 == line3.colNum2)
            {
                return line.colNum1;
            }
            else if (line.colNum2 == line3.colNum2)
            {
                return line.colNum2;
            }
            else if (line.colNum3 == line3.colNum2)
            {
                return line.colNum3;
            }

            if (line.colNum1 == line3.colNum3)
            {
                return line.colNum1;
            }
            else if (line.colNum2 == line3.colNum3)
            {
                return line.colNum2;
            }
            else if (line.colNum3 == line3.colNum3)
            {
                return line.colNum3;
            }

        }

        if (idNocollision == line4.id)
        {
            if (line.colNum1 == line4.colNum1)
            {
                return line.colNum1;
            }
            else if (line.colNum2 == line4.colNum1)
            {
                return line.colNum2;
            }
            else if (line.colNum3 == line4.colNum1)
            {
                return line.colNum3;
            }

            if (line.colNum1 == line4.colNum2)
            {
                return line.colNum1;
            }
            else if (line.colNum2 == line4.colNum2)
            {
                return line.colNum2;
            }
            else if (line.colNum3 == line4.colNum2)
            {
                return line.colNum3;
            }

            if (line.colNum1 == line4.colNum3)
            {
                return line.colNum1;
            }
            else if (line.colNum2 == line4.colNum3)
            {
                return line.colNum2;
            }
            else if (line.colNum3 == line4.colNum3)
            {
                return line.colNum3;
            }
        }

        return -1;
    }

    float Angle(GameObject start1, GameObject end1, GameObject start2, GameObject end2)
    {
        Vector2 prueba = end1.transform.position - start1.transform.position;
        Debug.Log(prueba);
        Vector2 prueba2 = end2.transform.position - start2.transform.position;
        Debug.Log(MathF.Acos(Vector2.Dot(prueba.normalized, prueba2.normalized)) * 180 / MathF.PI);

        return MathF.Acos(Vector2.Dot(prueba.normalized, prueba2.normalized)) * 180 / MathF.PI;
    }

    void IsQuadrilateral()
    {
        float angle1 = Angle(spawnPoints[0], endPoints[0], spawnPoints[1], endPoints[1]);
        float angle2 = Angle(spawnPoints[1], endPoints[1], spawnPoints[2], endPoints[2]);
        float angle3 = Angle(spawnPoints[2], endPoints[2], spawnPoints[3], endPoints[3]);
        float angle4 = Angle(spawnPoints[3], endPoints[3], spawnPoints[0], endPoints[0]);

        Debug.Log(angle1 + angle2 + angle3 + angle4);
    }

    private float GetTriangleArea(Vector2 a, Vector2 b, Vector2 c)
    {
        float side1 = (b - a).magnitude;
        float side2 = (c - b).magnitude;
        float side3 = (a - c).magnitude;

        float semiperimeter = (side1 + side2 + side3) / 2;
        float area = (float)Math.Sqrt(semiperimeter * (semiperimeter - side1) * (semiperimeter - side2) * (semiperimeter - side3));

        return area;
    }

    void CalculateArea()
    {
        quadArea = 0;

        Gizmos.color = Color.magenta;

        for (int i = 0; i < point.Length; i++)
        {
            if (point[i] > -1)
            {
                if (intersection[point[i]] != Vector2.zero)
                {
                    for (int j = 0; j < point.Length; j++)
                    {
                        if (i == j)
                            continue;

                        if (point[j] > -1)
                        {
                            if (intersection[point[j]] != Vector2.zero)
                            {
                                Gizmos.DrawLine(intersection[point[i]], intersection[point[j]]);

                                for (int k = 0; k < point.Length; k++)
                                {
                                    if (k == i)
                                        continue;

                                    if (k == j)
                                        continue;

                                    quadArea += GetTriangleArea(intersection[point[i]], intersection[point[j]],
                                        intersection[point[k]]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}