using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LivesCounter : MonoBehaviour
{
    private PlayerState playerState;
    private TMP_Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = transform.Find("Life Counter").GetComponent<TMP_Text>();

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

        if (playerStateObject != null)
        {
            playerState = playerStateObject.GetComponent<PlayerState>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (textComponent != null)
        {
            textComponent.SetText(playerState.ExtraLives.ToString());
        }
    }
}
