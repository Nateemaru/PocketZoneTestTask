using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.UI
{
    public class InventoryView : MonoBehaviour
    {
        private List<InventorySlotView> _slotsViews = new List<InventorySlotView>();

        public List<InventorySlotView> SlotsView => _slotsViews;

        public event Action OnInitialized;

        private void Start()
        {
            _slotsViews = GetComponentsInChildren<InventorySlotView>().ToList();
            OnInitialized?.Invoke();
        }
    }
}