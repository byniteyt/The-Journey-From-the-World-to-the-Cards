using TMPro;
using UnityEngine;

public class CreditsMovement : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] float speed;
    float movSpeed;
    int change = 0;
    float[] timer;
    [SerializeField] private TextMeshProUGUI end;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        end.gameObject.SetActive(false);
        timer = new float[3];
        movSpeed = speed;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localPosition = (
        rectTransform.offsetMin.y<=0)?
            new Vector3(transform.localPosition.x, 
            rectTransform.localPosition.y + movSpeed * Time.deltaTime, 
            rectTransform.localPosition.z):
            rectTransform.localPosition;
        timer[change] += Time.deltaTime*(change+1);
        if (rectTransform.offsetMin.y > 0)
        {
            EndCretids();
            this.gameObject.GetComponent<CreditsMovement>().enabled = false;
        }
    }
    public void MultiplySpeed(float multiplier)
    {
        movSpeed = speed * multiplier;
        change++;
        if (change > 2)
        {
            change = 0;
        }
    }

    void EndCretids()
    {
        int index;
        if (timer[0] > timer[1] && timer[0] > timer[2]) index = 0;
        else if (timer[1] > timer[2]) index = 1;
        else index = 2;
        switch (index)
        {
            case 0:
                end.text = "Thank you for playing\nand watching all the credits!";
                break;
            case 1:
                end.text = "Thank you for playing!";
                break;
            default:
                end.text = "Don't know why you started this scene\nif you skipped the credits >:(";
                break;
        }
        
        end.gameObject.SetActive(true);
    }
}
