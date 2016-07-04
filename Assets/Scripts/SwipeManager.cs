using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwipeManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private Vector2 touchStart, touchEnd, touchOrig;
    private float buttonSpacing = 250.0f;
    private float clampSpeed = 0.3f;
    private float containerWidth, totalSlide;
    private int selectedLevelIndex;
    private int prevSelectedLevelIndex = -1;
    private bool finishedClamp = true;
    private bool lerp, disableDrag = false;
    private Vector3 lerpTarget, startPos;

    //Fade in/out button variables
    private Color darkColor = new Color(0.25f, 0.25f, 0.25f);
    private Color lightColor = new Color(1.0f, 1.0f, 1.0f);
    private Vector3 largeScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 smallScale = new Vector3(0.7f, 0.7f, 1.0f);

    public Transform container;
    public Text levelTitle;

    public void Awake() {
        selectedLevelIndex = 0;
        levelTitle.text = container.transform.GetChild(selectedLevelIndex).GetComponent<LevelButton>().levelName;

        int numLevels = container.childCount;

        //Space level buttons equidistant from each other
        for (int i = 1; i < numLevels; i++) {
            Transform prevButton = container.GetChild(i - 1);
            container.GetChild(i).transform.localPosition = new Vector3(prevButton.transform.localPosition.x + buttonSpacing, 0, 0);
        }

        containerWidth = container.GetChild(numLevels - 1).transform.localPosition.x - container.GetChild(0).transform.localPosition.x;
    }

    public void Update() {
        if (lerp) {
            container.localPosition = Vector3.Lerp(container.localPosition, lerpTarget, 7.5f * Time.deltaTime);
            int dir = container.localPosition.x > lerpTarget.x ? 1 : -1;
            //Fade(dir);
            if (Mathf.Abs(container.localPosition.x - lerpTarget.x) < 0.1f) {
                if (dir > 0) {
                    container.localPosition = new Vector3(Mathf.Floor(container.localPosition.x), container.localPosition.y);
                } else {
                    container.localPosition = new Vector3(Mathf.Ceil(container.localPosition.x), container.localPosition.y);
                }
                //Fade(-dir);
                selectedLevelIndex = -(int)container.localPosition.x / (int)buttonSpacing;
                levelTitle.text = (container.transform.GetChild(selectedLevelIndex).GetComponent<LevelButton>().levelName);
                lerp = false;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        lerp = false;
        touchStart = eventData.position;
        touchOrig = touchStart;
        startPos = container.localPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        touchEnd = eventData.position;
        SlideContainer(touchEnd.x - touchStart.x);
        touchStart = touchEnd;
    }

    public void OnEndDrag(PointerEventData eventData) {
        totalSlide = 0.0f;
        lerp = true;
        disableDrag = true;
        touchEnd = eventData.position;
        float slideAmount = touchEnd.x - touchOrig.x;
        if (slideAmount > 0.0f) lerpTarget = startPos + new Vector3(buttonSpacing, 0, 0);
        else lerpTarget = startPos - new Vector3(buttonSpacing, 0, 0);
    }

    private void SlideContainer(float slideAmount) {
        totalSlide += slideAmount;
        if (Mathf.Abs(totalSlide) < buttonSpacing) container.Translate(slideAmount, 0.0f, 0.0f);
        container.localPosition = new Vector2(Mathf.Clamp(container.localPosition.x, startPos.x - buttonSpacing, startPos.x + buttonSpacing), container.localPosition.y);
        int dir = container.localPosition.x < startPos.x ? 1 : -1;
        Fade(dir);
    }

    private void Fade(int dir) {
        float t;
        if (dir > 0) {
            float shift = selectedLevelIndex * buttonSpacing;
            t = (-container.localPosition.x - shift) / buttonSpacing;
            container.transform.GetChild(selectedLevelIndex + 1).GetComponent<Image>().color = Color.Lerp(darkColor, lightColor, t);
            container.transform.GetChild(selectedLevelIndex).GetComponent<Image>().color = Color.Lerp(darkColor, lightColor, 1-t);
        } else {
            float shift = (selectedLevelIndex - 1) * buttonSpacing;
            t = (-container.localPosition.x - shift) / buttonSpacing;
            container.transform.GetChild(selectedLevelIndex).GetComponent<Image>().color = Color.Lerp(darkColor, lightColor, t);
            container.transform.GetChild(selectedLevelIndex - 1).GetComponent<Image>().color = Color.Lerp(darkColor, lightColor, 1-t);
        }
    }
}