﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Player[] faces;

    [HideInInspector]
    public int player1Face, player2Face;

    List<Player> players;
    GameObject[] spawnPoints1, spawnPoints2;
    UIManager UIM;

    public static GameManager instance;

    void Awake()
    {
        if (!instance)
            instance = this;
        if (instance != this)
            Destroy(gameObject);
        StartUp();
    }

    void OnLevelWasLoaded()
    {
        StartUp();
    }

    void StartUp()
    {
        UIM = GameObject.Find("UIManager").GetComponent<UIManager>();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Arena":
                players = new List<Player>();
                SpawnPlayers();
                break;
        }
    }

    void SpawnPlayers()
    {
        spawnPoints1 = GameObject.FindGameObjectsWithTag("SpawnPoint1");
        spawnPoints2 = GameObject.FindGameObjectsWithTag("SpawnPoint2");
        int index = Random.Range(0, spawnPoints1.Length);

        Player p1 = (Player)Instantiate(faces[player1Face], 
            spawnPoints1[index].transform.position, Quaternion.identity);

        Player p2 = (Player)Instantiate(faces[player2Face], 
            spawnPoints2[index].transform.position, Quaternion.identity);
        
        p1.useSecondaryControls = true;
        p1.healthSlider = UIM.p1Health;
        p1.powerSlider = UIM.p1Power;
        p2.healthSlider = UIM.p2Health;
        p2.powerSlider = UIM.p2Power;
        p1.SetControls();
        p2.SetControls();
        players.Add(p1);
        players.Add(p2);
    }

    public void GameOver(Player loser)
    {
        int i = players.IndexOf(loser);
        Player winner;
        if (i == 1)
            winner = players[0];
        else if (i == 0)
            winner = players[1];
        else winner = players[0];
        winner.Win();
    }
}
