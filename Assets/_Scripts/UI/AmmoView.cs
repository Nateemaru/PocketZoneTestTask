using System;
using _Scripts.Game.PlayerCore;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Scripts.UI
{
    public class AmmoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textView;
        
        private IAmmoHandler _ammoHandler;

        [Inject]
        private void Construct(IAmmoHandler ammoHandler)
        {
            _ammoHandler = ammoHandler;
        }

        private void Update()
        {
            if (_ammoHandler != null)
            {
                _textView.text = $"{_ammoHandler.BulletsInHolder.ToString()}/{_ammoHandler.MaxBulletsInHolder.ToString()}";
            }
        }
    }
}