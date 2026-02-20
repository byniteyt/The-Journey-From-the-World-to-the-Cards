using UnityEngine;
using UnityEngine.UI;

public class StackManager : MonoBehaviour
{
    public static StackManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DesactivatedSprite();
        }
        else
        {
            Destroy(gameObject);
        }
        EventManager.StartIATurn += (s, e) => { DesactivatedSprite(); };
        EventManager.StartTurn += (s, e) => { DesactivatedSprite(); };
    }
    public void AddToStack(BattleCard card)
    {
        GetComponentInChildren<Image>().enabled = true;
        GetComponentInChildren<Animator>().SetBool("Added", true);
        // Implement logic to add the effect to the stack
        Debug.Log($"Added {card.name} to the stack.");
        GetComponentInChildren<Image>().sprite = card.GetCard().artwork;
        //GetComponentInChildren<Animator>().SetBool("Added", false);
    }
    void DesactivatedSprite()
    {
        GetComponentInChildren<Image>().enabled = false;
    }

    public void EndCard()
    {
        GetComponentInChildren<Animator>().SetBool("Added", false);
    }   
}
