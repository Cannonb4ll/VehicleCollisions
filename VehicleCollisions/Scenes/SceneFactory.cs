namespace VehicleCollisions.Scenes
{
    public class SceneFactory
    {
        private readonly int _sceneNumber;
        
        public SceneFactory(int sceneNumber)
        {
            _sceneNumber = sceneNumber;
        }

        public IScene GetScene()
        {
            switch (_sceneNumber)
            {
                case 1 :
                    return new SevereCrashWithMilitaryConvoy();
                case 2 :
                    return new CarCrashedIntoSewers();
                case 3 :
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
                    return new MilitaryTransportEngineFailure();
                case 10:
                    return new CarCrashWithBike();
                default:
                    return new SevereCrashWithMilitaryConvoy();
            }
        }
    }
}