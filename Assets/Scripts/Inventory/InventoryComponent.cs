using UnityEngine;
using System.Collections.Generic;
using static ORCAS.Inventory;

namespace ORCAS
{
    public class InventoryComponent : MonoBehaviour
    {
        [SerializeField] private List<ResourceAmount> _initialResources;
        private Inventory _inventory;

        public Inventory Inventory => _inventory;

        private void Awake()
        {
            _inventory = new Inventory(_initialResources);
        }
    }
}