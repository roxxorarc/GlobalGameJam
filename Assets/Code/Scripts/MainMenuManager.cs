using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager s_Instance;

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


    public void ChangeLevel(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
         
        
    } 

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit ();
#endif


    }

    
    


}
