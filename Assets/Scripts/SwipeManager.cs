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
    public Color darkColor;
    public Color lightColor;
    private Vector3 largeScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 smallScale = new Vector3(0.7f, 0.7f, 1.0f);

    public Transform container;
    public Text levelTitle;

    public void Awake() {
        selectedLevelIndex = 0;
        //levelTitle.text

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
            container.localPosition = Vector3.Lerp(container.localPosition, lerpTarget, 10.0f * Time.deltaTime);
            if (Vector3.Distance(container.localPosition, lerpTarget) < 0.01f) {
                lerp = false;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        lerp = false;
        touchStart = eventData.position;
        touchOrig = touchStart;
        //Need to change startPos to current button's position, not container's position
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
        int nextIndex = Mathf.Clamp(Mathf.RoundToInt(-transform.localPosition.x / buttonSpacing), 0, transform.childCount - 1);
        if (nextIndex != selectedLevelIndex) {
            prevSelectedLevelIndex = selectedLevelIndex;
        }
        selectedLevelIndex = nextIndex;
        //currentLevelTitle.text = (container.transform.GetChild(selectedLevelIndex).GetComponent<LevelButton>().levelName);
    }
}