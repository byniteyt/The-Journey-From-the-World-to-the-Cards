using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.ChangeLife += ChangeLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeLife(object caller, int amount)
    {
        lives += amount;
        EventManager.UpdateLife?.Invoke(caller, lives);
        if (lives <= 0)
        {
            GameManager.CurrentGameState = GameState.GameOver;
        }
    }
}
