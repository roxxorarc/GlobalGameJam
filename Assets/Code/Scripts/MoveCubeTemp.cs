using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubeTemp : MonoBehaviour
{

    public float m_Speed;

    //Input Controller
    private float m_XInput;
    private float m_ZInput;


    private void Update()
    {

        m_XInput = Input.GetAxis("Horizontal");
        m_ZInput = Input.GetAxis("Vertical");


        Debug.Log(m_XInput);
        Debug.Log(m_ZInput);

    }

}
