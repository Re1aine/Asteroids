using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using TMPro;
using UnityEngine;

public class MenuWordView : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _letters;
    
    [Header("AnimationSettings")]
    [SerializeField] private float _jumpHeight = 20f;
    [SerializeField] private float _jumpTime = 0.3f;
    [SerializeField] private float _delayLetterJump = 0.1f;
    [SerializeField] private float _delayWordJump = 5;
    
    private CancellationTokenSource _cts;

    private void Awake() => 
        _cts = new CancellationTokenSource();

    private void Start() => 
        PlayWaveAnimation().Forget();

    private async UniTaskVoid PlayWaveAnimation()
    {
        while (!_cts.IsCancellationRequested)
        {
            await UniTask.Delay((int)(1000 * _delayWordJump), false, PlayerLoopTiming.Update, _cts.Token);
            await JumpWave();   
        }
    }

    private async UniTask JumpWave()
    {
        for (int i = 0; i < _letters.Count; i++)
        {
            JumpLetter(i).Forget();
            await UniTask.Delay((int)(1000 * _delayLetterJump), false, PlayerLoopTiming.Update, _cts.Token);
        }
    }

    private async UniTaskVoid JumpLetter(int index)
    {
        var letter = _letters[index];
        var startPos = letter.rectTransform.anchoredPosition;
        var upPos = startPos + new Vector2(0, _jumpHeight);

        await LMotion.Create(startPos, upPos, _jumpTime)
            .WithEase(Ease.OutBack)
            .Bind(pos => letter.rectTransform.anchoredPosition = pos)
            .AddTo(this)
            .ToUniTask();

        await LMotion.Create(upPos, startPos, _jumpTime)
            .WithEase(Ease.OutSine)
            .Bind(pos => letter.rectTransform.anchoredPosition = pos)
            .AddTo(this)
            .ToUniTask();
    }

    private void OnDestroy() => 
        _cts.Cancel();
}