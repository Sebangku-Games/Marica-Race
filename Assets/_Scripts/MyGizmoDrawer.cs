using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyGizmoDrawer : MonoBehaviour
{
    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmosSelected(MyGizmoDrawer script, GizmoType type)
    {
        // Your Gizmo drawing code here
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(script.transform.position, 0.1f);
    }
}
