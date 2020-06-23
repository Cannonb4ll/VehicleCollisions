using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;

namespace VehicleCollisions.Scenes
{
    internal class CarDroveThroughRoadBlock : IScene
    {
        public float[] AccidentHeadings =
        {
            169.54f,
            184.88f,
            113.12f,
            262.5f,
            268.09f,
            310.84f,
            17.39f,
            252.26f,
            301.94f,
            85.03f,
            350.01f,
            173.12f,
            35.85f
        };

        public int AccidentIndex;

        public Vector3[] AccidentLocations =
        {
            new Vector3(-441.44f, -1190.79f, 54.53f),
            new Vector3(-451.76f, -1396.11f, 31.70f),
            new Vector3(-899.01f, -1770.74f, 37.07f),
            new Vector3(37.4f, -1247.98f, 36.74f),
            new Vector3(748.77f, -1234.9f, 44.74f),
            new Vector3(576.16f, -547.26f, 52.78f),
            new Vector3(-82.66f, -537.86f, 39.97f),
            new Vector3(-61.42f, -541.64f, 31.57f),
            new Vector3(-173.03f, -487.25f, 34.12f),
            new Vector3(-389.58f, -489.68f, 41.27f),
            new Vector3(1075.94f, -1290.79f, 26.04f),
            new Vector3(1042.87f, -1263.83f, 25.30f),
            new Vector3(792.34f, -618.2f, 38.33f)
        };

        public Vector3 RandomCoordinates;

        public CarDroveThroughRoadBlock()
        {
            // Get a random accident
            AccidentIndex = Utilities.Between(0, AccidentLocations.Length);

            // Get the random accident coordinates
            RandomCoordinates = AccidentLocations[AccidentIndex];
        }

        private static Random rnd => new Random();
        public string Title => "Car drove through roadblock";

        public string Description =>
            "We've received a call a car has driven through a road block and is in a though spot.";

        public int ResponseCode => 3;
        public float StartDistance => 120f;

        public string[] Notifications()
        {
            return new[]
            {
                "We've received a call a car has driven through a road block and is in a though spot.",
                "Treat the victim, evaluate the condition of the car and tow it away."
            };
        }

        public Vector3 Coordinates => new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z);

        public PoliceCar[] PoliceCars => new PoliceCar[]
        {
        };

        public PolicePed[] PolicePeds => new PolicePed[]
        {
        };

        public ObjectModel[] ObjectModels => new ObjectModel[]
        {
        };

        public CrashedVehicle[] CrashedCars => new[]
        {
            new CrashedVehicle(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z),
                    AccidentHeadings[AccidentIndex], VehicleUtilities.GetSafeRandomVehicle())
                .SetEngineHealth(Utilities.Between(-50, 200))
                .ShouldHaveBlip(true)
                .SetBlinkingLights(Utilities.RandomBool())
                .ShouldRandomlyBurstTires(Utilities.RandomBool())
                .ShouldRandomlyBeDamaged(true)
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z),
                            AccidentHeadings[AccidentIndex], PedUtilities.GetRandomPed(), VehicleSeat.Driver)
                        .SetHealth(Utilities.Between(0, 100))
                })
        };

        public CivilianPed[] CivilianPeds => new CivilianPed[]
        {
        };

        public void Accept()
        {
        }

        public void Start(Ped[] CivilianPeds = null, Vehicle[] CrashedCars = null)
        {
        }

        public void Finish()
        {
        }

        public async Task RunAdditionalTasks()
        {
        }
    }
}