using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toto.Arith.Numeric
{
    public static class Mats
    {
        public static Mat I(int dim)
        {
            var mat = new Mat(dim);
            for (var i = 1; i <= dim; i++)
            {
                mat[i, i] = 1;
            } 
            return mat;
        }
        public static Mat Zero(int dim)
        {
            return new Mat(dim);
        }
        public static Mat One(int dim)
        {
            var mat = new Mat(dim);
            mat.Fill(1);
            return mat;
        }
        
        public static Mat Diag(Mat vec)
        {
            var mat = new Mat(vec.MaxDim) ;
            for (var i = 1; i <= mat.MinDim; i++)
            {
                mat[i, i] = vec[i];
            }
            return mat;
        }
        
    }
}