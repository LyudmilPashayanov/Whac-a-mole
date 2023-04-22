using System;
using System.Collections.Generic;
using Miniclip.Entities.Moles;
using UnityEngine;
using UnityEngine.U2D;
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
        private Queue<GameObject> _objectPool = new Queue<GameObject>();
        private GameObject _molePrefab;
        private SpriteAtlas _molesAtlas;
        
        public MoleFactory(GameObject molePrefab,SpriteAtlas molesAtlas)
        {
            _molePrefab = molePrefab;
            _molesAtlas = molesAtlas;
        }

        public GameObject GetMole(MoleType moleType)
        {
            Mole mole = GetMoleData(moleType);
            
            if (_objectPool.Count == 0)
            {
                CreateMole(mole);
            }

            GameObject moleGameObject = _objectPool.Dequeue();
            MoleController moleController = moleGameObject.GetComponent<MoleController>();
            Sprite moleSprite = _molesAtlas.GetSprite(mole.GetSpriteName());
            moleController.SetupMole(mole, moleSprite);
            
            return moleGameObject;
        }
        
        private Mole GetMoleData(MoleType moleType)
        {
            switch (moleType)
            {
                case MoleType.Normal:
                    return new NormalMole();
                case MoleType.Fortified:
                    return new FortifiedMole();
                case MoleType.Bomb:
                    return new BombMole();
                default:
                    throw new ArgumentException($"Invalid mole type: {moleType}");
            }
        }
        
        public void ReturnMole(GameObject mole)
        {
            mole.SetActive(false);
            mole.transform.parent = null;
            _objectPool.Enqueue(mole);
        }

        private void CreateMole(Mole mole)
        {
            GameObject newMole = Object.Instantiate(_molePrefab);
            newMole.name = $"{mole.GetType().DeclaringType} Mole";
            _objectPool.Enqueue(newMole);
        }
    }
}
