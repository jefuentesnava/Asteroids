using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartSinglePlayerMode()
    {
        GlobalState.instance.reset();
        SceneManager.LoadScene("Level01", LoadSceneMode.Single);
    }
}
