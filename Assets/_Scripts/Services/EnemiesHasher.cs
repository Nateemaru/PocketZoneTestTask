using System;
using System.Collections.Generic;
using _Scripts.Game.AI;
using Unity.Collections;

namespace _Scripts.Services
{
    public class EnemiesHasher
    {
        [ReadOnly] public List<BaseEnemy> Enemies = new List<BaseEnemy>();

        public delegate void OnAmountChanged(int amount);
        
        public OnAmountChanged OnEnemiesAmountChanged;
        public Action OnEnemiesEmpty;

        public void Add(BaseEnemy enemy)
        {
            if(!Enemies.Contains(enemy))
            {
                Enemies.Add(enemy);
                OnEnemiesAmountChanged?.Invoke(Enemies.Count);
            }
        }

        public void Remove(BaseEnemy enemy)
        {
            if(Enemies.Contains(enemy))
            {
                Enemies.Remove(enemy);
                OnEnemiesAmountChanged?.Invoke(Enemies.Count);
            }
            
            if(Enemies == null || Enemies.Count <= 0)
            {
                OnEnemiesEmpty?.Invoke();
            }
        }
    }
}
