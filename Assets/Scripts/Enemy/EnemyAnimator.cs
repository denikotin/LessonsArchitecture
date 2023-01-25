using Assets.Scripts.Logic;
using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
{
    private static readonly int Attack = Animator.StringToHash("Attack_1");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Die = Animator.StringToHash("Die");

    private static readonly int _idleStateHash = Animator.StringToHash("idle");
    private static readonly int _attackStateHash = Animator.StringToHash("attack01");
    private static readonly int _walkingStateHash = Animator.StringToHash("Move");
    private static readonly int _deathStateHash = Animator.StringToHash("Die");

    private Animator _animator;

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }

    private void Awake() => _animator = GetComponent<Animator>();

    public void PlayerHit() => _animator.SetTrigger(Hit);
    public void PlayDeath() => _animator.SetTrigger(Die);

    public void Move(float speed)
    {
        _animator.SetBool(IsMoving, true);
        _animator.SetFloat(Speed, speed);
    }

    public void StopMove() => _animator.SetBool(IsMoving, false);
    public void PlayAttack() => _animator.SetTrigger(Attack);

    public void EnteredState(int stateHash)
    {
        State = StateFor(stateHash);
        StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash)
    {
        StateExited?.Invoke(State); 
    }

    private AnimatorState StateFor(int stateHash)
    {
        AnimatorState state;
        if (stateHash == _idleStateHash)
            state = AnimatorState.Idle;
        else if (stateHash == _attackStateHash)
            state = AnimatorState.Attack;
        else if (stateHash == _deathStateHash)
            state = AnimatorState.Died;
        else if (_attackStateHash == _walkingStateHash)
            state = AnimatorState.Walking;
        else
            state = AnimatorState.Unknown;
        return state;
    }
}
