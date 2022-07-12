namespace OceanProj
{
    public class Predator : Prey
    {
        #region =====----- PUBLIC DATA -----=====

        public override char Image { get; } = Constants.PredatorImage;

        #endregion

        #region =====----- PRIVATE DATA -----=====

        private int timeToReproduce = Constants.PredatorTimeToReproduce;
        private int timeToFeed = Constants.PredatorTimeToFeed;

        #endregion

        public override void Process(int x, int y, IOceanCell ocean)
        {
            if (timeToFeed <= 0)
            {
                ocean.SetCellOrNothing(x, y, new Cell());
                ocean.PredatorsDied();
                return;
            }

            DirectionTools.DirectionRandomIterate((int nx, int ny) =>
            {
                nx += x;
                ny += y;

                Cell? cell = ocean.GetCellOrNull(nx, ny);

                if (cell == null)
                {
                    return true;
                }

                if (cell.Image == Constants.PreyImage)
                {
                    timeToFeed = Constants.PredatorTimeToFeed;
                    cell = new Cell();
                    ocean.SetCellOrNothing(nx, ny, cell);
                    ocean.PreyWasEaten();
                    return false;
                }
                if (cell.Image == Constants.EmptyImage)
                {
                    if (timeToReproduce <= 0)
                    {
                        ResetReproduce();
                        ocean.SetCellOrNothing(x, y, this);
                        ocean.SetCellOrNothing(nx, ny, new Predator());
                        ocean.OnPredatorsReproduced();
                    }
                    else
                    {
                        ocean.SetCellOrNothing(x, y, new Cell());
                        ocean.SetCellOrNothing(nx, ny, this);
                    }
                    return false;
                }
                return true;
            });

            timeToReproduce--;
            timeToFeed--;
        }
    }
}
