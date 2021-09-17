using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace OOPBlock
{
    /// <summary>
    /// ボールクラス
    /// </summary>
    class Ball
    {
        /* --- フィールド　--- */

        // ボールの初期位置の座標
        private Vector position;

        // ボールの速度(速度ベクトルの成分表示)
        private Vector speed;

        // ボールの半径
        private int radius;


        /* --- コンストラクタ　--- */
        public Ball(Vector position, Vector speed, int radius)
        {
            this.position = position;
            this.speed = speed;
            this.radius = radius;
        }


        /* --- メソッド　--- */

        // ボールのpositionのゲッタ
        public Vector GetPosition()
        {
            return this.position;
        }

        // ボールの半径のゲッタ
        public int GetRadius()
        {
            return this.radius;
        }

        // ボールを画面に描画するメソッド
        public void Draw(string htmlColor, PaintEventArgs e)
        {
            // 色を取得
            var color = ColorTranslator.FromHtml(htmlColor);

            // ボールを描画するためのブラシ（SolidBrush）を生成
            SolidBrush ballBrush = new SolidBrush(color);

            // ボールの座標を定義
            float px = (float)this.position.X - radius;
            float py = (float)this.position.Y - radius;

            // ボールを描画
            e.Graphics.FillEllipse(ballBrush, px, py, this.radius * 2, this.radius * 2);
        }

        // ボールを動かすメソッド
        public void Move()
        {
            this.position += speed;
        }

        // ボールの水平方向の速度を反転させるメソッド
        public void invertX()
        {
            // 速度のX軸成分を-1倍する
            this.speed.X *= -1;
        }

        // ボールの鉛直方向の速度を反転させるメソッド
        public void invertY()
        {
            // 速度のY軸成分を -1 倍する
            this.speed.Y *= -1;
        }
    }
}
