using MainFolder.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace MainFolder.Scripts.Environment
{
    public class SuccessBox : MonoBehaviour
    {
        [Inject] private SignalBus _bus;
        private void OnTriggerExit2D(Collider2D other)
        {
            _bus.Fire(new ColumnSuccess());
        
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _bus.Fire(new CreateNewColumn());
        }
    }
}
