using System;
using MainFolder.Scripts.Zenject.Signals;
using UnityEngine;
using Zenject;

namespace MainFolder.Scripts.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [Inject] private SignalBus _bus;
        
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip jumpClip;
        [SerializeField] private AudioClip dieClip;
        [SerializeField] private AudioClip scoreClip;

        private void OnEnable()
        {
            _bus.Subscribe<GameOver>(PlayDie);
            _bus.Subscribe<ColumnSuccess>(PlayScore);
        }

        private void OnDisable()
        {
            _bus.Unsubscribe<GameOver>(PlayDie);
            _bus.Unsubscribe<ColumnSuccess>(PlayScore);
        }

        public void PlayJump() => PlayCustomSound(jumpClip);
        public void PlayDie() => PlayCustomSound(dieClip);
        public void PlayScore() => PlayCustomSound(scoreClip);
        
        public void PlayCustomSound(AudioClip clip)
        {
            if (clip is not null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}

