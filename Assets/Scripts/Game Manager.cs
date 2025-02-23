using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> locations = new();
    [SerializeField] private Transform player;
    [SerializeField] private GameObject shield;

    private const float LevelLengthIncrement = 106.3f;
    private const int InitialLocationCount = 50;
    private float levelLength = LevelLengthIncrement;

    private void Start()
    {
        Instantiate(locations[0], transform.forward, transform.rotation);

        for (var i = 0; i < InitialLocationCount; i++)
        {
            CreateLocation();
        }

        Invoke(nameof(PlaceShield), Random.Range(20, 30));
    }

    private void Update()
    {
        if (player.position.z > levelLength - LevelLengthIncrement * InitialLocationCount)
        {
            CreateLocation();
        }
    }

    private void CreateLocation()
    {
        Instantiate(locations[Random.Range(0, locations.Count)], transform.forward * levelLength, transform.rotation);
        levelLength += LevelLengthIncrement;
    }

    private void PlaceShield()
    {
        var spawnPosition = player.position + new Vector3(0, 2, Random.Range(30, 70));
        Instantiate(shield, spawnPosition, Quaternion.identity);
        Debug.Log("Shield generated at: " + spawnPosition);
        Invoke(nameof(PlaceShield), Random.Range(5, 10));
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}