using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;
    public Interactor player;
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private LayerMask obstructionMask;
    public bool canSeePlayer;
    public bool isPlayerHidden;

    [SerializeField]
    Vector3 origin;

    [SerializeField]
    Vector3 modular;

    private void Start()
    {
        StartCoroutine(FOVRoutine());

    }
    private void Update()
    {
        DrawFOV();
        isPlayerHidden = player.isHidden;
    }
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask) && !isPlayerHidden)
                {
                    canSeePlayer = true;
                    GameManager.instance.SwitchState(GameManager.GameState.Spotted);
                }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void DrawFOV()
    {

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        float fov = angle;
        float viewDistance = radius;
        origin = this.transform.position;
        int rayCount = 50;
        float angleIncrease = fov / rayCount;


        float angle2 = -fov / 2;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 vertex = transform.TransformDirection(DirFromAngle(angle2)) * viewDistance;
            RaycastHit hit;

            float rotation = transform.rotation.eulerAngles.y;
            if (Physics.Raycast(origin, transform.TransformDirection(DirFromAngle(angle2)), out hit, viewDistance, obstructionMask))
            {
                vertex = (hit.point - origin);
            }
            vertices[vertexIndex] = Quaternion.Euler(0, -rotation, 0) * vertex;



            if (i >= 1)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;

            angle2 += angleIncrease;

        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

    }



    public Vector3 DirFromAngle(float angleInDegrees)
    {

        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(angleInRadians), 0, Mathf.Cos(angleInRadians));
    }

}
