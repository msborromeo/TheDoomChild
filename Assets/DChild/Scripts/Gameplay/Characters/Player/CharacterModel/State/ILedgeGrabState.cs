namespace DChild.Gameplay.Characters.Players.State
{
    public interface ILedgeGrabState
    {
        bool waitForBehaviour { get; set; }
        bool isLedgeGrabbing { get; set; }
        //bool isGrounded { set; }
    }
}
