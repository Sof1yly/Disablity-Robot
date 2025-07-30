using UnityEngine;

public abstract class ItemAbility : ScriptableObject
{
    private SoundPlayer soundPlayer;
    public abstract void Activate(GameObject target);
    

}
