using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartSinglePlayerMode()
    {
        GlobalState.Instance.Reset();
        SceneManager.LoadScene("Level01", LoadSceneMode.Single);
    }
}