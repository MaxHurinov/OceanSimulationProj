namespace OceanProj
{
    public class Ocean : IOceanCell
    {
        #region =====----- PRIVATE DATA -----=====

        private IOceanInterface _interface;
        private OceanStats _data;

        private Cell[,] _cells;
        private int _row;
        private int _col;

        #endregion

        #region =====----- CTOR -----=====

        public Ocean(OceanRealizeInterface oceanInterface)
        {
            _row = oceanInterface.oceanWidth;
            _col = oceanInterface.oceanHeight;
            _interface = oceanInterface;

            _data.Restore();
            Initialize();
        }

        #endregion

        private void Initialize()
        {
            _cells = new Cell[_col, _row];

            int fieldSize = _col * _row;
            int maxPredators = (int)(fieldSize + Constants.PredatorProportion);
            int maxPreys = (int)(fieldSize + Constants.PreyProportion);
            int maxObstacles = (int)(fieldSize * Constants.ObstacleProportion);

            for (int i = 0; i < _col; i++)
            {
                for (int j = 0; j < _row; j++)
                {
                    if (Constants.OceanRandom.NextDouble() <= Constants.PredatorProportion && _data.NumPredators < maxPredators)
                    {
                        _cells[i, j] = new Predator();
                        _data.NumPredators++;
                    }
                    else if (Constants.OceanRandom.NextDouble() <= Constants.PreyProportion && _data.NumPreys < maxPreys)
                    {
                        _cells[i, j] = new Prey();
                        _data.NumPreys++;
                    }
                    else if (Constants.OceanRandom.NextDouble() <= Constants.ObstacleProportion && _data.NumObstacles < maxObstacles)
                    {
                        _cells[i, j] = new Obstacle();
                        _data.NumObstacles++;
                    }
                    else
                    {
                        _cells[i, j] = new Cell();
                    }
                }
            }
        }

        private bool IsOutOfBorder(int x, int y)
        {
            return x >= _row || y >= _col || x < 0 || y < 0;
        }

        public Cell GetCellOrNull(int x, int y)
        {
            if (IsOutOfBorder(x, y))
            {
                return null;
            }
            return _cells[y, x];
        }

        public void SetCellOrNothing(int x, int y, Cell cell)
        {
            if (!IsOutOfBorder(x, y))
            {
                _cells[y, x] = cell;
            }
        }

        public void PreyWasEaten()
        {
            _data.NumPreys--;
        }

        public void PreysMultiplied()
        {
            _data.NumPreys++;
        }

        public void PredatorsDied()
        {
            _data.NumPredators--;
        }

        public void OnPredatorsReproduced()
        {
            _data.NumPredators++;
        }

        public void CheckGameEnd()
        {
            if (_data.NumPreys == 0 || _data.NumPredators == 0 || _data.NumCycle == Constants.CountCycles)
            {
                _interface.SetCursorToEnd();

                Console.WriteLine("\n*** End Of The Simulation! ***");
                Console.ReadKey();
                System.Environment.Exit(0);
            }
        }

        private void Process()
        {
            var operation = new List<int>();

            for (int i = 0; i < _col; i++)
            {
                for (int j = 0; j < _row; j++)
                {
                    int hash = _cells[i, j].GetHashCode();
                    if (!operation.Contains(hash))
                    {
                        _cells[i, j].Process(j, i, this);
                        operation.Add(hash);
                    }
                }
            }
        }

        public void Run(int cycles)
        {
            for (int i = 0; i < cycles; i++)
            {
                _data.NextCycle();
                _interface.Display(_cells, _data);
                CheckGameEnd();
                Process();
                Thread.Sleep(Constants.CycleInterval);
            }
        }
    }
}
