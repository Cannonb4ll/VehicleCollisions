using System;
using CitizenFX.Core;

namespace VehicleCollisions.Utils
{
    public class VehicleUtilities
    {
        private static readonly VehicleHash[] SafeVehicleHashes =
        {
            VehicleHash.Baller,
            VehicleHash.Baller2,
            VehicleHash.Baller3,
            VehicleHash.Baller4,
            VehicleHash.Baller5,
            VehicleHash.Baller6,
            VehicleHash.Bison,
            VehicleHash.Speedo,
            VehicleHash.BType3,
            VehicleHash.Emperor,
            VehicleHash.Comet2,
            VehicleHash.Exemplar,
            VehicleHash.F620,
            VehicleHash.Sentinel,
            VehicleHash.Zion,
            VehicleHash.Voodoo,
            VehicleHash.Voodoo2,
            VehicleHash.Stalion,
            VehicleHash.Sultan,
            VehicleHash.SultanRS,
            VehicleHash.Stalion2,
            VehicleHash.Blista,
            VehicleHash.Issi2,
            VehicleHash.Prairie,
            VehicleHash.Rhapsody,
            VehicleHash.Oracle,
            VehicleHash.Oracle2,
            VehicleHash.Windsor2,
            VehicleHash.Ruiner,
            VehicleHash.Ruiner2,
            VehicleHash.Vigero,
            VehicleHash.Virgo,
            VehicleHash.Virgo2,
            VehicleHash.Virgo3,
            VehicleHash.Rocoto,
            VehicleHash.Radi,
            VehicleHash.XLS,
            VehicleHash.XLS2,
            VehicleHash.Patriot,
            VehicleHash.Warrener,
            VehicleHash.Washington,
            VehicleHash.Taxi
        };

        private static readonly VehicleHash[] Bikes =
        {
            VehicleHash.Akuma,
            VehicleHash.Bati,
            VehicleHash.Bati2,
            VehicleHash.Enduro,
            VehicleHash.BF400,
            VehicleHash.Hakuchou,
            VehicleHash.Sanchez,
            VehicleHash.Sanchez2,
            VehicleHash.PCJ,
            VehicleHash.Lectro,
            VehicleHash.Blazer,
            VehicleHash.Blazer2,
            VehicleHash.Blazer5
        };

        private static readonly VehicleHash[] MilitaryAirplanes =
        {
            VehicleHash.Hydra,
            VehicleHash.Lazer,
            VehicleHash.Cargobob,
            VehicleHash.Savage,
            VehicleHash.Buzzard
        };

        public static VehicleHash GetRandomVehicle()
        {
            var vehicleHashes = Enum.GetValues(typeof(VehicleHash));
            var rnd = new Random();
            var vehicle = (VehicleHash) vehicleHashes.GetValue(Utilities.Between(0, vehicleHashes.Length));

            return vehicle;
        }

        public static VehicleHash GetSafeRandomVehicle()
        {
            return SafeVehicleHashes[Utilities.Between(0, SafeVehicleHashes.Length)];
        }

        public static VehicleHash GetRandomMilitaryPlane()
        {
            var rnd = new Random();

            return MilitaryAirplanes[Utilities.Between(0, MilitaryAirplanes.Length)];
        }

        public static VehicleHash GetRandomBike()
        {
            return Bikes[Utilities.Between(0, Bikes.Length)];
        }
    }
}