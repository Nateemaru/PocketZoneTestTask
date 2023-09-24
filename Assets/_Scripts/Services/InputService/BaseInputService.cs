using System;
using UnityEngine;

namespace _Scripts.Services.InputService
{
    public abstract class BaseInputService : MonoBehaviour
    {
        public abstract Vector3 GetDirection();
        public abstract bool IsFireButton();
        public abstract bool IsInventoryButton();
    }
}