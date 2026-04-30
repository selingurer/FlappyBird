using System.Collections.Generic;
using System.Linq;
using Booster;
using ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Service
{
    public class BoosterService : IBoosterService
    {
        private BoosterDatabase _boosterDatabase;

        private IEnumerable<IBooster> _boosters;

        [Inject]
        public BoosterService(BoosterDatabase boosterDatabase, IEnumerable<IBooster> boosters)
        {
            _boosterDatabase = boosterDatabase;
            _boosters = boosters;

            BoosterSetData();
        }

        public void BoosterSetData()
        {
            foreach (var booster in _boosters)
            {
                var boosterData = _boosterDatabase.GetBoosterData(booster.BoosterType);
                booster.SetData(boosterData);
            }
        }

        public void Boost(BoosterType type, Bird bird)
        {
            _boosterDatabase.GetBoosterData(type);
            var booster = _boosters.FirstOrDefault(x => x.BoosterType == type);

            if (booster == null)
            {
                Debug.LogError($"Booster bulunamadı: {type}");
                return;
            }

            booster.Activate(bird);
        }
    }

    public interface IBoosterService
    {
        public void Boost(BoosterType type, Bird bird);
    }
}