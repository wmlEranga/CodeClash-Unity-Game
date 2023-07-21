
using UnityEngine;



public class CheckTrigger : MonoBehaviour
{

    public static CheckTrigger instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public bool PerformRaycastForward(float vertical, float horizantal)
    {
        Vector3 direction = Vector3.forward;
        float maxDistance = 1.0f;
        float y = 1.5f;// the desired y-coordinate
        Vector3 raycastOrigin = new Vector3(transform.position.x + horizantal, y, transform.position.z + vertical);
        Debug.Log(raycastOrigin);
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, direction, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.Log("Obstacle detected in forward of game object!");
                return false;
            }
            else if (hit.collider.CompareTag("Star"))
            {
                Debug.Log("Star detected in forward of game object!");
                Destroy(hit.collider.gameObject);

            }
        }
        Debug.Log("NOOOO");
        return true;
    }

    public bool PerformRaycastBackward(float vertical, float horizantal)
    {
        Vector3 direction = -Vector3.forward;
        float maxDistance = 1.0f;
        float y = 1.5f;
        Vector3 raycastOrigin = new Vector3(transform.position.x + horizantal, y, transform.position.z + vertical);
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, direction, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.Log("Obstacle detected in backward of game object!");
                return false;
            }
            else if (hit.collider.CompareTag("Star"))
            {
                Debug.Log("Star detected in backward of game object!");
                Destroy(hit.collider.gameObject);

            }
        }
        Debug.Log("NOOOO");
        return true;
    }

    public bool PerformRaycastRight(float vertical, float horizantal)
    {
        Vector3 direction = Vector3.right;
        float maxDistance = 1.0f;
        float y = 1.5f;
        Vector3 raycastOrigin = new Vector3(transform.position.x + horizantal, y, transform.position.z + vertical);
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, direction, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.Log("Obstacle detected in Right of game object!");
                return false;
            }
            else if (hit.collider.CompareTag("Star"))
            {
                Debug.Log("Star detected in right of game object!");
                Destroy(hit.collider.gameObject);

            }
        }
        Debug.Log("NOOOO");
        return true;
    }

    public bool PerformRaycastLeft(float vertical, float horizantal)
    {
        Vector3 direction = -Vector3.right;
        float maxDistance = 1.0f;
        float y = 1.5f;
        Vector3 raycastOrigin = new Vector3(transform.position.x + horizantal, y, transform.position.z + vertical);
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, direction, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.Log("Obstacle detected in left of game object!");
                return false;
            }
            else if (hit.collider.CompareTag("Star"))
            {
                Debug.Log("Star detected in left of game object!");
                Destroy(hit.collider.gameObject);

            }
        }
        Debug.Log("NOOOO");
        return true;
    }
}