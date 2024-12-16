namespace DChild.Gameplay.FastTravel
{
    public static class FastTravelUtility
    {
        public static string GenerateActivationVariableName(FastTravelData data)
        {
            return $"FastTravel_{data.name}_isActivated";
        }
    }
}
