using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Gameplay.Services.Repository.Player;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class MenuHUDView : AHUDView
{
    [SerializeField] private Button _purchaseNoAdsButton;
    
    private IPurchaseService _purchaseService;
    private IRepositoriesHolder _repositoriesHolder;

    [Inject]
    private void Construct(IPurchaseService purchaseService, IRepositoriesHolder repositoriesHolder)
    {
        _purchaseService = purchaseService;
        _repositoriesHolder =  repositoriesHolder;
    }

    private void Start()
    {
        _purchaseNoAdsButton.onClick.AddListener(() => _purchaseService.Purchase(ProductId.AdsRemoval));

        _repositoriesHolder
            .GetRepository<PlayerRepository>().IsAdsRemoved
            .Subscribe(SetActiveRemoveAdsButton)
            .AddTo(this);
    }

    private void SetActiveRemoveAdsButton(bool isActive) => 
        _purchaseNoAdsButton.gameObject.SetActive(!isActive);
}