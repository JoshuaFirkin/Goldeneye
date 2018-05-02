using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public GameObject goldenGunPrefab;
    public Transform[] spawnPoints;

    private List<Transform> possibleSpawnPoints = new List<Transform>();

    private void Start()
    {
        foreach (Transform point in spawnPoints)
        {
            possibleSpawnPoints.Add(point);
        }

        //InstancePlayer(4);
    }

    public List<GameObject> InstancePlayer(int playerCount)
    {
        if (playerCount <= 0)
            return null;

        List<GameObject> players = new List<GameObject>();
        List<Camera> playerCams = new List<Camera>();

        for (int i = 0; i < playerCount; i++)
        {
            // PLACEHOLDER SPAWN POSITION
            Vector3 spawnPos = new Vector3(Random.Range(-12.5f, 12.5f), 1.0f, Random.Range(-8.0f, 12.5f));

            GameObject player = Instantiate(playerPrefabs[i], spawnPos, Quaternion.identity);
            player.name = "Player_" + (i + 1);
            player.GetComponentInChildren<PlayerController>().AssignControllerMap(i + 1);

            Camera cam = player.transform.Find("Camera").GetComponent<Camera>();

            if (cam != null)
            {
                switch (i)
                {
                    case 0:
                        cam.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                        break;

                    case 1:
                        cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                        break;

                    case 2:
                        cam.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
                        break;

                    case 3:
                        cam.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
                        break;
                }
            }

            players.Add(player);
        }

        return players;
    }


    public void MovePlayerToPoint(Transform player)
    {
        player.position = new Vector3(possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Count)].position.x, transform.position.y + 0.5f, possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Count)].position.z);
    }

    public void SpawnGoldenGun()
    {
        Vector3 spawnPos = new Vector3(possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Count)].position.x, 1.0f, possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Count)].position.z);
        Instantiate(goldenGunPrefab, spawnPos, Quaternion.identity);
    }
}
