using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement s_Instance;




    public CharacterController controller;

    public float speed = 60f;

    public RaycastHit m_Hit;
    private void Awake()
    {

        if (s_Instance == null)
        {
            s_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Physics.Raycast(transform.position, Vector3.back * 2, out m_Hit))
        {
            Debug.Log(m_Hit.distance);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.back * 2);
    }

}


