using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Quadrilateral : MonoBehaviour
{
    [SerializeField]private GameObject original;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] endPoints;

    [SerializeField] private Vector2[] intersection = new Vector2[6];
    // Start is called before the first frame update
    void Start()
    {
   


    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < intersection.Length; i++)
        {
            intersection[i]=Vector2.zero;
        }
        if    (lineLine(spawnPoints[0].transform, endPoints[0].transform, spawnPoints[1].transform, endPoints[1].transform,0)
            || lineLine(spawnPoints[0].transform, endPoints[0].transform, spawnPoints[2].transform, endPoints[2].transform,1)
            || lineLine(spawnPoints[0].transform, endPoints[0].transform, spawnPoints[3].transform, endPoints[3].transform,2))
        {                                                                                                              
            Debug.Log("1");                                                                                            
        }                                                                                                              
        if    (lineLine(spawnPoints[1].transform, endPoints[1].transform, spawnPoints[0].transform, endPoints[0].transform,0)
            || lineLine(spawnPoints[1].transform, endPoints[1].transform, spawnPoints[2].transform, endPoints[2].transform,3)
            || lineLine(spawnPoints[1].transform, endPoints[1].transform, spawnPoints[3].transform, endPoints[3].transform,4))
        {                                                                                                                
            Debug.Log("2");                                                                                              
        }                                                                                                                
        if    (lineLine(spawnPoints[2].transform, endPoints[2].transform, spawnPoints[1].transform, endPoints[1].transform,3)
            || lineLine(spawnPoints[2].transform, endPoints[2].transform, spawnPoints[0].transform, endPoints[0].transform,1)
            || lineLine(spawnPoints[2].transform, endPoints[2].transform, spawnPoints[3].transform, endPoints[3].transform,5))
        {                                                                                                               
            Debug.Log("3");                                                                                             
        }                                                                                                               
        if    (lineLine(spawnPoints[3].transform, endPoints[3].transform, spawnPoints[1].transform, endPoints[1].transform,4)
            || lineLine(spawnPoints[3].transform, endPoints[3].transform, spawnPoints[2].transform, endPoints[2].transform,5)
            || lineLine(spawnPoints[3].transform, endPoints[3].transform, spawnPoints[0].transform, endPoints[0].transform,2))
        {
            Debug.Log("4");
        }


    }

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






    }

    bool lineLine(Transform spawn1, Transform end1, Transform spawn2, Transform end2,int i)
    {

        // calculate the distance to intersection point
        float uA = ((end2.position.x - spawn2.position.x) * (spawn1.position.y - spawn2.position.y) - (end2.position.y - spawn2.position.y) * (spawn1.position.x - spawn2.position.x)) / ((end2.position.y - spawn2.position.y) * (end1.position.x - spawn1.position.x) - (end2.position.x - spawn2.position.x) * (end1.position.y - spawn1.position.y));
        float uB = ((end1.position.x - spawn1.position.x) * (spawn1.position.y - spawn2.position.y) - (end1.position.y - spawn1.position.y) * (spawn1.position.x - spawn2.position.x)) /((end2.position.y - spawn2.position.y) * (end1.position.x - spawn1.position.x) - (end2.position.x - spawn2.position.x) * (end1.position.y - spawn1.position.y));
        if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
        {
            float intersectionX = spawn1.position.x + (uA * (end1.position.x - spawn1.position.x));
            float intersectionY = spawn1.position.y + (uA * (end1.position.y - spawn1.position.y));
            intersection[i] = new Vector2(intersectionX, intersectionY);
            return true;
        }
        // if uA and uB are between 0-1, lines are colliding

            return false;
    }

    void CheckSquare()
    {
        Vector2[] corner = new Vector2[4];
        for (int i = 0; i < corner.Length; i++)
        {
            for (int j = 0; j < intersection.Length; j++)
            {
                
            }

        }
    }
}
