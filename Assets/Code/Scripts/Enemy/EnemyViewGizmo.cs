using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyView))]
public class EnemyViewGizmo : Editor
{
    private void OnSceneGUI()
    {
        EnemyView enemyView = (EnemyView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemyView.transform.position, Vector3.up, Vector3.forward, 360, enemyView.radius);

        Vector3 viewAngle01 = DirectionFromAngle(enemyView.transform.eulerAngles.y, -enemyView.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(enemyView.transform.eulerAngles.y, enemyView.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(enemyView.transform.position, enemyView.transform.position + viewAngle01 * enemyView.radius);
        Handles.DrawLine(enemyView.transform.position, enemyView.transform.position + viewAngle02 * enemyView.radius);

        if (enemyView.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(enemyView.transform.position, enemyView.player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}