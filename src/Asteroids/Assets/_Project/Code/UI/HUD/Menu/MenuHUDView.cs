using UnityEngine;

namespace _Project.Code.UI.HUD.Menu
{
    public class MenuHUDView : AHUDView
    {
        [SerializeField] private NoAdsButtonView _noAdsButtonView;
        [SerializeField] private DevWindowView _devWindowView;
        
        public override void Build()
        {
            _noAdsButtonView.gameObject.SetActive(true);
            _devWindowView.gameObject.SetActive(true);
        }
    }
}