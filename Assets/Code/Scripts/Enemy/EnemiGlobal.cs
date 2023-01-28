using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemiGlobal : MonoBehaviour
{
    //Enemy View system
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject player;
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private LayerMask obstructionMask;
    public bool canSeePlayer;
    public bool isPlayerHidden;

    [SerializeField]
    Vector3 origin;

    //Enemi Patrol

    public List<Transform> _AnchorList;

    public float m_Speed = 5;

    [SerializeField] private int m_Index = 0;


    private void Awake()
    {
        //   player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Start()
    {
        transform.position = _AnchorList[0].transform.position;
        StartCoroutine(FOVRoutine());

    }

    private void Update()
    {
        //     StartCoroutine(Move());
        DrawFOV();
    }

    IEnumerator Move()
    {
        transform.LookAt(_AnchorList[m_Index]);
        Vector3 dest = _AnchorList[m_Index].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, dest, m_Speed * Time.deltaTime);
        transform.position = newPos;


        float dist = Vector3.Distance(transform.position, dest);
        if (dist <= 0.01f)
        {
            m_Index++;

        }
        if (m_Index > _AnchorList.Count - 1)
        {
            m_Index = 0;
        }

        yield return null;
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
        isPlayerHidden = player.GetComponent<Interactor>().isHidden;
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
                    GameManager.instance.SetState(GameManager.GameState.Spotted);
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
        int rayCount = 200;
        float angleIncrease = fov / rayCount;


        float angle2 = -fov / 2;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 vertex = DirFromAngle(angle2) * viewDistance;
            RaycastHit hit;
            if (Physics.Raycast(origin, DirFromAngle(angle2), out hit, viewDistance, obstructionMask))
            {
                vertex = hit.point - origin;
            }

            vertices[vertexIndex] = vertex;

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

    }



    public Vector3 DirFromAngle(float angleInDegrees)
    {

        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(angleInRadians), 0, Mathf.Cos(angleInRadians));
    }

}
