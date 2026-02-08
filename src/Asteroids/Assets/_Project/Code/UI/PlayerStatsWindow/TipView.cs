using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine;

public class TipView : MonoBehaviour
{
    [Header("AnimationSettings")]
    [SerializeField] private float _durationMoveAnim;
    
    private RectTransform _rect;
    
    private Vector2 _originPosition;
    private Vector2 _targetPosition;
    
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _originPosition = _rect.anchoredPosition;
        _targetPosition = new Vector2(_rect.anchoredPosition.x, -_rect.anchoredPosition.y / 10 );
    }

    private void Start() => 
        PlayEntranceAnimation();

    public async UniTaskVoid Destroy()
    {
        await PlayHideAnimation();
        Destroy(gameObject);
    }

    private void PlayEntranceAnimation()
    {
        LMotion.Create(_originPosition, _targetPosition, _durationMoveAnim)
            .WithDelay(0.2f)
            .WithEase(Ease.OutBack)
            .Bind(pos => _rect.anchoredPosition = pos)
            .AddTo(this);
    }
    
    private UniTask PlayHideAnimation()
    {
        return LMotion.Create(_targetPosition, _originPosition, _durationMoveAnim)
            .WithDelay(0.2f)
            .WithEase(Ease.OutBack)
            .Bind(pos => _rect.anchoredPosition = pos)
            .AddTo(this)
            .ToUniTask();
    }
}