using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnnemyView))]
public class EnnemyViewGizmo : Editor
{
    private void OnSceneGUI()
    {
        EnnemyView ennemyView = (EnnemyView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(ennemyView.transform.position, Vector3.up, Vector3.forward, 360, ennemyView.radius);

        Vector3 viewAngle01 = DirectionFromAngle(ennemyView.transform.eulerAngles.y, -ennemyView.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(ennemyView.transform.eulerAngles.y, ennemyView.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(ennemyView.transform.position, ennemyView.transform.position + viewAngle01 * ennemyView.radius);
        Handles.DrawLine(ennemyView.transform.position, ennemyView.transform.position + viewAngle02 * ennemyView.radius);

        if (ennemyView.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(ennemyView.transform.position, ennemyView.player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}