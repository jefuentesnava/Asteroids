using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartSinglePlayerMode()
    {
        SceneManager.LoadScene("Level01", LoadSceneMode.Single);
    }
}
