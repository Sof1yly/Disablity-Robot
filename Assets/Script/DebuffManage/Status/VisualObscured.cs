using UnityEngine;

public class VisualObscured : Status
{
    public override StatusType StatusType => StatusType.VisualObscured;

    [Header("UI Overlay")]
    [Tooltip("Drag the GameObject you want shown when obscured here")]
    [SerializeField] private GameObject overlayUI;

    protected override void OnActive()
    {
        if (overlayUI != null)
            overlayUI.SetActive(true);
    }

    protected override void OffActive()
    {
        if (overlayUI != null)
            overlayUI.SetActive(false);
    }
}
