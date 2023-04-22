using System;
using System.Collections.Generic;
using Miniclip.Entities.Moles;

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
            {MoleType.Normal, typeof(NormalMole)},
            {MoleType.Fortified, typeof(FortifiedMole)},
            {MoleType.Bomb, typeof(BombMole)}
        };

        public Mole CreateMole(MoleType moleType)
        {
            if (!_moleTypes.ContainsKey(moleType))
            {
                throw new ArgumentException($"Invalid mole type: {moleType}");
            }

            Type type = _moleTypes[moleType];
            Mole mole = Activator.CreateInstance(type) as Mole;

            return mole;
        }
    }
}
