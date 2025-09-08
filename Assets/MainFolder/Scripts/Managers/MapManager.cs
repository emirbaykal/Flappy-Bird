using System;
using System.Collections.Generic;
using MainFolder.Scripts.Environment;
using MainFolder.Scripts.Zenject.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace MainFolder.Scripts.Managers
{
   public class MapManager : MonoBehaviour
   {
      //Zenject variable
      private ColumnsGroup.Pool _pool;
      private SignalBus _bus;

      //Column paralax
      private int _currentIndex;
      private const int MaxColumns = 3;
      private readonly List<ColumnsGroup> _allColumns = new();

      [Header("Column Info")] 
      public Vector3 startingLocation;
      private const float ColumnMaxHeight = 2.5f;

      [Inject]
      private void Construct(ColumnsGroup.Pool pool, SignalBus bus)
      {
         _pool = pool;
         _bus = bus;
      
         CreateNewColumn();
      }

      private void OnEnable()
      {
         _bus.Subscribe<GameOver>(DespawnAllColumns);
         _bus.Subscribe<RestartGame>(CreateNewColumn);
         _bus.Subscribe<CreateNewColumn>(CreateNewColumn);

      }

      private void OnDisable()
      {
         _bus.Unsubscribe<CreateNewColumn>(CreateNewColumn);
         _bus.Unsubscribe<GameOver>(DespawnAllColumns);
         _bus.Unsubscribe<RestartGame>(CreateNewColumn);
      }

      
      
      public void CreateNewColumn()
      {
         ColumnsGroup col;
         if (_allColumns.Count < MaxColumns)
         {
            col = _pool.Spawn();
            _allColumns.Add(col);
            col.ColumnMovement();
         }
         else
         {
            col = _allColumns[_currentIndex];
            _pool.Despawn(col);
            col = _pool.Spawn();
            _allColumns[_currentIndex] = col;
            col.ColumnMovement();

            float y = Random.Range(-ColumnMaxHeight, ColumnMaxHeight);
            col.transform.position = new Vector3(startingLocation.x, startingLocation.y + y, 0);

            _currentIndex = (_currentIndex + 1) % MaxColumns;
         }
      }

      public void DespawnAllColumns()
      {
         foreach (var item in _allColumns)
         {
            _pool.Despawn(item);
         }
         _allColumns.Clear();
      } 
   
   }
}
