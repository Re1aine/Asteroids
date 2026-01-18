using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Menu.Services.Purchase;
using Code.Logic.Menu.Services.Purchase.Product;
using Code.Logic.Services.Repository.Player;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Code.UI.HUD.Menu
{
    public class NoAdsButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
    
        private IPurchaseService _purchaseService;
        private IRepositoriesHolder _repositoriesHolder;

        [Inject]
        public void Construct(IPurchaseService purchaseService, IRepositoriesHolder repositoriesHolder)
        {
            _purchaseService = purchaseService;
            _repositoriesHolder = repositoriesHolder;
        }
    
        private void OnEnable()
        {
            _button.onClick.AddListener(() => _purchaseService.Purchase(ProductId.AdsRemoval));
            
            _repositoriesHolder
                .GetRepository<PlayerRepository>().IsAdsRemoved
                .Subscribe(SetActiveRemoveAdsButton)
                .AddTo(this);   
        }
    
        private void SetActiveRemoveAdsButton(bool isActive) => 
            _button.gameObject.SetActive(!isActive);
    }
}