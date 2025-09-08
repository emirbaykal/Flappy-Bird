using MainFolder.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace MainFolder.Scripts.Environment
{
    public class Column : MonoBehaviour
    {
        [SerializeField] private string birdTag;
    
        private SignalBus _bus;
        //private SoundManager _soundManager;
        
        [Inject] 
        public void Construct(SignalBus bus)
        {
            //_soundManager = soundManager;
            _bus = bus;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.CompareTag(birdTag)) return;
            //Declare signal
            _bus.Fire(new BirdDestroy
            {
                DestroyObject = other.gameObject
            });
            //If you bind using container.BindSignal, use this.
            //_bus.Fire<BirdDestroySignal>();
        }
    }
}
