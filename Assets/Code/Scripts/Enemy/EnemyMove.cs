using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public List<Transform> _AnchorList;
    public float m_Speed = 5;

    [SerializeField] private int m_Index = 0;

    private void Start()
    {
        transform.position = _AnchorList[0].transform.position;

    }
    private void Update()
    {
        StartCoroutine(Move());

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
}
