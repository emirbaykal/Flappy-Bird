using System;
using MainFolder.Scripts.Zenject.Signals;
using UnityEngine;
using Zenject;

namespace MainFolder.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector] public int score;

        //Zenject variable
        private BirdInputs _birdInputs;
        private SignalBus _bus;
    
        [Inject]
        public void Construct(SignalBus bus ,BirdInputs birdInputs)
        {
            //Zenject Binds
            _bus = bus;
            _birdInputs = birdInputs;
        }

        private void OnEnable()
        {
            _bus.Subscribe<BirdDestroy>(GameOver);
        }

        private void OnDisable()
        {
            _bus.Unsubscribe<BirdDestroy>(GameOver);
        }

        public void GameOver(){
            //After it happens, we close the inputs during the fall.
            _birdInputs.Disable();
        
            _bus.Fire(new GameOver());
        }

    
        //Restart Game Button Function
        public void RestartGame()
        {
            _bus.Fire(new BirdReset());
        
            _bus.Fire(new RestartGame());

            score = 0;
            
            //After it happens we reopen the inputs we closed during the fall.
            _birdInputs.Enable();
        }
    
        //Score

        public int AddScore()
        {
            score++;
            return score;
        }
    }
}
