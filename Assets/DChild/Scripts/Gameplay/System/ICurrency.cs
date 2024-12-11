using DChild.Gameplay.Trade;
using Holysoft.Event;

namespace DChild.Gameplay.Systems
{
    public struct CurrencyUpdateEventArgs : IEventActionArgs
    {
        public CurrencyUpdateEventArgs(CurrencyType type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }

        public CurrencyType type { get; }
        public int amount { get; }
    }

    public interface ICurrency
    {
        int GetCurrencyAmount(CurrencyType currencyType);
        event EventAction<CurrencyUpdateEventArgs> OnAmountSet;
        event EventAction<CurrencyUpdateEventArgs> OnAmountAdded;
    }
}
