using System.Drawing;


namespace OOPBlock
{
    /// <summary>
    /// ブロッククラス
    /// </summary>
    class Brick
    {
        /* --- フィールド --- */

        // ブロックの長方形の位置情報
        private Rectangle position;


        /* --- メソッド --- */
        public Brick(int x, int y, int width, int height)
        {
            this.position = new Rectangle(x, y, width, height);
        }


        /* --- メソッド　--- */

        // ブロックの位置のゲッタ
        public Rectangle GetPosition()
        {
            return this.position;
        }




    }
}
