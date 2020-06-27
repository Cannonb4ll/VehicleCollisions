using VehicleCollisions.Utils;

namespace VehicleCollisions.Scenes
{
    public class SceneFactory
    {
        private readonly int _sceneNumber;

        public SceneFactory()
        {
            _sceneNumber = Utilities.Between(1, 13);
        }

        public IScene GetScene(VehicleCollisions vehicleCollisions)
        {
            switch (_sceneNumber)
            {
                case 1:
                    return new SevereCrashWithMilitaryConvoy();
                case 2:
                    return new CarCrashedIntoSewers();
                case 3:
                    return new TrafficFightWithCarCrash();
                case 4:
                    return new TrafficLightMalfunctionWithAccident();
                case 5:
                    return new CarHasDrivenIntoChurch();
                case 6:
                    return new BigAccidentOnBridge();
                case 7:
                    return new TrafficCheckAssistance();
                case 8:
                    return new CarDroveThroughRoadBlock();
                case 9:
                    return new MilitaryTransportEngineFailure(vehicleCollisions);
                case 10:
                    return new CarCrashWithBike();
                case 11:
                    return new CrashWithEmergencyServices();
                case 12:
                    return new HighwayAccident();
                case 13:
                    return new CarCrashWithFire();
                default:
                    return new SevereCrashWithMilitaryConvoy();
            }
        }
    }
}