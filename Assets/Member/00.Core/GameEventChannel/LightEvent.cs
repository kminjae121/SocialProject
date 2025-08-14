namespace Core.Events
{
    public static class LightEvent
    {
        public static TurnOffTheLight lightEvent = new TurnOffTheLight();
    }

    public class TurnOffTheLight : GameEvent
    {
        public bool isTurnOff;

        public TurnOffTheLight Initialize(bool isTurnOff)
        {
            this.isTurnOff = isTurnOff;
            return this;
        }
    }
}