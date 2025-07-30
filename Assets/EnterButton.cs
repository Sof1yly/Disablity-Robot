public class EnterButton : ButtonAction
{
    public override void OnPress()
    {
        keyboard.OnFinish();
    }
}
