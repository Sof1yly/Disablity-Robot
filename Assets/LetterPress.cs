public class LetterPress : ButtonAction
{



    public override void OnPress()
    {

        keyboard.AddLetter(this.gameObject.name[0]);
    }
}
