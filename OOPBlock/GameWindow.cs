using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows;

namespace OOPBlock
{
    public partial class GameWindow : Form
    {
        /* --- フィールド　--- */

        // ブロックのコレクション
        private List<Brick> brickList;

        // タイマー
        private Timer timer;

        // ボール
        private Ball ball;

        // パドル
        private Paddle paddle;

        // blockManager
        private BrickManager brickManager;

        /* -- コンストラクタ -- */
        public GameWindow()
        {
            // フォームの初期化
            InitializeComponent();

            // ボールを生成
            this.ball = new Ball(new Vector(200, 200), new Vector(4, 4), 10);

            // パドルの生成
            this.paddle = new Paddle(this.Width / 2 , this.Height - 50, 90, 10, this);

            // BrickManagerのインスタンス化
            brickManager = new BrickManager();

            // ブロックリストの生成
            this.brickList = brickManager.GetBrickList();

            /* タイマーの初期化と生成 
             * 以下の処理はsetInterval(Update, 1);とほぼ同じ
             */
            timer = new Timer();
            timer.Interval = 1; // 更新処理を行う間隔
            timer.Tick += new EventHandler(Update);
            timer.Start();
        }

        /* --- メソッド --- */

        /// <summary>
        /// フォームの一番最初の描画を行うメソッド
        /// つまりUnityのStartメソッドと同じ
        /// </summary>
        /// <param name="sender">イベントを送信したオブジェクト</param>
        /// <param name="e">イベント情報</param>
        private void Start(object sender, PaintEventArgs e)
        {
            // ボールを描画
            ball.Draw(Settings.canvasColor, e);

            // パドルを描画
            paddle.Draw(Settings.canvasColor, e);

            // ブロックを描画
            brickManager.Draw(Settings.canvasColor, e);
        }

        /// <summary>
        /// 毎フレーム呼ばれる処理です。
        /// UnityのUpdateメソッドそのものです
        /// </summary>
        /// <param name="sender">イベントを送信したオブジェクト</param>
        /// <param name="e">イベントに関係する引数</param>
        private void Update(object sender, EventArgs e)
        {
            // ボールの移動
            ball.Move();

            // 左右の壁に当たったボールを跳ね返す
            if (ball.GetPosition().X + ball.GetRadius() > this.Bounds.Width || ball.GetPosition().X - ball.GetRadius() < 0)
            {
                // 水平方向の速度を反転する(鉛直方向の速度はそのまま)
                ball.invertX();
            }

            // 上下の壁に当たったボールを跳ね返す
            if (ball.GetPosition().Y - ball.GetRadius() < 0)
            {
                // 鉛直方向の速度を反転する(水平方向の速度はそのまま)
                ball.invertY();
            }

            // Colliderクラスをインスタンス化
            Collider collider = new Collider(ball);

            // パドルとボールのあたり判定
            if (collider.LineCollider(new Vector(this.paddle.GetPosition().Left, this.paddle.GetPosition().Top),
                             new Vector(this.paddle.GetPosition().Right, this.paddle.GetPosition().Top),
                             ball.GetPosition(), ball.GetRadius()))
            {
                // 鉛直方向の速度を判定する(水平方向の速度はそのまま)
                ball.invertY();
            }
            
            // ブロックとボールの衝突処理
            for (int i = 0; i < this.brickList.Count; i++)
            {
                int collision = collider.RectangleCollider(brickList[i].GetPosition(), ball.GetPosition());
                if (collision == 1 || collision == 2)　// 矩形の上側または下側で衝突した場合は鉛直方向の速度を反転する
                {
                    ball.invertY();
                    this.brickList.Remove(brickList[i]); 
                }
                else if (collision == 3 || collision == 4) // 矩形の右側または左側で衝突した場合は水平方向の速度を反転する
                {
                    ball.invertX();
                    this.brickList.Remove(brickList[i]);
                }
            }

            if (isGameClear())
            {
                // タイマーをまず止める(これ重要)
                timer.Stop();

                //ゲームクリアのメッセージを出す
                MessageBox.Show("Game Clear",
                    "Congrats",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // フォームを閉じる
                this.Close();
            }

            if (isGameOver())
            {
                // タイマーをまず止める(これ重要)
                timer.Stop();

                //ゲームオーバーのメッセージを出す
                MessageBox.Show("GAME OVER",
                    "GAME OVER",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                // フォームを閉じる
                this.Close();
            }
            
            // 再描画
            Invalidate();
        }



        /// <summary>
        /// キー入力のイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベントを送信したオブジェクト</param>
        /// <param name="e">キー入力のイベント情報</param>
        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            paddle.Move(sender, e);
        }

        /// <summary>
        /// 。
        /// </summary>
        /// <param name="sender">イベントを送信したオブジェクト</param>
        /// <param name="e">マウスのイベント情報</param>
        private void GameWindow_MouseMove(object sender, MouseEventArgs e)
        {
            paddle.Move(sender, e);
        }

        /// <summary>
        /// クリア判定をするメソッドです。
        /// </summary>
        /// <returns>ゲームがクリア状態であるか(bool)</returns>
        private bool isGameClear()
        {
            return (brickList.Count == 0) ? true : false;
        }

        /// <summary>
        /// ゲームオーバーの判定をするメソッドです。
        /// </summary>
        /// <returns>ゲームオーバー状態であるか(bool)</returns>
        private bool isGameOver()
        {
            return (ball.GetPosition().Y > this.Bounds.Height) ? true : false; // ボールのY座標が画面の高さを超えた場合
        }
    }
}
