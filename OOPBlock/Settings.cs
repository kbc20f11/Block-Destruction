
namespace OOPBlock
{
    class Settings
    {
        // 一つ一つのブロックの幅
        public static int brickWidth = 80;

        // 一つ一つのブロックの高さ
        public static int brickHeight = 20;

        // ウィンドウ最上部から一番上の列のブロックまでのオフセット
        public static int brickOffsetTop = 30;

        // ウィンドウ左端から一番左のブロックまでのオフセット
        public static int brickOffsetLeft = 30;

        // 一つ一つのブロックの間隔(上下)
        public static int brickMarginTop = 20;

        // 一つ一つのブロックの間隔(左右)
        public static int brickMarginSide = 20;

        // ブロックを生成する範囲(高さ)
        public static int brickGenerateHeightRange = 150;

        // ブロックを生成する範囲(幅)
        public static int brickGenerateWidthRange = 320;

        // 塗りつぶしの色
        public static string canvasColor = "#0095DD";
    }
}
