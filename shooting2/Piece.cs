using System;
using System.Drawing;

/// <summary>
/// �e�g���X�u���b�N
/// </summary>
public class Piece
{
	private bool[,] piece;
	private Point location;

	/// <summary>
	/// �u���b�N�̕��͏�� 5
	/// </summary>
	public const int WIDTH = 5;

	/// <summary>
	/// �u���b�N�̍����͏�� 5
	/// </summary>
	public const int HEIGHT = 5;

	/// <summary>
	/// �u���b�N�� X ���W
	/// </summary>
	public int X 
	{
		get { return location.X ; }
		set { location.X = value; }
	}

	/// <summary>
	/// �u���b�N�� Y ���W
	/// </summary>
	public int Y 
	{
		get { return location.Y ; }
		set { location.Y = value; }
	}

	/// <summary>
	/// �u���b�N�Ŗ��߂��Ă���̈�܂ł̏�̃}�[�W��
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
	/// �u���b�N�Ŗ��߂��Ă���̈�܂ł̉��̃}�[�W��
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
	/// �u���b�N�Ŗ��߂��Ă���̈�܂ł̍��̃}�[�W��
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
	/// �u���b�N�Ŗ��߂��Ă���̈�܂ł̉E�̃}�[�W��
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
	/// �u���b�N�̈�̓񎟌��z��
	/// �̈悪�u���b�N�Ŗ��߂��Ă���ꍇ�� true
	/// </summary>
	public bool this[int x, int y] 
	{
		get { return piece[x, y];}
		set { piece[x, y] = value; }
	}

	/// <summary>
	/// �u���b�N�������_���Ő������ĕԂ�
	/// </summary>
	/// <returns>�u���b�N</returns>
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
	/// ���݂̃u���b�N����]�������V�����u���b�N�𐶐�����
	/// </summary>
	/// <returns>���݂̃u���b�N����]�������V�����u���b�N</returns>
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
	/// ���݂̃u���b�N���R�s�[����
	/// </summary>
	/// <returns>���݂̃u���b�N���R�s�[�����V�����C���X�^���X</returns>
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