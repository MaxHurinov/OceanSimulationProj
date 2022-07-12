using System;

namespace OceanProj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var oceanInterface = new OceanRealizeInterface(Constants.OceanWidth, Constants.OceanHeight);
            Ocean ocean = new Ocean(oceanInterface);
            ocean.Run(Constants.CountCycles);
        }
    }
}