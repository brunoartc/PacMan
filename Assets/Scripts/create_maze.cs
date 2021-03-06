﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_maze : MonoBehaviour
{

    public GameObject pellet;
    public GameObject energizer;
    public GameObject ghost;
    public GameObject player;


    private GameObject walls; // hold pillars
    private GameObject pellets; // hold pellets

    // 0 - Empty
    // 2 - Player
    // 3 - Ghost
    // 5 - Pellet
    // 6 - Energizer
    // 8 - Wall
    // 9 - Ghost House Door

    public static readonly int[,] map = {
        {8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8},
        {8,5,5,5,5,5,5,5,5,5,5,5,5,8,8,5,5,5,5,5,5,5,5,5,5,5,5,8},
        {8,5,8,8,8,8,5,8,8,8,8,8,5,8,8,5,8,8,8,8,8,5,8,8,8,8,5,8},
        {8,6,8,8,8,8,5,8,8,8,8,8,5,8,8,5,8,8,8,8,8,5,8,8,8,8,6,8},
        {8,5,8,8,8,8,5,8,8,8,8,8,5,8,8,5,8,8,8,8,8,5,8,8,8,8,5,8},
        {8,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,8},
        {8,5,8,8,8,8,5,8,8,5,8,8,8,8,8,8,8,8,5,8,8,5,8,8,8,8,5,8},
        {8,5,8,8,8,8,5,8,8,5,8,8,8,8,8,8,8,8,5,8,8,5,8,8,8,8,5,8},
        {8,5,5,5,5,5,5,8,8,5,5,5,5,8,8,5,5,5,5,8,8,5,5,5,5,5,5,8},
        {8,8,8,8,8,8,5,8,8,8,8,8,0,8,8,0,8,8,8,8,8,5,8,8,8,8,8,8},
        {0,0,0,0,0,8,5,8,8,8,8,8,0,8,8,0,8,8,8,8,8,5,8,0,0,0,0,0},
        {0,0,0,0,0,8,5,8,8,0,0,0,0,0,0,0,0,0,0,8,8,5,8,0,0,0,0,0},
        {0,0,0,0,0,8,5,8,8,0,8,8,8,9,9,8,8,8,0,8,8,5,8,0,0,0,0,0},
        {8,8,8,8,8,8,5,8,8,0,8,0,0,0,0,0,0,8,0,8,8,5,8,8,8,8,8,8},
        {0,0,0,0,0,0,5,0,0,0,8,0,0,0,0,0,0,8,0,0,0,5,0,0,0,0,0,0},
        {8,8,8,8,8,8,5,8,8,0,8,0,0,0,0,0,0,8,0,8,8,5,8,8,8,8,8,8},
        {0,0,0,0,0,8,5,8,8,0,8,8,8,8,8,8,8,8,0,8,8,5,8,0,0,0,0,0},
        {0,0,0,0,0,8,5,8,8,0,0,0,0,0,0,0,0,0,0,8,8,5,8,0,0,0,0,0},
        {0,0,0,0,0,8,5,8,8,0,8,8,8,8,8,8,8,8,0,8,8,5,8,0,0,0,0,0},
        {8,8,8,8,8,8,5,8,8,0,8,8,8,8,8,8,8,8,0,8,8,5,8,8,8,8,8,8},
        {8,5,5,5,5,5,5,5,5,5,5,5,5,8,8,5,5,5,5,5,5,5,5,5,5,5,5,8},
        {8,5,8,8,8,8,5,8,8,8,8,8,5,8,8,5,8,8,8,8,8,5,8,8,8,8,5,8},
        {8,5,8,8,8,8,5,8,8,8,8,8,5,8,8,5,8,8,8,8,8,5,8,8,8,8,5,8},
        {8,6,5,5,8,8,5,5,5,5,5,5,5,0,2,5,5,5,5,5,5,5,8,8,5,5,6,8},
        {8,8,8,5,8,8,5,8,8,5,8,8,8,8,8,8,8,8,5,8,8,5,8,8,5,8,8,8},
        {8,8,8,5,8,8,5,8,8,5,8,8,8,8,8,8,8,8,5,8,8,5,8,8,5,8,8,8},
        {8,5,5,5,5,5,5,8,8,5,5,5,5,8,8,5,5,5,5,8,8,5,5,5,5,5,5,8},
        {8,5,8,8,8,8,8,8,8,8,8,8,5,8,8,5,8,8,8,8,8,8,8,8,8,8,5,8},
        {8,5,8,8,8,8,8,8,8,8,8,8,5,8,8,5,8,8,8,8,8,8,8,8,8,8,5,8},
        {8,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,8},
        {8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8},
    };

    public static int bound(int dimension)
    {
        return map.GetUpperBound(dimension);
    }

    void CreatePillar(float x, float z)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(x, 1, z);
        cube.transform.localScale = new Vector3(1, 2, 1);
        cube.transform.parent = walls.transform;
        cube.GetComponent<MeshRenderer>().material.color = Color.blue;
    }




    void Start()
    {


        pellets = new GameObject("Pellets");


        // Rescalling Plane to fit Maze
        walls = new GameObject("Walls");
        gameObject.transform.position =
                new Vector3((bound(0) % 2 == 0 ? 0 : 0.5f), 0, (bound(1) % 2 == 0 ? 0 : 0.5f));
        gameObject.transform.localScale =
                new Vector3(1 + ((bound(0) - 9) * 0.1f), 1, 1 + ((bound(1) - 9) * 0.1f));
        gameObject.GetComponent<MeshRenderer>().material.color = Color.black;


        // Filling Maze
        for (int i = 0; i <= bound(0); i++)
        {
            for (int j = 0; j <= bound(1); j++)
            {
                float x = (-bound(0) / 2) + i;
                float z = (-bound(1) / 2) + j;
                if (map[i, j] == 8)
                    CreatePillar(x, z);
                else if (map[i, j] == 5)
                    Instantiate(pellet, new Vector3(x, 1, z), Quaternion.identity, pellets.transform);
                else if (map[i, j] == 6)
                    Instantiate(energizer, new Vector3(x, 1, z), Quaternion.identity, pellets.transform);
                else if (map[i, j] == 2)
                    Instantiate(player, new Vector3(x, 1, z), Quaternion.identity, pellets.transform);
                else if (map[i, j] == 3)
                    Instantiate(ghost, new Vector3(x, 1, z), Quaternion.identity, pellets.transform);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
