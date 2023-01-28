using System.Collections;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform player;
    [SerializeField] private float time = 0.3f;
    private Vector3 velocity = Vector3.zero;

    //CameraCollision
    private RaycastHit m_Hit;

    public Transform _CamTransform;
    Vector3 _BaseOffset;

    void Start()
    {
        offset = transform.position - player.position;
        _BaseOffset = offset;


    }

    private void Update()
    {

        if (PlayerMovement.s_Instance.m_Hit.distance <= 3)
        {
            TopCamera.enabled = true;
            MainCamera.enabled = false;
        }
        if (PlayerMovement.s_Instance.m_Hit.distance >= 3)
        {

            MainCamera.enabled = true;
            TopCamera.enabled = false;
        }
    }


    IEnumerator HighCamera()
    {
        RaycastHit m_Hit;
        if (Physics.Raycast(transform.position, Vector3.back * 2, out m_Hit))
        {
            if (m_Hit.distance <= 2)
            {
                Debug.Log("On wall");
                transform.position = _CamTransform.transform.position;
                yield return null;

            }

        }

        yield return null;


    }

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, time);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.back * 2);
    }

}
