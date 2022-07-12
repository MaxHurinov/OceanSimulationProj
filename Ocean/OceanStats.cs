namespace OceanProj
{
    public struct OceanStats
    {

        #region =====----- PROPERTIES -----=====

        public int NumPreys { get; set; }

        public int NumPredators { get; set; }

        public int NumObstacles { get; set; }

        public int NumCycle { get; set; }

        #endregion

        public void Restore()
        {
            NumPreys = 0;
            NumPredators = 0;
            NumObstacles = 0;
            NumCycle = 0;
        }

        public void NextCycle()
        {
            NumCycle++;
        }

    }
}
