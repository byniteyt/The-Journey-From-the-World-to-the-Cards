using UnityEngine;
using System.Collections.Generic;


public static class PlayerProperties 
{
    public static Dictionary<Card, int> cardCollection;

    public static void InitializePlayerProperties()
    {
        /*cardCollection = PlayerPrefs.HasKey("CardCollection") 
            ? JsonUtility.FromJson<Serialization<Card>>(PlayerPrefs.GetString("CardCollection")).ToDictionary() 
            : new Dictionary<Card, int>();*/
    }
}
