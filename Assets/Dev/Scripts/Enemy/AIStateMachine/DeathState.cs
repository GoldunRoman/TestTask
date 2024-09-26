using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class DeathState : EnemyState
{
    private Tween _jumpTween;

    private readonly float _jumpHeight = 2.0f;
    private readonly float _jumpDuration = 0.5f;
    private readonly float _scaleDuration = 0.5f;
    private readonly Vector3 _targetScale = Vector3.zero;

    public DeathState(Enemy enemy, NavMeshAgent agent) : base(enemy, agent) { }

    public override void EnterState()
    {
        Agent.isStopped = true;
        JumpTowardsPlayer();
    }

    private void JumpTowardsPlayer()
    {
        Vector3 startPosition = Enemy.transform.position;
        Vector3 targetPosition = Enemy.PlayerPosition;

        Vector3 jumpPeak = (startPosition + targetPosition) / 2;
        jumpPeak.y += _jumpHeight;

        Enemy.transform.DOScale(_targetScale, _scaleDuration).SetEase(Ease.InOutQuad);

        _jumpTween = Enemy.transform
            .DOPath(new[] { jumpPeak, targetPosition }, _jumpDuration, PathType.CatmullRom)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                Enemy.Despawn();
            });
    }

    public override void ExitState()
    {
        _jumpTween?.Kill();
    }
}
