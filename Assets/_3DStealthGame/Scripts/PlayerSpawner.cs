using UnityEngine;
using Unity.Cinemachine;

public class ScriptableObjectManagedSpawner : MonoBehaviour
{
    public GameObject ivanPrefab;
    public GameObject johnPrefab;

    public GameObject followCamera;

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (GameSettings.character == 1)
        {
            johnPrefab.SetActive(true);
            ivanPrefab.SetActive(false);

            followCamera.GetComponent<CinemachineCamera>().Follow = johnPrefab.transform;
        }
        else if (GameSettings.character == 2)
        {
            ivanPrefab.SetActive(true);
            johnPrefab.SetActive(false);

            followCamera.GetComponent<CinemachineCamera>().Follow = ivanPrefab.transform;
        }
        else
        {
            Debug.LogError("Invalid character selection in GameSettings.");
            return;
        }
    }
}