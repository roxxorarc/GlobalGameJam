using System.Collections;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform player;
    [SerializeField] private float time = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public Camera MainCamera;
    public Camera TopCamera;


    void Start()
    {
        offset = transform.position - player.position;
        MainCamera.enabled = true;
        TopCamera.enabled = false;


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
            //            Debug.Log("main camera");
            MainCamera.enabled = true;
            TopCamera.enabled = false;
        }
    }




    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, time);
    }


}