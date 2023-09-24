using System;
using _Scripts.Game.InventorySystem;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.PlayerCore
{
    public class PlayerInventoryProvider : MonoBehaviour
    {
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
        }
    }
}