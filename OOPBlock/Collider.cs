using System;
using System.Drawing;
using System.Windows;

namespace OOPBlock
{
    class Collider
    {
        /* --- フィールド --- */

        // ボールの参照を受け取る
        private Ball ball;


        // コンストラクタ
        public Collider(Ball ball)
        {
            this.ball = ball;
        }


        /* --- メソッド　--- */

        // 内積を返すメソッド
        public double InnerProduct(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y; // 内積計算
        }

        // 外積を返すメソッド
        public double OuterProduct(Vector a, Vector b)
        {
            return a.X * b.Y - b.X * a.Y; // 外積計算
        }

        // パドルと球の衝突判定
        public bool LineCollider(Vector start, Vector end, Vector center, float radius)
        {
            // ひたすらベクトルの計算
            // https://yttm-work.jp/collision/collision_0006.html
            Vector start_to_center = center - start;
            Vector end_to_center = center - end;
            Vector start_to_end = end - start;

            Vector n = start_to_end;
            n.Normalize();

            // 円の中心から線分までの距離
            double distance = Math.Abs(OuterProduct(start_to_center, n));

            // 内積を計算
            double dot1 = InnerProduct(start_to_center, start_to_end);
            double dot2 = InnerProduct(end_to_center, start_to_end);

            // あたり判定(true or false)
            return (dot1 * dot2 <= 0.0f && distance < radius) ? true : false;
        }

        // ブロックと球との衝突判定
        public int RectangleCollider(Rectangle rectangle, Vector ball)
        {
            // 矩形の上側に当たった場合
            if (LineCollider(new Vector(rectangle.Left, rectangle.Top),
                new Vector(rectangle.Right, rectangle.Top), ball, this.ball.GetRadius()))
                return 1;

            // 矩形の下側に当たった場合
            if (LineCollider(new Vector(rectangle.Left, rectangle.Bottom),
                new Vector(rectangle.Right, rectangle.Bottom), ball, this.ball.GetRadius()))
                return 2;

            // 矩形の右側に当たった場合
            if (LineCollider(new Vector(rectangle.Right, rectangle.Top),
                new Vector(rectangle.Right, rectangle.Bottom), ball, this.ball.GetRadius()))
                return 3;

            // 矩形の左側に当たった場合
            if (LineCollider(new Vector(rectangle.Left, rectangle.Top),
                new Vector(rectangle.Left, rectangle.Bottom), ball, this.ball.GetRadius()))
                return 4;

            // どれにも当たってないとき
            return -1;
        }
    }
}
