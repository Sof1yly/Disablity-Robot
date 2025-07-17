using DavidJalbert;
using UnityEngine;

public class TinyCarFOV : MonoBehaviour
{
    public TinyCarController carController;
    public AnimationCurve velocityToFOV = new AnimationCurve(
        new Keyframe(0, 60),
        
        new Keyframe(100, 80)
    );
    public float fovLerpSpeed = 5f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (carController == null) return;

        float speed = Mathf.Abs(carController.getForwardVelocity());
        float targetFOV = velocityToFOV.Evaluate(speed);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);
    }
}
