using System;

namespace Toto.Arith.Numeric
{
    public partial class Mat
    {
        private double[,] data;
        private int x, y;
        private Dim dim;

        public double[,] _
        {
            get => data;
        }

        public int MinDim
        {
            get => Math.Min(x, y);
        }
        public int MaxDim
        {
            get => Math.Max(x, y);
        }
        
        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }

        public Dim Dim
        {
            get => dim;
        }

        public Mat( int m, int n )
        {
            X = m;
            Y = n;
            data = new double[X, Y];
            dim = new Dim(X, Y);
        }

        public Mat( int m ):this(m, m)
        {
        }

        public Mat( double[,] array )
        {
            X = array.GetLength(0);
            Y = array.GetLength(1);
            data = array;
        }


        public double this[int i, int j]
        {
            get => data[i - 1, j - 1];
            set => data[i - 1, j - 1] = value;
        }
        public double this[int i]
        {
            get
            {
                if (IsRowVec) return this[1, i];
                else if (IsColumnVec) return this[i, 1];
                else return this[i, i];

            }
            set
            {
                if (IsRowVec) this[1, i] = value ;
                else if (IsColumnVec)  this[i, 1] = value;
                else this[i, i] = value;

            }
        }

        //  逆矩阵
        public Mat Inverse(  )
        {
            return null;
        }
        
        // 转置矩阵
        public Mat Transpose( )
        {
            var m = X;
            var n = Y;
            var mat = new Mat(n, m);
            for (var i = 1; i <= m; i++)
            {
                for (var j = 1; j <= n; j++)
                {
                    mat[j, i] = this[i, j];
                }
            }
            return mat;
        }


    }


    
}
