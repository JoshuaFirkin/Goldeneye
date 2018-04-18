﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;

    private List<Transform> possibleSpawnPoints = new List<Transform>();

    private void Start()
    {
        foreach (Transform point in spawnPoints)
        {
            possibleSpawnPoints.Add(point);
        }

        InstancePlayer(4);
    }

    void InstancePlayer(int playerCount)
    {
        if (playerCount <= 0)
            return;

        List<Camera> playerCams = new List<Camera>();

        for (int i = 0; i < playerCount; i++)
        {
            // PLACEHOLDER SPAWN POSITION
            Vector3 spawnPos = new Vector3(Random.Range(-12.5f, 12.5f), 1.0f, Random.Range(-8.0f, 12.5f));

            GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
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
        }

        //for (int i = 0; i < playerCams.Count; i++)
        //{
        //    Debug.Log(playerCams[i].transform.parent.name);
        //    switch (i)
        //    {
        //        case 1:
        //            playerCams[i].rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
        //            break;

        //        case 2:
        //            playerCams[i].rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
        //            break;

        //        case 3:
        //            playerCams[i].rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
        //            break;

        //        case 4:
        //            playerCams[i].rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
        //            break;
        //    }
        //}
    }
}