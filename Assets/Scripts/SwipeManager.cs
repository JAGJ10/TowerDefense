using UnityEngine;
using UnityEngine.UI;

public class SwipeManager : MonoBehaviour {
    [SerializeField]
    private RectTransform panel;
    [SerializeField]
    private Button[] buttons;

    private bool isDragging = false;
    private int buttonIndex;
    private int buttonDistance;

    public void Start() {
        buttonDistance = (int)Mathf.Abs(buttons[1].GetComponent<RectTransform>().anchoredPosition.x - buttons[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    public void Update() {
        if (!isDragging) {
            float newX = Mathf.Lerp(panel.anchoredPosition.x, buttonIndex * -buttonDistance, Time.deltaTime * 5.0f);
            panel.anchoredPosition = new Vector2(newX, panel.anchoredPosition.y);
        }
    }

    public void StartDrag() {
        isDragging = true;
    }

    public void EndDrag() {
        isDragging = false;

        int idx = (int)panel.anchoredPosition.x / buttonDistance;
        buttonIndex = panel.anchoredPosition.x < 0 ? -idx : 0;
        buttonIndex = Mathf.Min(buttons.Length - 1, buttonIndex);
    }
}
