public class Iframe : Status
{
    public override StatusType StatusType => StatusType.Iframe;
    protected override void OnActive()
    {
        //apply visual effect
    }
    protected override void OffActive()
    {
        //unapply visual effect 
    }
}
