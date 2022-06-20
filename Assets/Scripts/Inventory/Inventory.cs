using System.Collections.Generic;
using UnityEngine;

namespace ORCAS
{
    [System.Serializable]
    public partial class Inventory
    {
        [SerializeField] private List<ResourceAmount> _resources;

        public Inventory(List<ResourceAmount> resources)
        {
            _resources = resources;
        }

        public bool HasEnough(Resource resource, float amount)
        {
            return GetAmount(resource) >= amount;
        }

        public float GetAmount(Resource resource)
        {
            return _resources.Find((r) => r.Resource == resource).Amount;
        }

        public void Add(Resource resource, float amount)
        {
            if (amount < 0f)
            {
                throw new System.ArgumentOutOfRangeException(nameof(amount), $"Doesn't support negative numbers, use {nameof(UpdateAmountBy)}() instead");
            }

            UpdateAmountBy(resource, amount);
        }

        public bool TryRemove(Resource resource, float amount)
        {
            if (amount < 0f)
            {
                throw new System.ArgumentOutOfRangeException(nameof(amount), $"Doesn't support negative numbers, use {nameof(UpdateAmountBy)}() instead");
            }

            if (!HasEnough(resource, amount))
            {
                return false;
            }

            UpdateAmountBy(resource, -amount);
            return true;
        }

        public void UpdateAmountBy(Resource resource, float amount)
        {
            int index = FindIndex(resource);

            ResourceAmount resourceAmount = _resources[index];
            resourceAmount.Amount += amount;

            _resources[index] = resourceAmount;
        }

        private int FindIndex(Resource resource)
        {
            return _resources.FindIndex((r) => r.Resource == resource);
        }
    }
}
