using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartSinglePlayerMode()
    {
        GlobalState.instance.Reset();
        SceneManager.LoadScene("Level01", LoadSceneMode.Single);
    }
}