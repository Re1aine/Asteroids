using UnityEngine;
using VContainer;

public sealed class TransformWrapper : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private IPointWrapService _coordinateWrapper;
    private IBoundaries _boundaries;

    private Vector2 _correction;

    private bool _isInsideBounds;
    private bool _hasEnteredBounds;
    
    [Inject]
    public void Construct(IPointWrapService coordinateWrapper, IBoundaries boundaries)
    {
        _coordinateWrapper = coordinateWrapper;
        _boundaries =  boundaries;
    }

    private void Awake() => 
        _correction = _spriteRenderer ? _spriteRenderer.bounds.size / 2 : Vector2.zero;

    private void Update()
    {
        _isInsideBounds = transform.position.x >= _boundaries.Min.x &&
                              transform.position.x <= _boundaries.Max.x &&
                              transform.position.y >= _boundaries.Min.y &&
                              transform.position.y <= _boundaries.Max.y;

        if (_isInsideBounds) 
            _hasEnteredBounds  = true;
        
        if (_hasEnteredBounds)
            transform.position = _coordinateWrapper.WrapPoint(transform.position, _correction);
    }
}