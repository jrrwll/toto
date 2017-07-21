using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Toto.Froms
{
    public static class GDIPracticalDrawing
    {

        /*
        根据美国法典目录4,第1及2章：
        国旗的宽度: A = 1.0
        国旗的长度: B = 1.9
        联邦范围宽度: C = 0.5385 （7/13, 7条间纹的阔度）
        联邦范围长度: D = 0.76 （1.9 × 2/5, 五份二的国旗长度）
        E = F = 0.0538 （C/10, 联邦范围的十份之一阔度）
        G = H = 0.0633 （D/12, 联邦范围的十二份之一长度）
        星的直径: K = 0.0616
        条纹的宽度: L = 0.0769 （1/13）
        */
        public static void DrawAmericanFlag(Graphics g, float x, float y, float width)
        {
            if (g.SmoothingMode != SmoothingMode.AntiAlias)
                g.SmoothingMode = SmoothingMode.AntiAlias;

            SolidBrush whiteB = new SolidBrush(Color.White);
            SolidBrush blueB = new SolidBrush(Color.Blue);
            SolidBrush redB = new SolidBrush(Color.Red);
            // 长宽之比为19:10
            float height = 10 * width / 19;
            float r = width * 0.0313F;

            // 画白色矩形背景
            g.FillRectangle(whiteB, x, y, width, height);

            // 十三条红白相间横条，画七条红条纹
            for(int i=0; i<7; i++)
            {
                g.FillRectangle( redB, x, y + 2 * i * height / 13, width, height / 13 );
            }

            //
            RectangleF blueBox = new RectangleF( x, y, 2 * width / 5, 7 * height / 13 );
            g.FillRectangle(redB, blueBox);

            //
            float d = blueBox.Width / 40;
            float dx = ( blueBox.Width - 2 * d ) / 11;
            float dy = ( blueBox.Height - 2 * d ) / 9;

            for (int j = 0; j < 9; j++)
            {
                float cy = y + d + j * dy + dy / 2;
                for (int i = 0; i < 11; i++)
                {
                    float cx = x + d + i * dx + dx / 2;
                    if (( i + j ) % 2 == 0) DrawStar(g, whiteB, r, cx, cy);
                }
            }

            whiteB.Dispose();
            blueB.Dispose();
            redB.Dispose();
        }

        public static void DrawStar(Graphics g, SolidBrush brush, float r, float cx, float cy)
        {
            float sin36 = (float)Math.Sin(36.0 * Math.PI / 180.0);
            float sin72 = (float)Math.Sin(72.0 * Math.PI / 180.0);
            float cos36 = (float)Math.Cos(36.0 * Math.PI / 180.0);
            float cos72 = (float)Math.Cos(72.0 * Math.PI / 180.0);
            float r1 = r * cos72 / cos36 ;

            PointF[] pfs = new PointF[10];
            pfs[0] = new PointF(cx, cy - r);
            pfs[1] = new PointF(cx + r1 * sin36, cy - r1 * cos36);
            pfs[2] = new PointF(cx + r * sin72, cy - r * cos72);
            pfs[3] = new PointF(cx + r1 * sin72, cy + r1 * cos72);
            pfs[4] = new PointF(cx + r * sin36, cy + r * cos36);

            pfs[5] = new PointF(cx, cy + r1);
            pfs[6] = new PointF(cx - r* sin36, cy + r * cos36);
            pfs[7] = new PointF(cx - r1 * sin72, cy + r * cos72);
            pfs[8] = new PointF(cx - r * sin72, cy - r * cos72);
            pfs[9] = new PointF(cx - r1 * sin36, cy - r1 * cos36);

            g.FillPolygon(brush, pfs);
        }

    }
}
