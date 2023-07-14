using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterMotor _characterMotor;

    private void Start()
    {
        _characterMotor.OnJump.AddListener(OnJump);
    }

    private void Update()
    {
        _animator.SetBool(Keywords.GROUNDED_KEY, _characterMotor.Grounded);
        _animator.SetFloat(Keywords.SPEED_PERCENT_KEY, _characterMotor.SpeedPercent);
    }

    private void OnJump()
    {
        _animator.SetTrigger(Keywords.JUMP_KEY);
    }
}
