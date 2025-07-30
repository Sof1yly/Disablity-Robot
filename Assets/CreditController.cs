using NUnit.Framework.Internal.Commands;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CreditController : MonoBehaviour
{
    [SerializeField] float maxSkip = 2f;
    float currentSkip = 0;
    float x4Delay = 0;
    [SerializeField] Image x2UI;
    [SerializeField] Image SkipUI;
    [SerializeField] Image SkipUIBar;
    [SerializeField] TMP_Text x2x4;
    void Update()
    {
        bool speedKeyPressed = Keyboard.current.spaceKey.isPressed;
        bool skipKeyPressed = Keyboard.current.escapeKey.isPressed;

        if (Gamepad.current != null)
        {
            speedKeyPressed = Keyboard.current.spaceKey.isPressed || Gamepad.current.rightTrigger.isPressed;
            skipKeyPressed = Keyboard.current.escapeKey.isPressed || Gamepad.current.aButton.isPressed;
        }
        

        if (speedKeyPressed)
        {
            Debug.Log("Space");
            if (x4Delay <= 5f)
            {
                Time.timeScale = 2;
                x4Delay += Time.deltaTime;
                if (!x2UI.isActiveAndEnabled)
                {
                    x2UI.gameObject.SetActive(true);
                    x2x4.text = "x2";
                }
            }
            else
            {
                Time.timeScale = 4;
                x2x4.text = "x4";
            }
        }
        else
        {
            Time.timeScale = 1;
            x4Delay = 0;
            x2UI.gameObject.SetActive(false);

        }

        if (skipKeyPressed)
        {
            Debug.Log("Esc");
            currentSkip += Time.deltaTime;
            if (currentSkip > maxSkip)
            {
                Skip();
            }
            SkipUI.gameObject.SetActive(true);
        }
        else
        {
            if (currentSkip > 0)
            {
                currentSkip -= Time.deltaTime;
            }
            else { SkipUI.gameObject.SetActive(false); }
        }

        SkipUIBar.fillAmount = currentSkip/maxSkip;
    }

    void Skip()
    {
        Debug.Log("Skip");
    }
}
