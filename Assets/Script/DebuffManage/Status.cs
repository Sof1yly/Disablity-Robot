using UnityEngine;

public abstract class Status : MonoBehaviour
{
    public virtual StatusType StatusType => StatusType.None; // Declear status type here
    public virtual StatusType NextStatusType => StatusType.None; // None => NO apply any status after this Status End , 
    [SerializeField] float _debuffTime;
    public float DebuffTime => _debuffTime;
    public abstract void OnActive();
    public abstract void OffActive();
}
