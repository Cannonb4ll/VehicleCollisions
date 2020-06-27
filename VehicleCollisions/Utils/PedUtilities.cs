using System;
using CitizenFX.Core;

namespace VehicleCollisions.Utils
{
    public static class PedUtilities
    {
        private static readonly PedHash[] CopHashes =
        {
            PedHash.Cop01SFY,
            PedHash.Cop01SMY,
            PedHash.CopCutscene,
            PedHash.Ranger01SFY,
            PedHash.Ranger01SMY,
            PedHash.PrologueSec01Cutscene,
            PedHash.PrologueSec02Cutscene
        };

        private static readonly PedHash[] FirefighterHashes =
        {
            PedHash.Fireman01SMY,
        };

        private static readonly PedHash[] MilitaryHashes =
        {
            PedHash.Armymech01SMY,
            PedHash.Marine01SMM,
            PedHash.Marine01SMY,
            PedHash.Marine02SMM,
            PedHash.Marine02SMY
        };

        private static readonly PedHash[] SafePeds = new PedHash[54]
        {
            0,
            PedHash.Humpback,
            PedHash.Dolphin,
            PedHash.KillerWhale,
            PedHash.Fish,
            PedHash.HammerShark,
            PedHash.TigerShark,
            PedHash.Boar,
            PedHash.Cat,
            PedHash.ChickenHawk,
            PedHash.Chimp,
            PedHash.Coyote,
            PedHash.Cow,
            PedHash.Deer,
            PedHash.Pig,
            PedHash.Rabbit,
            PedHash.Crow,
            PedHash.Cormorant,
            PedHash.Husky,
            PedHash.Rottweiler,
            PedHash.Pug,
            PedHash.Poodle,
            PedHash.Retriever,
            PedHash.Seagull,
            PedHash.Pigeon,
            PedHash.MountainLion,
            PedHash.BradCadaverCutscene,
            PedHash.Chop,
            PedHash.Hen,
            PedHash.JohnnyKlebitz,
            PedHash.LamarDavisCutscene,
            PedHash.MagentaCutscene,
            PedHash.Marston01,
            PedHash.Misty01,
            PedHash.MovAlien01,
            PedHash.MoviePremFemaleCutscene,
            PedHash.MoviePremMaleCutscene,
            PedHash.MrsPhillipsCutscene,
            PedHash.MrKCutscene,
            PedHash.NataliaCutscene,
            PedHash.NigelCutscene,
            PedHash.NervousRonCutscene,
            PedHash.Niko01,
            PedHash.PaigeCutscene,
            PedHash.OscarCutscene,
            PedHash.OrtegaCutscene,
            PedHash.OrleansCutscene,
            PedHash.Orleans,
            PedHash.Pogo01,
            PedHash.Rat,
            PedHash.Rhesus,
            PedHash.Stingray,
            PedHash.SteveHainsCutscene,
            PedHash.Westy
        };

        public static PedHash GetRandomCop()
        {
            return CopHashes[Utilities.Between(0, CopHashes.Length)];
        }

        public static PedHash GetRandomMilitary()
        {
            return MilitaryHashes[Utilities.Between(0, MilitaryHashes.Length)];
        }

        public static PedHash GetRandomFirefighter()
        {
            return FirefighterHashes[Utilities.Between(0, FirefighterHashes.Length)];
        }

        public static PedHash GetRandomPed()
        {
            var values = Enum.GetValues(typeof(PedHash));
            
            PedHash pedHash;
            
            do
            {
                pedHash = (PedHash) values.GetValue(Utilities.Between(0, values.Length));
            } while (Array.IndexOf(SafePeds, pedHash) != -1);

            return pedHash;
        }
    }
}