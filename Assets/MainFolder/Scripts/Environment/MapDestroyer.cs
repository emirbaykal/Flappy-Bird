using MainFolder.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace MainFolder.Scripts.Environment
{
    public class MapDestroyer : MonoBehaviour
    {
        private SignalBus _bus;

        [Inject]
        public void Construct(SignalBus bus)
        {
            _bus = bus;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            _bus.Fire(new BirdDestroy
            {
                DestroyObject = other.gameObject
            });
        }
    }
}
