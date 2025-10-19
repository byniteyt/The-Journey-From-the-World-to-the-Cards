using System;
using TMPro;
using UnityEngine;

public class GameTextManager : MonoBehaviour
{
    GameObject playerLife;
    GameObject enemyLive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLife = GameObject.Find("PlayerLifeText");
        enemyLive = GameObject.Find("EnemyLifeText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdatePlayerLife(int life)
    {
        playerLife.GetComponent<TextMeshProUGUI>().text = "Player Life: " + life;
    }
    void UpdateEnemyLife(int life)
    {
        enemyLive.GetComponent<TextMeshProUGUI>().text = "Enemy Life: " + life;
    }
}
