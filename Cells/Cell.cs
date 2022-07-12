namespace OceanProj
{
    public class Cell
    {

        #region =====----- PUBLIC DATA -----=====

        public virtual char Image { get; } = Constants.EmptyImage;

        #endregion

        public virtual void Process(int x, int y, IOceanCell ocean)
        {

        }
    }
}
