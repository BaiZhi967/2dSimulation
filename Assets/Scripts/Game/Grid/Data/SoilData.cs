namespace WhiteZhi.SimulationGame
{
    [System.Serializable]
    public class SoilData
    {
        public bool hasSeed;
        public bool watered;
        public PlantStates plantState = PlantStates.Seed;
    }
}