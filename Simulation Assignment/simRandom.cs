using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troschuetz.Random;

namespace Simulation_Assignment
{
    public class simRandom
    {
        //Random _r;
        Generator g;
        ExponentialDistribution exD;
        GammaDistribution gammaD;
        LognormalDistribution lognD;

        public simRandom(int seed)
        {
            g = new StandardGenerator(seed);

            exD = new ExponentialDistribution(g);
            gammaD = new GammaDistribution(g);
            lognD = new LognormalDistribution(g);
            //_r = new Random(seed);
        }

        public double getExponential(double mean)
        {
            exD.Lambda = (1.0 / mean);
            return exD.NextDouble();
            //return Math.Log(1.0 - _r.NextDouble()) / (-(1 / mean));
        }

        public double getGamma(double mean, double alpha)
        {
            gammaD.Alpha = alpha;
            gammaD.Theta = (1.0 / Convert.ToDouble(mean));

            return gammaD.NextDouble();

            /*if (alpha == 1)
                return getExponential(mean);
            else
                return getGamma(mean, alpha - 1) + getExponential(mean);*/
        }

        public double getLogNormal(int mean)
        {
            return 0.0;
        }

        public double getUniform(int min, int max)
        {

            return g.NextDouble(min, max);
        }


    }
}
