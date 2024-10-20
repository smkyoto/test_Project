using System;
using System.Drawing;

/// <summary>
/// テトリスブロック
/// </summary>
public class Piece
{
	private bool[,] piece;
	private Point location;

	/// <summary>
	/// ブロックの幅は常に 5
	/// </summary>
	public const int WIDTH = 5;

	/// <summary>
	/// ブロックの高さは常に 5
	/// </summary>
	public const int HEIGHT = 5;

	/// <summary>
	/// ブロックの X 座標
	/// </summary>
	public int X 
	{
		get { return location.X ; }
		set { location.X = value; }
	}

	/// <summary>
	/// ブロックの Y 座標
	/// </summary>
	public int Y 
	{
		get { return location.Y ; }
		set { location.Y = value; }
	}

	/// <summary>
	/// ブロックで埋められている領域までの上のマージン
	/// </summary>
	public int MarginTop
	{
		get 
		{
			for(int y = 0 ; y < HEIGHT ; y++) 
			{
				for(int x = 0 ; x < WIDTH ; x++) 
				{
					if (this[x, y]) return y;
				}
			}
			return HEIGHT - 1;
		}
	}

	/// <summary>
	/// ブロックで埋められている領域までの下のマージン
	/// </summary>
	public int MarginBottom 
	{
		get 
		{
			for(int y = HEIGHT - 1 ; y > 0 ; y--) 
			{
				for(int x = 0 ; x < WIDTH ; x++) 
				{
					if (this[x, y]) return y;
				}
			}
			return 0;
		}
	}

	/// <summary>
	/// ブロックで埋められている領域までの左のマージン
	/// </summary>
	public int MarginLeft
	{
		get 
		{
			for(int x = 0 ; x < WIDTH ; x++) 
			{
				for(int y = 0 ; y < HEIGHT ; y++) 
				{
					if (this[x, y]) return x;
				}
			}
			return WIDTH - 1;
		}
	}

	/// <summary>
	/// ブロックで埋められている領域までの右のマージン
	/// </summary>
	public int MarginRight
	{
		get 
		{
			for(int x = WIDTH - 1 ; x > 0 ; x--) 
			{
				for(int y = 0 ; y < HEIGHT ; y++) 
				{
					if (this[x, y]) return x;
				}
			}
			return 0;
		}
	}

	/// <summary>
	/// ブロック領域の二次元配列
	/// 領域がブロックで埋められている場合は true
	/// </summary>
	public bool this[int x, int y] 
	{
		get { return piece[x, y];}
		set { piece[x, y] = value; }
	}

	/// <summary>
	/// ブロックをランダムで生成して返す
	/// </summary>
	/// <returns>ブロック</returns>
	public static Piece Random() 
	{
		Piece result = new Piece();
		Random random = new Random();

		result[2, 2] = true;
		result[2, 3] = true;
		switch(random.Next(7)) 
		{
			case 0:
				result[2, 1] = true;
				result[2, 4] = true;
				break;
			case 1:
				result[2, 1] = true;
				result[3, 3] = true;
				break;
			case 2:
				result[2, 1] = true;
				result[1, 3] = true;
				break;
			case 3:
				result[3, 2] = true;
				result[1, 3] = true;
				break;
			case 4:
				result[1, 2] = true;
				result[3, 3] = true;
				break;
			case 5:
				result[1, 2] = true;
				result[1, 3] = true;
				break;
			case 6:
				result[1, 3] = true;
				result[3, 3] = true;
				break;
		}
		return result;
	}

	/// <summary>
	/// 現在のブロックを回転させた新しいブロックを生成する
	/// </summary>
	/// <returns>現在のブロックを回転させた新しいブロック</returns>
	public Piece TurnPiece() 
	{
		Piece result = new Piece();
		result.X = X;
		result.Y = Y;
		for(int y = 0 ; y < HEIGHT ; y++) 
		{
			for(int x = 0 ; x < WIDTH ; x++) 
			{
				result[HEIGHT - 1 - y, x] = this[x, y];
			}
		}

		return result;
	}

	/// <summary>
	/// 現在のブロックをコピーする
	/// </summary>
	/// <returns>現在のブロックをコピーした新しいインスタンス</returns>
	public Piece Copy() 
	{
		Piece result = new Piece();
		result.X = X;
		result.Y = Y;
		for(int y = 0 ; y < HEIGHT ; y++) 
		{
			for(int x = 0 ; x < WIDTH ; x++) 
			{
				result[x, y] = this[x, y];
			}
		}
		return result;
	}

	public Piece()
	{
		piece = new bool[WIDTH, HEIGHT];
		location = new Point(0, 0);
	}
}