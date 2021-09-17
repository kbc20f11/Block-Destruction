using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPBlock
{
    class BrickManager
    {
        /* --- フィールド --- */

        // ブロックのリスト
        private List<Brick> brickList = new List<Brick>();


        /* --- コンストラクタ　--- */
        public BrickManager()
        {
            // 縦四列、横4列になるように並べる
            for (int x = 0; x <= Settings.brickGenerateWidthRange; x += Settings.brickWidth + Settings.brickMarginSide)
            {
                for (int y = 0; y <= Settings.brickGenerateHeightRange; y += Settings.brickHeight + Settings.brickMarginTop)
                {
                    this.brickList.Add(new Brick(Settings.brickOffsetLeft + x, Settings.brickOffsetTop + y, Settings.brickWidth, Settings.brickHeight));
                }
            }
        }


        /* --- メソッド　--- */

        // brickListのゲッタ
        public List<Brick> GetBrickList()
        {
            return this.brickList;
        }

        // brickListを描画するメソッド
        public void Draw(string htmlColor, PaintEventArgs e)
        {
            // 色を取得
            var color = ColorTranslator.FromHtml(htmlColor);

            // ブラシを生成
            SolidBrush blickBrush = new SolidBrush(color);

            // ブロックを描画
            for (int i = 0; i < this.brickList.Count; i++)
            {
                e.Graphics.FillRectangle(blickBrush, brickList[i].GetPosition());
            }
        }

    }
}
