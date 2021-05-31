﻿using ArkSavegameToolkitNet.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkSavegameToolkitNet.Domain
{
    public enum ArkCreatureGender { Male, Female }

    public class ArkCreature : ArkGameDataContainerBase
    {
        private static readonly ArkName _dinoId1 = ArkName.Create("DinoID1");
        private static readonly ArkName _dinoId2 = ArkName.Create("DinoID2");
        private static readonly ArkName _baseCharacterLevel = ArkName.Create("BaseCharacterLevel");
        private static readonly ArkName _bIsBaby = ArkName.Create("bIsBaby");
        private static readonly ArkName _babyAge = ArkName.Create("BabyAge");
        private static readonly ArkName _bIsFemale = ArkName.Create("bIsFemale");
        private static readonly ArkName[] _colorSetIndices = new[]
        {
            ArkName.Create("ColorSetIndices", 0),
            ArkName.Create("ColorSetIndices", 1),
            ArkName.Create("ColorSetIndices", 2),
            ArkName.Create("ColorSetIndices", 3),
            ArkName.Create("ColorSetIndices", 4),
            ArkName.Create("ColorSetIndices", 5)
        };
        private static readonly ArkName[] _numberOfLevelUpPointsApplied = new[]
        {
            ArkName.Create("NumberOfLevelUpPointsApplied", 0), //health
            ArkName.Create("NumberOfLevelUpPointsApplied", 1), //stamina
            ArkName.Create("NumberOfLevelUpPointsApplied", 2), //torpor
            ArkName.Create("NumberOfLevelUpPointsApplied", 3), //oxygen
            ArkName.Create("NumberOfLevelUpPointsApplied", 4), //food
            ArkName.Create("NumberOfLevelUpPointsApplied", 5), //water
            ArkName.Create("NumberOfLevelUpPointsApplied", 6), //temperature
            ArkName.Create("NumberOfLevelUpPointsApplied", 7), //weight
            ArkName.Create("NumberOfLevelUpPointsApplied", 8), //melee damage
            ArkName.Create("NumberOfLevelUpPointsApplied", 9), //movement speed
            ArkName.Create("NumberOfLevelUpPointsApplied", 10), //fortitude
            ArkName.Create("NumberOfLevelUpPointsApplied", 11) //crafting speed
        };

        internal static readonly ArkNameTree _dependencies = new ArkNameTree
        {
            { _dinoId1, null },
            { _dinoId2, null },
            { _baseCharacterLevel, null },
            { _bIsBaby, null },
            { _babyAge, null },
            { _bIsFemale, null },
            { _colorSetIndices[0], null },
            { _colorSetIndices[1], null },
            { _colorSetIndices[2], null },
            { _colorSetIndices[3], null },
            { _colorSetIndices[4], null },
            { _colorSetIndices[5], null },
            { _numberOfLevelUpPointsApplied[0], null },
            { _numberOfLevelUpPointsApplied[1], null },
            { _numberOfLevelUpPointsApplied[2], null },
            { _numberOfLevelUpPointsApplied[3], null },
            { _numberOfLevelUpPointsApplied[4], null },
            { _numberOfLevelUpPointsApplied[5], null },
            { _numberOfLevelUpPointsApplied[6], null },
            { _numberOfLevelUpPointsApplied[7], null },
            { _numberOfLevelUpPointsApplied[8], null },
            { _numberOfLevelUpPointsApplied[9], null },
            { _numberOfLevelUpPointsApplied[10], null },
            { _numberOfLevelUpPointsApplied[11], null }
        };

        internal IGameObject _creature;
        internal IGameObject _status;

        internal void Decouple()
        {
            _creature = null;
            _status = null;
        }

        public ArkCreature()
        {
            Colors = new byte[_colorSetIndices.Length];
            BaseStats = new byte[_numberOfLevelUpPointsApplied.Length];
        }

        public ArkCreature(IGameObject creature, IGameObject status, ISaveState saveState) : this()
        {
            _creature = creature;
            _status = status;

            //Id = creature.Index;
            //Uuid = _creature.Uuid;
            Id1 = creature.GetPropertyValue<uint>(_dinoId1);
            Id2 = creature.GetPropertyValue<uint>(_dinoId2);
            ClassName = creature.ClassName.Name;
            IsBaby = creature.GetPropertyValue<bool?>(_bIsBaby) ?? false;
            BabyAge = creature.GetPropertyValue<float?>(_babyAge);
            Gender = creature.GetPropertyValue<bool?>(_bIsFemale) == true ? ArkCreatureGender.Female : ArkCreatureGender.Male;
            for (var i = 0; i < Colors.Length; i++) Colors[i] = creature.GetPropertyValue<byte?>(_colorSetIndices[i]) ?? 0;

            if (status != null)
            {
                BaseLevel = status.GetPropertyValue<int?>(_baseCharacterLevel) ?? 1;
                for (var i = 0; i < BaseStats.Length; i++) BaseStats[i] = status.GetPropertyValue<byte?>(_numberOfLevelUpPointsApplied[i]) ?? 0;
            }

            if (creature.Location != null) Location = new ArkLocation(creature.Location, saveState);
        }

        //public int Id { get; set; }
        //public Guid Uuid { get; set; } //not unique?
        public uint Id1 { get; set; }
        public uint Id2 { get; set; }
        public ulong Id => ((ulong)Id1 << 32) | Id2;
        public string ClassName { get; set; }
        public int BaseLevel { get; set; }
        public bool IsBaby { get; set; }
        public float? BabyAge { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ArkCreatureGender Gender { get; set; }
        public byte[] Colors { get; set; }
        public byte[] BaseStats { get; set; }
        public ArkLocation Location { get; set; }
    }
}