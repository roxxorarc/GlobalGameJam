using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement s_Instance;

    private Animator _Animator;


    public CharacterController controller;

    public float speed = 60f;
    public Vector3 _Direction;

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

        _Animator = GetComponent<Animator>();

    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        _Animator.SetFloat("LX", x);
        _Animator.SetFloat("LY", z);
/*
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);*/
        
        _Direction = new Vector3(x, 0, z);
        if (_Direction.magnitude > 0.01f)        //Use to keep the last joystick direction
        {
       
            float targetAngle = Mathf.Atan2(_Direction.x, _Direction.z) * Mathf.Rad2Deg;        //Atan2 return angle(positionX --> positionZ)
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            Vector3 dist = _Direction * speed * Time.deltaTime;
            controller.Move(dist);


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.back * 2);
    }

}


