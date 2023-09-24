using UnityEngine;

namespace _Scripts.Configs
{
    [CreateAssetMenu(menuName = "SO/DefaultPlayerConfig", fileName = "DefaultPlayerConfig")]
    public class DefaultPlayerConfig : ScriptableObject
    {
        [SerializeField] private float _hp;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _movementSpeed;
    }
}