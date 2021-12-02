using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInput : MonoBehaviour
    {
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            float horizontalDirection = Input.GetAxis(GlobalInputStrings.HORIZONTAL_AXIS);
            bool isJumpButtonPressed = Input.GetButtonDown(GlobalInputStrings.JUMP_BUTTON);
                
            _playerMovement.Move(horizontalDirection, isJumpButtonPressed);
        }
    }
}