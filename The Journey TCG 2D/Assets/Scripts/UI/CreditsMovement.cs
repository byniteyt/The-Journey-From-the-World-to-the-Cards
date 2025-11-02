using UnityEngine;

public class CreditsMovement : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localPosition = (
        rectTransform.offsetMin.y<=0)?
            new Vector3(transform.localPosition.x, 
            rectTransform.localPosition.y + speed* Time.deltaTime, 
            rectTransform.localPosition.z):
            rectTransform.localPosition;
        if (rectTransform.offsetMin.y > 0)
            this.gameObject.GetComponent<CreditsMovement>().enabled = false;
    }
}
