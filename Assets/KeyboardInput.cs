using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] float InputDelay = 0.25f;
    [SerializeField] float PressDelay = 0.25f;
    float currentTimer;
    float currenPressTimer;

    [SerializeField] PlayerInput playerInput;
    [SerializeField] Vector2Int currentMoveIndex = Vector2Int.zero;
    [SerializeField] UnityEvent<string> OnUpdateText;
    [SerializeField] UnityEvent OnOffKeyboard;
    Image currentImage;
    string currentText = "";


    private void Start()
    {
        playerInput.SwitchCurrentActionMap("Ui");
        ApplyPosition();
        OnUpdateText?.Invoke(currentText);
    }

    private void Update()
    {
        movePosition();
        pressButton();
    }

    private void movePosition()
    {
        if (Time.time < currentTimer) return;

        InputAction moveAction = playerInput.actions["Move"];

        // Read the value (e.g., Vector2 for movement)
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        int x = moveValue.x > 0.5f ? 1 : moveValue.x < -0.5f ? -1 : 0;
        int y = -(moveValue.y > 0.5f ? 1 : moveValue.y < -0.5f ? -1 : 0);
        Vector2Int moveDir = new Vector2Int(x, y);

        if (moveDir == Vector2Int.zero) return;

        Vector2Int futurePos = moveDir + currentMoveIndex;
        if (futurePos.y < 0 || futurePos.y >= this.transform.childCount)
        {
            futurePos = new Vector2Int(futurePos.x, currentMoveIndex.y);
        }
        Transform row = this.transform.GetChild(futurePos.y);

        if (futurePos.x >= row.childCount || futurePos.x < 0)
        {
            futurePos = new Vector2Int(currentMoveIndex.x, currentMoveIndex.y);

        }


        if (futurePos.x == currentMoveIndex.x && futurePos.y == currentMoveIndex.y) return;

        currentMoveIndex = futurePos;
        currentTimer = Time.time + InputDelay;
        currenPressTimer = 0;
        ApplyPosition();

    }

    private void pressButton()
    {

        InputAction moveAction = playerInput.actions["Press"];

        if (moveAction.IsPressed())
        {
            if (Time.time < currentTimer) return;

            currentTimer = Time.time + PressDelay;
            Transform row = this.transform.GetChild(currentMoveIndex.y);
            RectTransform colume = row.GetChild(currentMoveIndex.x).GetComponent<RectTransform>();
            colume.GetComponent<ButtonAction>().OnPress();
        }
    }

    void ApplyPosition()
    {
        if (currentImage != null) currentImage.color = Color.white;

        Transform row = this.transform.GetChild(currentMoveIndex.y);
        RectTransform colume = row.GetChild(currentMoveIndex.x).GetComponent<RectTransform>();

        currentImage = colume.GetComponent<Image>();
        currentImage.color = Color.gray;
    }

    public void AddLetter(char newLetter)
    {
        currentText += newLetter;
        OnUpdateText?.Invoke(currentText);
    }

    public void RemoveLetter()
    {
        if (currentText.Length == 0) return;

        string tempText = "";
        for (int i = 0; i < currentText.Length - 1; i++)
        {
            tempText += currentText[i];
        }
        currentText = tempText;
        OnUpdateText?.Invoke(currentText);
    }


    public void OnFinish()
    {
        OnOffKeyboard?.Invoke();
        playerInput.SwitchCurrentActionMap("Ui");
        this.gameObject.SetActive(false);
    }
}
