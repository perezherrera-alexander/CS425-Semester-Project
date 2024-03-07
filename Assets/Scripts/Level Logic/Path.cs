using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour
{
    public static Transform[] waypoints;

    void Awake (){
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++){
            waypoints[i] = transform.GetChild(i);
        }
    }

    // void OnDrawGizmos()
    // {
    //     //Debug.Log("Drawing");
    //     if(waypoints.Length > 1)
    //     {
    //         for(int i = 0; i < waypoints.Length - 1; i++)
    //         {
    //             Gizmos.color = Color.red;
    //             Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
    //         }
    //     }
    // }
}
