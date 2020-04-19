using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    //properties
    public bool isWrappingHorizontally { get; private set; } = false;
    public bool isWrappingVertically { get; private set; } = false;
    public float WrappingTimeOut { get; set; } = 2f;
    public float WrappingTimer { get; private set; } = 0.0f;

    void Update()
    {
        screenWrap();
    }

    private bool isVisible()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            if (r.isVisible)
            {
                return true;
            }
        }

        return false;
    }

    private void screenWrap()
    {
        Camera camera = Camera.main;
        Vector3 viewportPosition = camera.WorldToViewportPoint(transform.position);
        Vector3 newPosition = transform.position;

        //if object is visible, no wrapping is necessary
        if (isVisible())
        {
            isWrappingHorizontally = false;
            isWrappingVertically = false;
            return;
        }

        //if object is currently wrapping, check for timeout
        if (isWrappingHorizontally && isWrappingVertically)
        {
            WrappingTimer += Time.deltaTime;

            if (WrappingTimer > WrappingTimeOut)
            {
                //return asteroid to viewport
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 1f));

                //reset timer
                WrappingTimer = 0f;
            }

            return;
        }

        //detect whether object has disappeared horizontally...
        if (!isWrappingHorizontally && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
            isWrappingHorizontally = true;
        }

        //...or vertically
        if (!isWrappingVertically && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;
            isWrappingVertically = true;
        }

        transform.position = newPosition;
    }
}
