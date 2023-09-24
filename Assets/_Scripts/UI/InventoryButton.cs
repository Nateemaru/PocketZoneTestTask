using Zenject;

namespace _Scripts.UI
{
    public class InventoryButton : BaseButton
    {
        private InventoryView _inventoryView;
        private bool _isDisable;

        [Inject]
        private void Construct(InventoryView inventoryView)
        {
            _inventoryView = inventoryView;
        }

        protected override void Start()
        {
            base.Start();
            _inventoryView.Hide();
            _isDisable = true;
        }

        public override void OnClick()
        {
            if (_isDisable)
            {
                _inventoryView.Show();
                _isDisable = false;
            }
            else
            {
                _inventoryView.Hide();
                _isDisable = true;
            }
        }
    }
}