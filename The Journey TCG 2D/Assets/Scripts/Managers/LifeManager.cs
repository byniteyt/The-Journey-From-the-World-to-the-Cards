using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int lives;
    static public LifeManager PlayerHealth;
    static public LifeManager EnemyHealth;
    GameObject activeLife;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerHealth = GameObject.Find("PlayerLife").GetComponent<LifeManager>();

        EnemyHealth = GameObject.Find("EnemyLife").GetComponent<LifeManager>();
        EventManager.ChangeLife += ChangeLife;
        EventManager.DealDamage += ChangeLife;
        EventManager.StartTurn += (object caller, System.EventArgs e) => { ChangeActiveLife(); };
        EventManager.StartIATurn += (object caller, System.EventArgs e) => { ChangeActiveLife(); }; 
    }
    public int GetLives()
    {
        return lives;
    }
    // Update is called once per frame
    void ChangeActiveLife()
    {
        if (activeLife == null)
        {
            activeLife = GameObject.Find("EnemyLife");
            return;
        }
        activeLife = (activeLife == GameObject.Find("PlayerLife")) ? 
            GameObject.Find("EnemyLife") 
            : GameObject.Find("PlayerLife");
        Debug.Log($"Active life changed to {activeLife.name}");
    }
    public void ChangeLife(object caller, int amount)
    {
        if (this.gameObject != activeLife) return;
        lives = Mathf.Clamp(lives + amount, 0, int.MaxValue);
        EventManager.UpdateLife?.Invoke(this, lives);
        if (lives <= 0)
        {
            bool playerWon = (gameObject.name != "PlayerLife");
            EventManager.GameOver?.Invoke(this, playerWon);
        }
    }
}
