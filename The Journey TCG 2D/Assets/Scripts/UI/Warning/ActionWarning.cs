using System;
using TMPro;
using UnityEngine;

public class ActionWarning : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI warning;
    Action actionToComplete;

    void Start()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localPosition = Vector3.zero;
        EventManager.GetOrder += ReceiveAction;
        EventManager.AddWarningDetails += AddDetails;
    }
    void AddDetails(object sender, string details)
    {
        Debug.Log("Adding details to warning: " + details);
        warning.text += details;
    }
    public void Confirm()
    {
        if (actionToComplete==null )
        {
            Debug.Log("No action assigned to complete.");
            Destroy(this.gameObject);
            return;

        }
        actionToComplete.Invoke();
        Destroy(this.gameObject);
    }
    public void Cancel()
    {
        Destroy(this.gameObject);
    }
    void ReceiveAction(object sender, Action action)
    {
        actionToComplete = action;
    }
}
