using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Quadrilateral : MonoBehaviour
{
    [SerializeField] private GameObject original;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] endPoints;

    [SerializeField] private Vector2[] intersection = new Vector2[6];

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


    }

    // Update is called once per frame
    void Update()
    {
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
        {
            intersection[i] = Vector2.zero;
        }
        colLineLine(ref line1, line2, 0);
        colLineLine(ref line1, line3, 1);
        colLineLine(ref line1, line4, 2);
        colLineLine(ref line2, line1, 1);
        colLineLine(ref line2, line3, 3);
        colLineLine(ref line2, line4, 4);
        colLineLine(ref line3, line2, 3);
        colLineLine(ref line3, line1, 1);
        colLineLine(ref line3, line4, 5);
        colLineLine(ref line4, line2, 4);
        colLineLine(ref line4, line3, 5);
        colLineLine(ref line4, line1, 2);

        point[0] = checkColl(ref line1, line2, line3, line4);
        point[1] = checkColl(ref line2, line1, line3, line4);
        point[2] = checkColl(ref line3, line2, line1, line4);
        point[3] = checkColl(ref line4, line2, line3, line1);


    }

    void detectLines()
    {

    }
    //lineLine(spawnPoints[0].transform, endPoints[0].transform, spawnPoints[1].transform, endPoints[1].transform,0);
    //lineLine(spawnPoints[0].transform, endPoints[0].transform, spawnPoints[2].transform, endPoints[2].transform,1);
    //lineLine(spawnPoints[0].transform, endPoints[0].transform, spawnPoints[3].transform, endPoints[3].transform,2);
    //                                                                                                          
    //lineLine(spawnPoints[1].transform, endPoints[1].transform, spawnPoints[0].transform, endPoints[0].transform,0);
    //lineLine(spawnPoints[1].transform, endPoints[1].transform, spawnPoints[2].transform, endPoints[2].transform,3);
    //lineLine(spawnPoints[1].transform, endPoints[1].transform, spawnPoints[3].transform, endPoints[3].transform,4);
    //                                                                                                             
    //lineLine(spawnPoints[2].transform, endPoints[2].transform, spawnPoints[1].transform, endPoints[1].transform,3);
    //lineLine(spawnPoints[2].transform, endPoints[2].transform, spawnPoints[0].transform, endPoints[0].transform,1);
    //lineLine(spawnPoints[2].transform, endPoints[2].transform, spawnPoints[3].transform, endPoints[3].transform,5);
    //                                                                                                       
    //                                                                                                            
    //lineLine(spawnPoints[3].transform, endPoints[3].transform, spawnPoints[1].transform, endPoints[1].transform,4);
    //lineLine(spawnPoints[3].transform, endPoints[3].transform, spawnPoints[2].transform, endPoints[2].transform,5);
    //lineLine(spawnPoints[3].transform, endPoints[3].transform, spawnPoints[0].transform, endPoints[0].transform,2);
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(spawnPoints[0].transform.position, endPoints[0].transform.position);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(spawnPoints[1].transform.position, endPoints[1].transform.position);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(spawnPoints[2].transform.position, endPoints[2].transform.position);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(spawnPoints[3].transform.position, endPoints[3].transform.position);


        Gizmos.color = Color.cyan;



        Gizmos.color = Color.black;
        if (point[0] > -1)
        {
            if (intersection[point[0]] != Vector2.zero)
            {
                if (point[1] > -1)
                {
                    if (intersection[point[1]] != Vector2.zero)
                        Gizmos.DrawLine(intersection[point[0]], intersection[point[1]]);
                }

                if (point[2] > -1)
                {
                    if (intersection[point[2]] != Vector2.zero)
                        Gizmos.DrawLine(intersection[point[0]], intersection[point[2]]);
                }

                if (point[3] > -1)
                {
                    if (intersection[point[3]] != Vector2.zero)
                        Gizmos.DrawLine(intersection[point[0]], intersection[point[3]]);
                }
            }
        }

        Gizmos.color = Color.white;
        if (point[1] > -1)
        {
            if (intersection[point[1]] != Vector2.zero)
            {
                if (point[0] > -1)
                {
                    if (intersection[point[0]] != Vector2.zero)
                        Gizmos.DrawLine(intersection[point[1]], intersection[point[0]]);
                }

                if (point[2] > -1)
                {
                    if (intersection[point[2]] != Vector2.zero)
                        Gizmos.DrawLine(intersection[point[1]], intersection[point[2]]);
                }

                if (point[3] > -1)
                {
                    if (intersection[point[3]] != Vector2.zero)
                        Gizmos.DrawLine(intersection[point[1]], intersection[point[3]]);
                }
            }
        }
        Gizmos.color = Color.magenta;
        if (point[2] > -1)
        {
            if (intersection[point[2]] != Vector2.zero)
            {
                if (point[0] > -1)
                {
                    if (intersection[point[0]] != Vector2.zero)

                        Gizmos.DrawLine(intersection[point[2]], intersection[point[0]]);
                }

                if (point[1] > -1)
                {
                    if (intersection[point[1]] != Vector2.zero)
                        Gizmos.DrawLine(intersection[point[2]], intersection[point[1]]);
                }

                if (point[3] > -1)
                {
                    if (intersection[point[3]] != Vector2.zero)
                        Gizmos.DrawLine(intersection[point[2]], intersection[point[3]]);
                }
            }
        }
        Gizmos.color = Color.red;

        if (point[3] > -1)
        {
            if (intersection[point[3]] != Vector2.zero)
            {

                if (point[0] > -1)
                {
                    if (intersection[point[0]] != Vector2.zero)
                        Gizmos.DrawLine(intersection[point[3]], intersection[point[0]]);

                }
                if (point[1] > -1)
                {
                    if (intersection[point[1]] != Vector2.zero)
                        Gizmos.DrawLine(intersection[point[3]], intersection[point[1]]);
                }
                if (point[2] > -1)
                {
                    if (intersection[point[2]] != Vector2.zero)
                        Gizmos.DrawLine(intersection[point[3]], intersection[point[2]]);
                }
            }

        }


        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(spawnPoints[3].transform.position, endPoints[3].transform.position);



    }

    Vector2 promedio()
    {
        Vector2 promedio = (intersection[0] + intersection[2] + intersection[3] + intersection[5]) / 4;
        return promedio;
    }

    /*bool lineLine(Transform spawn1, Transform end1, Transform spawn2, Transform end2,int i)
    {

        // calculate the distance to intersection point
        float uA = ((linee.endPoint.x - spawn2.position.x) * (line1.startPoint.y - spawn2.position.y) - (linee.endPoint.y - spawn2.position.y) * (line1.startPoint.x - spawn2.position.x)) / ((linee.endPoint.y - spawn2.position.y) * (end1.position.x - line1.startPoint.x) - (linee.endPoint.x - spawn2.position.x) * (end1.position.y - line1.startPoint.y));
        float uB = ((end1.position.x - line1.startPoint.x) * (line1.startPoint.y - spawn2.position.y) - (end1.position.y - line1.startPoint.y) * (line1.startPoint.x - spawn2.position.x)) /((linee.endPoint.y - spawn2.position.y) * (end1.position.x - line1.startPoint.x) - (linee.endPoint.x - spawn2.position.x) * (end1.position.y - line1.startPoint.y));
        if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
        {
            float intersectionX = line1.startPoint.x + (uA * (end1.position.x - line1.startPoint.x));
            float intersectionY = line1.startPoint.y + (uA * (end1.position.y - line1.startPoint.y));
            intersection[i] = new Vector2(intersectionX, intersectionY);
            return true;
        }
        // if uA and uB are between 0-1, lines are colliding

            return false;
    }*/
    bool colLineLine(ref Line line1, Line line2, int i)
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
            {
                line1.collId1 = line2.id;
            }
            else if (line1.collId2 == 0)
            {
                line1.collId1 = line2.id;
            }
            else if (line1.collId3 == 0)
            {
                line1.collId1 = line2.id;
            }

            line1.colCounter++;
            return true;
        }
        // if uA and uB are between 0-1, lines are colliding

        return false;
    }

    int checkColl(ref Line line, Line line2, Line line3, Line line4)
    {
        int counter = 0;
        int idNocollision = -1;
        if (line.colCounter > 2 || line.colCounter < 2)
        {
            return -1;
        }
        else if (line.colCounter == 2)
        {
            if (line.id == line2.collId1 || line.id == line2.collId2 || line.id == line2.collId3)
            {
                counter++;
            }
            else
            {
                idNocollision = line2.id;
            }

            if (line.id == line3.collId1 || line.id == line3.collId2 || line.id == line3.collId3)
            {
                counter++;
            }
            else
            {
                idNocollision = line3.id;
            }

            if (line.id == line4.collId1 || line.id == line4.collId2 || line.id == line4.collId3)
            {
                counter++;
            }
            else
            {
                idNocollision = line4.id;

            }

            if (counter > 2)
            {
                return -1;
            }


            if (idNocollision == line2.id)
            {
                if (idNocollision == line2.id)
                {
                    if (line.colNum1 == line2.colNum1)
                    {
                        return line.colNum1;
                    }
                    else if (line.colNum2 == line2.colNum1)
                    {
                        return line.colNum2;
                    }
                    else if (line.colNum3 == line2.colNum1)
                    {
                        return line.colNum3;
                    }

                    if (line.colNum1 == line2.colNum2)
                    {
                        return line.colNum1;
                    }
                    else if (line.colNum2 == line2.colNum2)
                    {
                        return line.colNum2;
                    }
                    else if (line.colNum3 == line2.colNum2)
                    {
                        return line.colNum3;
                    }

                    if (line.colNum1 == line2.colNum3)
                    {
                        return line.colNum1;
                    }
                    else if (line.colNum2 == line2.colNum3)
                    {
                        return line.colNum2;
                    }
                    else if (line.colNum3 == line2.colNum3)
                    {
                        return line.colNum3;
                    }
                }

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

        }
        return -1;

    }

    float AnglePromedio()
    {
        return 0;
    }

    float Angle(GameObject start1, GameObject end1, GameObject start2, GameObject end2)
    {



        Vector2 prueba = end1.transform.position - start1.transform.position;
        Debug.Log(prueba);
        Vector2 prueba2 = end2.transform.position - start2.transform.position;
        Debug.Log(MathF.Acos(Vector2.Dot(prueba.normalized, prueba2.normalized)) * 180 / MathF.PI);


        //  Debug.Log(prueba.SqrMagnitude());



        return MathF.Acos(Vector2.Dot(prueba.normalized, prueba2.normalized)) * 180 / MathF.PI;
    }

    void isQuadrilateral()
    {
        float angle1 = Angle(spawnPoints[0], endPoints[0], spawnPoints[1], endPoints[1]);
        float angle2 = Angle(spawnPoints[1], endPoints[1], spawnPoints[2], endPoints[2]);
        float angle3 = Angle(spawnPoints[2], endPoints[2], spawnPoints[3], endPoints[3]);
        float angle4 = Angle(spawnPoints[3], endPoints[3], spawnPoints[0], endPoints[0]);

        Debug.Log(angle1 + angle2 + angle3 + angle4);
    }

    void CheckSquare()
    {
        Vector2[] corner = new Vector2[4];
        int collisionCounter = 0;
        for (int i = 0; i < intersection.Length; i++)
        {
            if (intersection[i] != Vector2.zero)
            {
                collisionCounter++;
            }

        }

        if (collisionCounter == 4)
        {
            Debug.Log("Hay 4 collisiones");


        }

    }
}



