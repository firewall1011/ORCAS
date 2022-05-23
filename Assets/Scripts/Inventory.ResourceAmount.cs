namespace ORCAS
{
    public partial class Inventory
    {
        [System.Serializable]
        public struct ResourceAmount
        {
            public Resource Resource;
            public float Amount;

            public ResourceAmount(Resource resource, float amount)
            {
                Resource = resource;
                Amount = amount;
            }

            public void Deconstruct(out Resource resource, out float amount)
            {
                resource = Resource;
                amount = Amount;
            }
        }
    }
}
