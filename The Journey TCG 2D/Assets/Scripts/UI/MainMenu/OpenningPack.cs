using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenningPack : MonoBehaviour
{
    GameObject exit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        exit = GetComponentInChildren<Button>().gameObject;
        EventManager.OpenPack += OpenPack;
    }

    void OnDestroy()
    {
        EventManager.OpenPack -= OpenPack;
    }

    void OpenPack(object sender, BasePack pack)
    {
        StartCoroutine(ShowCard(pack));
    }
    IEnumerator ShowCard(BasePack pack, int index = 0)
    {
        exit.GetComponent<Button>().enabled = false;
        GameObject cardButton = Resources.Load<GameObject>($"Prefabs/Collections/{pack.GetCollection().collectionName}/plantilla");
        if (cardButton == null)
        {
            Debug.LogError("Card button prefab not found!");
            yield return null;
        }
        if (index < pack.GetCardsInPack().Count)
        {
            BattleCard card = pack.GetCardsInPack()[index];
            GameObject cardAsset = Instantiate(cardButton, GetComponentInChildren<GridLayoutGroup>().gameObject.transform);
            cardAsset.gameObject.name = card.name;
            cardAsset.transform.GetChild(0).GetComponent<Image>().sprite = card.GetCard().artwork;
            cardAsset.AddComponent<BoxCollider2D>();
            switch (card.GetType().ToString())
            {
                case "RoomCard":
                    cardAsset.AddComponent<BattleRoomCard>().SetCard(card.GetCard());
                    break;
                case string s when s.Contains("Spell"):
                    cardAsset.AddComponent<BattleSpellCard>().SetCard(card.GetCard());
                    break;
                case "CharacterCard":
                    cardAsset.AddComponent<BattleCharCard>().SetCard(card.GetCard());
                    break;
                default:
                    Debug.Log($"Adding {card.name} as a Unknown card type");
                    break;
            }
            cardAsset.transform.localScale = new Vector3(0, 1, 1);
            while (cardAsset.transform.localScale.x < 1)
            {
                cardAsset.transform.localScale += new Vector3(0.1f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
            CardCollection.AddCard(card.GetCard());
            index++;
            yield return new WaitForSeconds(0.7f);
            StartCoroutine(ShowCard(pack, index));
            StopCoroutine(ShowCard(pack, index-1));
        }
        else
        {
            Debug.Log("All cards shown");
            exit.GetComponent<Button>().enabled = true;
            StopCoroutine(ShowCard(pack, index));
        }
            
        yield return null;
    }
    public void Close()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
