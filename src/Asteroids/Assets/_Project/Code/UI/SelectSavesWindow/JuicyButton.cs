using LitMotion;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JuicyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite _onMouseEnterSprite;
    [SerializeField] private Sprite _onMouseExitSprite;

    [Header("AnimationSettings")]
    [SerializeField] private float _bounceScale;
    [SerializeField] private float _durationBounce;

    private MotionHandle _handle;
    
    private RectTransform _rect;
    
    private Image _image;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = _onMouseEnterSprite;
        
        _handle = AnimatePulse();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = _onMouseExitSprite;
        
        _handle.Cancel();
        
        LMotion.Create(_rect.localScale, Vector3.one, _durationBounce)
            .WithEase(Ease.OutSine)
            .Bind(scale => _rect.localScale = scale)
            .AddTo(this);
    }

    private MotionHandle AnimatePulse()
    {
        return LMotion.Create(_rect.localScale, Vector3.one * _bounceScale, _durationBounce)
            .WithEase(Ease.OutSine)
            .WithLoops(-1, LoopType.Yoyo)
            .Bind(scale => _rect.localScale = scale)
            .AddTo(this);
    }
}