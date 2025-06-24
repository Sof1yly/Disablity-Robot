using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    private CapsuleCollider col;

    private Vector3 defaultColCenter;
    private float defaultColHeight;
    private float defaultColRadius;


    [SerializeField] private PlayerMovement playerMovement;

    void Awake()
    {

        col = GetComponent<CapsuleCollider>();

        defaultColCenter = col.center;
        defaultColHeight = col.height;
        defaultColRadius = col.radius;
    }

    void Start()
    {
        UpdateColliderState(true);
    }

    public void UpdateColliderState(bool hasLegs)
    {
        if (hasLegs)
        {

            col.center = defaultColCenter;
            col.height = defaultColHeight;
            col.radius = defaultColRadius;

        }
        else
        {
 
            col.center = new Vector3(defaultColCenter.x, defaultColCenter.y + 0.5f, defaultColCenter.z);   
            col.height = defaultColHeight - 0.75f;  
            col.radius = defaultColRadius - 0.1f;  

        }
    }
}
