namespace OceanProj
{
    public class Constants
    {
        #region =====----- PUBLIC DATA -----=====

        public const char ObstacleImage = '#';
        public const char EmptyImage = '-';
        public const char PreyImage = 'f';
        public const char PredatorImage = 'S';

        public const double PredatorProportion = 0.01;
        public const int PredatorTimeToFeed = 20;
        public const int PredatorTimeToReproduce = 30;

        public const double PreyProportion = 0.03;
        public const int PreyTimeToReproduce = 20;

        public const double ObstacleProportion = 0.04;

        public const int CycleInterval = 50;

        public const int CountCycles = 1000;
        public const int OceanWidth = 25;
        public const int OceanHeight = 70;

        #endregion

        public static Random OceanRandom = new Random();    
    }
}
