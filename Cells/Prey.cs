using System;

namespace OceanProj
{
    public class Prey : Cell
    {

        #region =====----- PUBLIC DATA -----=====

        public override char Image { get; } = Constants.PreyImage;

        #endregion

        #region =====----- PRIVATE DATA -----=====

        private int timeToReproduce;

        #endregion

        #region =====----- CTOR -----=====

        public Prey()
        {
            ResetReproduce();
        }

        #endregion

        public void ResetReproduce()
        {
            timeToReproduce = Constants.OceanRandom.Next(Constants.PreyTimeToReproduce) + (Constants.PreyTimeToReproduce / 2);
        }

        public override void Process(int x, int y, IOceanCell ocean)
        {
            DirectionTools.DirectionRandomIterate((int nx, int ny) => 
            {
                nx += x;
                ny += y;

                Cell? cell = ocean.GetCellOrNull(nx, ny);
                
                if(cell == null)
                {
                    return true;
                }

                if (cell.Image == Constants.EmptyImage)
                {
                    if (timeToReproduce <= 0)
                    {
                        ResetReproduce();
                        ocean.SetCellOrNothing(x, y, this);
                        ocean.SetCellOrNothing(nx, ny, new Prey());
                        ocean.PreysMultiplied();
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
        }
    }
}

