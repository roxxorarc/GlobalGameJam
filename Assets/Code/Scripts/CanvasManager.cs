using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager s_Instance;


    public GameObject m_HUDMenu;
    public GameObject m_GameOver;
    public GameObject m_PauseMenu;
    public GameObject m_Ending;


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


    public void SetHUDMenu(bool enable)
    {
        m_HUDMenu.SetActive(enable);
    }

    public void SetPauseMenu(bool enable)
    {
        m_PauseMenu.SetActive(enable);
    }

    public void SetGameOver(bool enable)
    {
        m_GameOver.SetActive(enable);
    }

    public void SetEnding(bool enable)
    {
        m_Ending.SetActive(enable);
    }

}
