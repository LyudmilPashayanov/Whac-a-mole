using System;
using System.Collections.Generic;
using Miniclip.Entities.Moles;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Miniclip.Game
{
    public enum MoleType
    {
        Normal=0,
        Fortified=1,
        Bomb=2
    }

    public class MoleFactory
    {
        private readonly Dictionary<MoleType, Type> _moleTypes = new Dictionary<MoleType, Type>()
        {
            { MoleType.Normal, typeof(NormalMole) },
            { MoleType.Fortified, typeof(FortifiedMole) },
            { MoleType.Bomb, typeof(BombMole) }
        };

        private Dictionary<MoleType, Queue<GameObject>> _objectPool = new Dictionary<MoleType, Queue<GameObject>>();
        private readonly int _maxPoolSize = 8;

        public MoleFactory(GameObject molePrefab)
        {
            foreach (MoleType moleType in Enum.GetValues(typeof(MoleType)))
            {
                _objectPool[moleType] = new Queue<GameObject>();
                for (int i = 0; i < _maxPoolSize; i++)
                {
                    GameObject newMole = CreateMole(moleType, molePrefab);
                    newMole.SetActive(false);
                    _objectPool[moleType].Enqueue(newMole);
                }
            }
        }

        public GameObject GetMole(MoleType moleType)
        {
            if (!_moleTypes.ContainsKey(moleType))
            {
                throw new ArgumentException($"Invalid mole type: {moleType}");
            }

            if (_objectPool[moleType].Count == 0)
            {
                return null; // pool is empty
            }

            GameObject mole = _objectPool[moleType].Dequeue();
            mole.SetActive(true);
            return mole;
        }

        public void ReturnMole(GameObject mole)
        {
            mole.SetActive(false);
            MoleController moleController = mole.GetComponent<MoleController>();
            moleController.ResetMole();
            _objectPool[moleController.GetMoleType()].Enqueue(mole);
        }

        private GameObject CreateMole(MoleType moleType, GameObject molePrefab)
        {
            Type type = _moleTypes[moleType];
            Mole mole = Activator.CreateInstance(type) as Mole;
            GameObject newMole = Object.Instantiate(molePrefab);
            newMole.name = $"{moleType} Mole";
            MoleController moleController = newMole.GetComponent<MoleController>();
            moleController.SetupMole(mole, moleType);
            return newMole;
        }
    }
}
