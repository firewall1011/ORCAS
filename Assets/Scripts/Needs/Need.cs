namespace ORCAS
{
    public struct Need
    {
        public NeedType Type;
        public float Amount;

        public Need(NeedType type, float amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}