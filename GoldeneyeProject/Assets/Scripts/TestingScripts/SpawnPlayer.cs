using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    RIFLE,
    SHOTGUN,
    ROCKET_LAUNCHER,
    PISTOL,
}

public class SpawnPlayer : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public GameObject goldenGunPrefab;
    public Transform[] spawnPoints;
    public Transform[] weaponSpawnPoints;
    public GameObject[] weaponTypes;

    private GameObject weaponToSpawn;

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
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Vector3 spawnPos = new Vector3(point.position.x, point.position.y, point.position.z);

            GameObject player = Instantiate(playerPrefabs[i], spawnPos, Quaternion.identity);
            player.name = "Player_" + (i + 1);
            player.GetComponentInChildren<PlayerController>().AssignControllerMap(i + 1);

            Camera cam = player.transform.Find("Camera").GetComponent<Camera>();

            if (cam != null)
            {
                switch (playerCount)
                {
                    case 1:
                        cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                        break;


                    case 2:
                        switch (i)
                        {
                            case 0:
                                cam.rect = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
                                break;

                            case 1:
                                cam.rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
                                break;
                        }
                        break;


                    case 3:
                        switch (i)
                        {
                            case 0:
                                cam.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                                break;

                            case 1:
                                cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                                break;

                            case 2:
                                cam.rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
                                break;
                        }
                        break;


                    case 4:
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


    public void SpawnWeaponOfType(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.ROCKET_LAUNCHER:
                foreach (GameObject weap in weaponTypes)
                {
                    if (weap.GetComponent<RocketLauncher>() != null)
                    {
                        weaponToSpawn = weap;
                    }
                }
                break;
        }

        foreach (Transform point in weaponSpawnPoints)
        {
            Instantiate(weaponToSpawn, point.position, point.rotation);
        }
    }
}
