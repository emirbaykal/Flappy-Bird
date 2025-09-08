using DG.Tweening;
using MainFolder.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace MainFolder.Scripts.Environment
{
    public class ColumnsGroup : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Tween _moveTween;
        public void ColumnMovement()
        {
            _moveTween?.Kill();
            Debug.Log("MOVE");
            float distance = 100f;
            float duration = distance / speed;

            _moveTween = transform.DOMoveX(-distance, duration).
                SetRelative(true).
                SetEase(Ease.Linear).
                SetLoops(-1, LoopType.Incremental);
        }
        public class Pool : MonoMemoryPool<ColumnsGroup>
        {
            [Inject] private MapManager _mapManager;
            protected override void Reinitialize(ColumnsGroup item)
            {
                item.gameObject.SetActive(true);
            
                item.transform.position = _mapManager.startingLocation;
            }
        
            protected override void OnDespawned(ColumnsGroup item)
            {
                item._moveTween?.Kill();
                item.gameObject.SetActive(false);
                item.transform.position = Vector3.zero;
            }
        
        
        }
    }
}
