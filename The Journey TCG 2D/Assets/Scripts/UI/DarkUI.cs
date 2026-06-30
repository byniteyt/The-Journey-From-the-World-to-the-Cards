using UnityEngine;

public class DarkUI : MonoBehaviour
{
    public static DarkUI Instance { get; private set; }
    RectTransform target;

    public RectTransform top;
    public RectTransform bottom;
    public RectTransform left;
    public RectTransform right;

    [SerializeField] private Canvas canvas;

    private void Start()
    {
        Instance = this;
        HideUI();
    }

    void LateUpdate()
    {
        //Debug.Log("Updating DarkUI...");
        Vector3[] corners = new Vector3[4];
        target.GetWorldCorners(corners);

        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        Vector2 min = RectTransformUtility.WorldToScreenPoint(cam, corners[0]);
        Vector2 max = RectTransformUtility.WorldToScreenPoint(cam, corners[2]);

        float screenW = Screen.width;
        float screenH = Screen.height;

        SetRect(top, new Rect(0, max.y, screenW, screenH - max.y));
        SetRect(bottom, new Rect(0, 0, screenW, min.y));
        SetRect(left, new Rect(0, min.y, min.x, max.y - min.y));
        SetRect(right, new Rect(max.x, min.y, screenW - max.x, max.y - min.y));
    }
    public void SetTarget(RectTransform newTarget)
    {
        if (newTarget == null)
        {
            HideUI();
            return;
        }
        ShowUI();
        target = newTarget;
    }

    public void HideUI()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void SetRect(RectTransform rt, Rect r)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        Vector2 localPosMin;
        Vector2 localPosMax;

        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null
            : canvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            new Vector2(r.xMin, r.yMin),
            cam,
            out localPosMin
        );

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            new Vector2(r.xMax, r.yMax),
            cam,
            out localPosMax
        );

        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);

        rt.anchoredPosition = (localPosMin + localPosMax) / 2f;
        rt.sizeDelta = localPosMax - localPosMin;
    }
}
