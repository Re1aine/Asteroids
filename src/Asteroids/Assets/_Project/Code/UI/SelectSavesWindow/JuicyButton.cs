using LitMotion;
using UnityEngine;
using UnityEngine.EventSystems;

public class JuicyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("AnimationSettings")]
    [SerializeField] private float _bounceScale;
    [SerializeField] private float _durationBounce;

    private MotionHandle _handle;
    
    private RectTransform _rect;

    private void Awake() => 
        _rect = GetComponent<RectTransform>();

    public void OnPointerEnter(PointerEventData eventData) => 
        _handle = AnimatePulse();

    public void OnPointerExit(PointerEventData eventData)
    {
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