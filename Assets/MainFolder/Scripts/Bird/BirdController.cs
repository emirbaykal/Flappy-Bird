using MainFolder.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace MainFolder.Scripts.Bird
{
    public class BirdController : MonoBehaviour
    {
        //Zenject
        private SoundManager _soundManager;
        private SignalBus _bus;
        
        private Rigidbody2D _rb;
    
        private static readonly float jumpForce = 5f;

        [Inject]
        public void Construct(SignalBus bus, SoundManager soundManager)
        {
            _bus = bus;
            _soundManager = soundManager;
        }

        private void OnEnable()
        {
            _bus.Subscribe<BirdReset>(ResetBirdPosition);
        }

        private void OnDisable()
        {
            _bus.Unsubscribe<BirdReset>(ResetBirdPosition);
        }

        private void Awake()
        {
            _rb ??= GetComponent<Rigidbody2D>();
        }
    
        //Input Jump
        public void OnJump()
        {
            _rb.linearVelocity = Vector2.up * jumpForce;
            _soundManager.PlayJump();
        }

        public void ResetBirdPosition()
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.simulated = false;
            gameObject.transform.position = Vector3.zero;
            _rb.simulated = true;
        }
    }
}
