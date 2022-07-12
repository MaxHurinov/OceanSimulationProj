namespace OceanProj
{
    public interface IOceanCell
    {
        public Cell GetCellOrNull(int x, int y);

        public void SetCellOrNothing(int x, int y, Cell cell);

        public void PreyWasEaten();

        public void PreysMultiplied();

        public void PredatorsDied();

        public void OnPredatorsReproduced();

        public void CheckGameEnd();

    }
}
