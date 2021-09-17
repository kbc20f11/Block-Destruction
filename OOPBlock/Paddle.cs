using System.Drawing;
using System.Windows.Forms;

namespace OOPBlock
{
    /// <summary>
    /// パドルクラス
    /// </summary>
    class Paddle
    {
        /* --- フィールド　--- */

        // パドルの長方形の位置情報
        private Rectangle position;

        // ゲームウィンドウの参照を受け取る
        private Form GameWindow;


        /* --- コンストラクタ　--- */
        public Paddle(int x, int y, int width, int height, Form form)
        {
            position = new Rectangle(x,y,width, height);
            this.GameWindow = form;
        }


        /* --- メソッド --- */

        // positionのゲッタ
        public Rectangle GetPosition()
        {
            return this.position;
        }

        // パドルを画面に描画するメソッド
        public void Draw(string htmlColor, PaintEventArgs e)
        {
            // 色を取得
            var color = ColorTranslator.FromHtml(htmlColor);

            // パドルを描画するためのブラシ（SolidBrush）を生成
            SolidBrush paddleBrush = new SolidBrush(color);

            // パドルを描画
            e.Graphics.FillRectangle(paddleBrush, position);
        }

        // 矢印キーを入力したときにパドルを動かすメソッド
        public void Move(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && this.position.X > 0) // ←キーが押されたとき かつ パドルが画面外からはみ出してないとき
            {
                this.position.Y -= 30;
            }
            else if (e.KeyCode == Keys.Right && this.position.X < this.GameWindow.Bounds.Width - this.position.Width) // →キーが押されたとき かつ パドルが画面外からはみ出してないとき
            {
                this.position.X += 30;
            }
        }

        // マウスの動きでパドルを動かすメソッド
        public void Move(object sender, MouseEventArgs e)
        {
            // マウスが画面内にある場合、マウスとパドルの中心の水平方向の座標を一致させる
            if (e.X > 0 && e.X < this.GameWindow.Bounds.Width)
            {
                this.position.X = e.X - (this.position.Width / 2);
            }
        }
    }
}
