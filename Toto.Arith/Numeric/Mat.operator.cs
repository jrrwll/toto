namespace Toto.Arith.Numeric
{
    public partial class Mat
    {
        // 数组&矩阵的转换
        public static explicit operator Mat( double[,] array )
        {
            return new Mat( array );
        }


        // 矩阵的加法
        public static Mat operator + (Mat lhs, Mat rhs)
        {
            var m = lhs.X;
            var n = lhs.Y;
            var mat = new Mat(m, n);
            for (var i = 1; i <= m; i++)
            {
                for (var j = 1; j <= n; j++)
                {
                    mat[i, j] = lhs[i, j] + rhs[i, j];
                } 
            }
            return mat;
        }
        public static Mat operator + (Mat lhs, double rhs)
        {
            var m = lhs.X;
            var n = lhs.Y;
            var mat = new Mat(m, n);
            for (var i = 1; i <= m; i++)
            {
                for (var j = 1; j <= n; j++)
                {
                    mat[i, j] = lhs[i, j] + rhs;
                }
            }
            return mat;
        }
        public static Mat operator +(double lhs, Mat rhs)
        {
            return rhs + lhs;
        }
        // 矩阵的减法
        public static Mat operator -(Mat lhs, Mat rhs)
        {
            return lhs + ( -rhs );
        }
        public static Mat operator -(Mat lhs, double rhs)
        {
            return lhs + ( -rhs );
        }
        public static Mat operator -(double lhs, Mat rhs)
        {
            return (- rhs) + lhs;
        }


        // 矩阵的乘法
        public static Mat operator * (Mat lhs, Mat rhs)
        {
            var m = lhs.X;
            var l = lhs.Y;
            var n = rhs.Y;
            if (l != rhs.X) return null;

            var mat = new Mat(lhs.X, rhs.Y);
            for (var i = 1; i <= m; i++)
            {
                for (var j = 1; j <= n; j++)
                {
                    for (var k = 1; k <= l; k++)
                        mat[i, j] += lhs[i, k] * rhs[k, j];
                }
            }
            return mat;
        }
        // 矩阵除法
        public static Mat operator /(Mat lhs, Mat rhs)
        {
            return lhs * rhs.Inverse();
        }


        // 矩阵的数乘
        public static Mat operator *(Mat lhs, double[,] rhs)
        {
            var m = lhs.X;
            var n = lhs.Y;
            var mat = new Mat(m, n);
            for (var i = 1; i <= m; i++)
            {
                for (var j = 1; j <= n; j++)
                {
                    mat[i, j] = lhs[i, j] * rhs[i-1, j-1];
                }
            }
            return mat;
        }
        public static Mat operator *(double[,] lhs, Mat rhs)
        {
            return rhs * lhs;
        }

        public static Mat operator * (Mat lhs, double rhs)
        {
            var m = lhs.X;
            var n = lhs.Y;
            var mat = new Mat(m, n);
            for (var i = 1; i <= m; i++)
            {
                for (var j = 1; j <= n; j++)
                {
                    mat[i, j] = lhs[i, j] * rhs;
                }
            }
            return mat;
        }
        public static Mat operator * (double lhs, Mat rhs)
        {
            return rhs * lhs;
        }
        
        public static Mat operator /(Mat lhs, double rhs)
        {
            return lhs * ( 1 / rhs );
        }


        // 矩阵的幂
        public static Mat operator ^ (Mat lhs, int rhs)
        {
            if (!lhs.IsSquare) return null;
            if (rhs < 0)
            {
                return lhs.Inverse() ^ rhs;
            } else if (rhs == 0)
            {
                return Mats.I( lhs.MinDim );
            }
                
            
            var mat = lhs;
            for (var i = 1; i <= rhs; i++)
            {
                mat *= lhs;
            }
            return mat;
        }
        // 矩阵的转置
        public static Mat operator ~ ( Mat hs )
        {
            return hs.Transpose();
        }
        // 矩阵对负一的数乘
        public static Mat operator -(Mat hs)
        {
            return hs * -1;
        }


    }
}