using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterMotor _motor;
    [SerializeField] private InputsManager _inputs;

    private void Awake()
    {
        _inputs.OnMovePerformed.AddListener(_motor.MovePerformed);
        _inputs.OnJumpPerformed.AddListener(_motor.JumpPerformed);
    }
}
