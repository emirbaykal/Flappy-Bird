using MainFolder.Scripts.Bird;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace MainFolder.Input
{
    public class InputHandler : MonoBehaviour
    {
        private BirdInputs _birdInputs;
        private BirdController _birdController;

        [Inject]
        public void Construct(BirdInputs birdInputs, BirdController birdController)
        {
            _birdInputs = birdInputs;
            _birdController = birdController;
        }
    
        private void Awake()
        {
            _birdInputs.Enable();

            _birdInputs.Bird.Jump.performed += PlayerJump;
        }

        public void PlayerJump(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                _birdController.OnJump();
            }
        }
    }
}
