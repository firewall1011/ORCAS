using System;

namespace ORCAS
{
    [System.Serializable]
    public class ResourceReward : IRewardable
    {
        public Resource Resource;
        public float Delta;

        public ResourceReward(Resource type, float delta)
        {
            Resource = type;
            Delta = delta;
        }

        public void Deconstruct(out Resource type, out float amount)
        {
            type = Resource;
            amount = Delta;
        }

        public void ApplyReward(Agent agent)
        {
            var inventory = agent.GetComponent<InventoryComponent>().Inventory;

            inventory.UpdateAmountBy(Resource, Delta);
        }

        public float GetAppliedValue(Agent agent)
        {
            var inventory = agent.GetComponent<InventoryComponent>().Inventory;

            var result = inventory.GetAmount(Resource) + Delta;
            return result;
        }

        public float GetCurrentValue(Agent agent)
        {
            var inventory = agent.GetComponent<InventoryComponent>().Inventory;
            return inventory.GetAmount(Resource);
        }

        public float GetScore(Agent agent, Func<float, float> atenuationFunc)
        {
            float pastValue = GetCurrentValue(agent);
            float newValue = GetAppliedValue(agent);

            return atenuationFunc(pastValue) - atenuationFunc(newValue);
        }
    }
}