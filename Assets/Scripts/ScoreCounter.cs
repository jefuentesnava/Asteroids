using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    private PlayerState PlayerState;
    private TMP_Text TextComponent;

    private void Start()
    {
        TextComponent = GetComponent<TMP_Text>();

        //get access to player state
        GameObject playerStateObject = null;
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject g in rootGameObjects)
        {
            if (g.transform.CompareTag("PlayerState"))
            {
                playerStateObject = g;
                break;
            }
        }

        PlayerState = playerStateObject.GetComponent<PlayerState>();
    }

    private void FixedUpdate()
    {
        TextComponent.SetText(PlayerState.Score.ToString());
    }
}