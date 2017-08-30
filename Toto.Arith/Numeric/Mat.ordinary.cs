using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toto.Arith.Numeric
{
    public partial class Mat
    {
        public void Fill(int ele)
        {
            for (var i = 1; i <= X; i++)
            {
                for (var j = 1; j <= Y; j++)
                {
                    this[i, j] = ele;
                } 
            } 
        }

        public void FillDiag( int ele )
        {
            for (var i = 1; i <= MinDim; i++)
            {
                this[i, i] = ele;
            }
        }
        public void FillDiag(params int[] ele)
        {
            for (var i = 1; i <= MinDim && i <= ele.Length; i++)
            {
                this[i, i] = ele[i-1];
            }
        }

    }
}