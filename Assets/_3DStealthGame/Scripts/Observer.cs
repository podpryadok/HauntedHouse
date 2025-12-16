using UnityEngine;

public class Observer : MonoBehaviour
{
    private GameObject playerPrefab;
    bool m_IsPlayerInRange;
    public GameEnding gameEnding;

    void Start()
    {
        playerPrefab = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.transform == playerPrefab.transform)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.transform == playerPrefab.transform)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update ()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = playerPrefab.transform.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == playerPrefab.transform)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}