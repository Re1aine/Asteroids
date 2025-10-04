using UnityEngine;
using VContainer;

[RequireComponent(typeof(SpriteRenderer))]
public sealed class BackgroundResizer : MonoBehaviour
{
    private IBoundaries _boundaries;
    private SpriteRenderer _background;

    [Inject]
    public void Construct(IBoundaries boundaries) => 
        _boundaries = boundaries;
        
    private void Awake() => 
        _background = GetComponent<SpriteRenderer>();
        
    private void Start() =>
        ResizeBackground();

    private void ResizeBackground()
    {
        var worldWidth = _boundaries.Max.x - _boundaries.Min.x;
        var worldHeight = _boundaries.Max.y - _boundaries.Min.y;
        
        var sprite = _background.sprite;
        var backgroundWorldWidth = sprite.bounds.size.x;
        var backgroundWorldHeight = sprite.bounds.size.y;
        
        var scaleX = worldWidth / backgroundWorldWidth;
        var scaleY = worldHeight / backgroundWorldHeight;
        
        transform.localScale = new Vector3(scaleX, scaleY, 1);
    }
}
