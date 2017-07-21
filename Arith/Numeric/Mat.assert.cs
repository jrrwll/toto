using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toto.Arith.Numeric
{
    public partial class Mat
    {
        // 方阵
        public bool IsSquare
        {
            get => X == Y;
        }

        // 行向量
        public bool IsRowVec
        {
           get => X == 1;
        }
        // 列向量
        public bool IsColumnVec
        {
            get => Y == 1;
        }


    }

    public struct Dim
    {
        public int X;
        public int Y;
        public Dim(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Dim lhs, Dim rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }
        public static bool operator !=(Dim lhs, Dim rhs)
        {
            return ! (lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format( "Toto.Arith.Numeric.Dim({0}, {1})", X, Y );
        }


    }

}