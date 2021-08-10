using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPose : MonoBehaviour
{
    [ExecuteInEditMode]

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gameObject.transform.position, 1f);
    }
}
