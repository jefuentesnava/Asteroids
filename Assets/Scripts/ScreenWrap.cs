using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    public bool IsWrappingHorizontally { get; private set; } = false;
    public bool IsWrappingVertically { get; private set; } = false;
    public float WrappingTimeOut { get; set; } = 2f;
    public float WrappingTimer { get; private set; } = 0.0f;

    private void FixedUpdate()
    {
        WrapObject();
    }

    private bool IsVisible()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            if (r.isVisible) return true;
        }

        return false;
    }

    private void WrapObject()
    {
        var camera = Camera.main;
        var viewportPosition = camera.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        //if object is visible, no wrapping is necessary
        if (IsVisible())
        {
            IsWrappingHorizontally = false;
            IsWrappingVertically = false;
            return;
        }

        //if object is currently wrapping, check for timeout
        if (IsWrappingHorizontally && IsWrappingVertically)
        {
            WrappingTimer += Time.fixedDeltaTime;

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
        if (!IsWrappingHorizontally && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
            IsWrappingHorizontally = true;
        }

        //...or vertically
        if (!IsWrappingVertically && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;
            IsWrappingVertically = true;
        }

        transform.position = newPosition;
    }
}