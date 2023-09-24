using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Game
{
    public class BlinkTween : MonoBehaviour
    {
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private float _duration = 0.2f;
        [SerializeField] private List<SpriteRenderer> _renderers = new List<SpriteRenderer>();

        private List<Color> _defaultColors = new List<Color>();

        private void Start()
        {
            _renderers = GetComponentsInChildren<SpriteRenderer>().ToList();
            
            foreach (SpriteRenderer renderer in _renderers)
            {
                _defaultColors.Add(renderer.color);
            }
        }

        public void Play()
        {
            foreach (SpriteRenderer renderer in _renderers)
            {
                renderer.DOColor(_color, _duration).SetLoops(2, LoopType.Yoyo);
            }
        }

        public void Kill()
        {
            for (int i = 0; i < _renderers.Count; i++)
            {
                _renderers[i].color = _defaultColors[i];
            }

            this.DOKill();
        }
    }
}
