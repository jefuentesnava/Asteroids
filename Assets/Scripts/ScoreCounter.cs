using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    private PlayerState playerState;
    private TMP_Text textComponent;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();

        //get access to player state
        GameObject playerStateObject = null;
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject g in rootGameObjects)
        {
            if (g.transform.CompareTag("PlayerState"))
            {
                playerStateObject = g;
            }
        }
        playerState = playerStateObject.GetComponent<PlayerState>();

    }

    private void FixedUpdate()
    {
        textComponent.SetText(playerState.Score.ToString());
    }
}