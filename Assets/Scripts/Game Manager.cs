using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> locations = new List<GameObject>(); 
    [SerializeField] Transform player; // Referencja do gracza
    [SerializeField] GameObject shield; // Referencja do prefabu tarczy

    float levelLength = 106.3f; // Długość poziomu
    int count = 50; // Liczba generowanych obiektów poziomu

    void Start()
    {
        // Generowanie pierwszej lokacji
        Instantiate(locations[0], transform.forward, transform.rotation);

        // Generowanie kolejnych lokacji
        for (int i = 0; i < count; i++)
        {
            CreateLocation();
        }

        // Pierwsze wywołanie funkcji generującej tarczę
        Invoke("GenerateObject", Random.Range(20, 30));
    }

    void Update()
    {
        // Sprawdzanie, czy trzeba wygenerować nową lokację
        if (player.position.z > levelLength - 106.3f * count)
        {
            CreateLocation();
        }
    }

    void CreateLocation()
    {
        // Generowanie losowej lokacji
        Instantiate(locations[Random.Range(0, locations.Count)], transform.forward * levelLength, transform.rotation);
        levelLength += 106.3f;
    }

    void GenerateObject()
    {
        // Generowanie tarczy w losowej odległości przed graczem
        float distance = Random.Range(30, 70);
        Vector3 spawnPosition = player.position + new Vector3(0, 2, distance);

        Instantiate(shield, spawnPosition, Quaternion.identity);

        // Debugowanie, aby upewnić się, że tarcza została wygenerowana
        Debug.Log("Shield generated at: " + spawnPosition);

        // Ponowne wywołanie funkcji po losowym czasie
        Invoke("GenerateObject", Random.Range(5, 10));
    }

    public void RestartGame()
    {
        // Restart aktualnej sceny
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
