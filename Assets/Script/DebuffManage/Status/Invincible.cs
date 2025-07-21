public class Invincible : Status
{
    public override StatusType StatusType => StatusType.Invincible;

    protected override void OffActive()
    {
        //Apply Status
    }

    protected override void OnActive()
    {
        //UnApply Status
    }
}
