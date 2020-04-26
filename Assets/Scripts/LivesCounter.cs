using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesCounter : MonoBehaviour
{
    private PlayerState PlayerState;
    private TMP_Text TextComponent;

    private void Start()
    {
        TextComponent = transform.Find("Life Counter").GetComponent<TMP_Text>();

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
        TextComponent.SetText(PlayerState.ExtraLives.ToString());
    }
}