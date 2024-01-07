using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool ShouldQuitGame => Input.GetKeyUp(KeyCode.Escape);
    public GameObject cabine1;
    public GameObject cabine2;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        cabine2.SetActive(false);
    }

    void Update()
    {
        if (ShouldQuitGame)
        {
            QuitGame();
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            cabine1.SetActive(true);
            cabine2.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            cabine1.SetActive(false);
            cabine2.SetActive(true);
        }
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // todo handle WebGL
        Application.Quit();
#endif
    }
}
