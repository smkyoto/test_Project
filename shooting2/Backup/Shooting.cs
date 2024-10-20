using System;
using System.Collections; //for .NET 1.1
//using System.Collections.Generic; //for .NET 2.0
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;


public class Shooting : Form
{
	private Timer timer;
	private Bitmap ���;
	private �A�C�R�� �����@��;
	private ArrayList �����~�T�C���z��; //for .NET 1.1
	//private List<�A�C�R��> �����~�T�C���z��; //for .NET 2.0
	private ArrayList �G�@�z��; //for .NET 1.1
	//private List<�A�C�R��> �G�@�z��; //for .NET 2.0
	private ArrayList �G�~�T�C���z��; //for .NET 1.1
	//private List<�A�C�R��> �G�~�T�C���z��; //for .NET 2.0
	private Random random;
	private Image �����@�̉摜;
	private Image �����~�T�C���摜;
	private Image �G�@�摜;
	private Image �G�~�T�C���摜;
	private int   �_��=0;
	private Audio ������,���蔭�ˉ�,�������ˉ�;

	private void �A�C�R���z��\��(Graphics g, ArrayList A)      //for .NET 1.1
	// private void �A�C�R���z��\��(Graphics g, List A)   //for .NET 2.0
	{
		for(int i = 0 ; i < A.Count ; i++)
		{
			�A�C�R�� M = (�A�C�R��)A[i];
			g.DrawImage(M.Image, M.X, M.Y);
		}
	}
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint (e);

		Graphics g = Graphics.FromImage(���);
		
		Brush brush = new SolidBrush(Color.White);
		
		g.FillRectangle(brush, 0, 0, ���.Width, ���.Height);

		g.DrawImage(�����@��.Image, �����@��.X, �����@��.Y);
		�A�C�R���z��\��(g, �����~�T�C���z��);
		�A�C�R���z��\��(g, �G�@�z��);
		�A�C�R���z��\��(g, �G�~�T�C���z��);
		
		e.Graphics.DrawImage(���, 0, 0);
		//�_���\��
		Brush brush2= new SolidBrush(Color.Black);
		Font font =new Font("Arial",20,FontStyle.Underline | FontStyle.Bold);
		e.Graphics.DrawString(�_��.ToString(),font,brush2,10,10);
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown (e);
		switch (e.KeyCode)
		{
			case Keys.Left  : �����@��.X -= 10; break;
			case Keys.Right : �����@��.X += 10; break;
			case Keys.Space : ����()          ; break;
			case Keys.Escape: �J�n()          ; break;
		}
		Invalidate();
	}

	private void �G�̐���()
	{
		if(�G�@�z��.Count < 5) 
		{
			�A�C�R�� �G�@ = new �A�C�R��(
				�G�@�摜, random.Next(���.Width - �G�@�摜.Width), 0);
			�G�@�z��.Add(�G�@);
		}
	}

	private void �G�̃~�T�C������()
	{
		if(random.Next(100) < 5) 
		{
			
			
			���蔭�ˉ�.Stop(); 	      
			���蔭�ˉ�.Play();

			int index = random.Next(�G�@�z��.Count);
			�A�C�R�� �G�@ = (�A�C�R��)�G�@�z��[index];
			�A�C�R�� �~�T�C�� = new �A�C�R��(
				�G�~�T�C���摜,
				�G�@.X + �G�@.Width / 2 - �G�@.Width / 2,
				�G�@.Y + �G�@.Height
				);
			�G�~�T�C���z��.Add(�~�T�C��);
		
		}
	}

	private void �G�~�T�C���ړ�()
	{
		for(int i = 0 ; i < �G�~�T�C���z��.Count ; i++) 
		{
			�A�C�R�� �~�T�C�� = (�A�C�R��)�G�~�T�C���z��[i];
			�~�T�C��.Y += 5;
			if (�~�T�C��.Y >= ���.Height) �G�~�T�C���z��.RemoveAt(i);
		}
	}
	private void �����~�T�C���ړ�()
	{
		for(int i = 0 ; i < �����~�T�C���z��.Count ; i++)
		{
			�A�C�R�� �~�T�C�� = (�A�C�R��)�����~�T�C���z��[i];
			�~�T�C��.Y -= 5;
			if(�~�T�C��.Y + �~�T�C��.Height < 0) �����~�T�C���z��.RemoveAt(i);
		}
	}
	private void �G�@��ʊO�폜()
	{
		for(int i = 0 ; i < �G�@�z��.Count ; i++) 
		{
			�A�C�R�� �G�@ = (�A�C�R��)�G�@�z��[i];
			�G�@.Y += 4;

			if(i % 2 == 0) �G�@.X += 2;
			else           �G�@.X -= 2;

			if     (�G�@.X < 0)                       �G�@�z��.RemoveAt(i);
			else if(�G�@.X + �G�@.Width > ���.Width) �G�@�z��.RemoveAt(i);
			else if(�G�@.Y > ���.Height)		 	  �G�@�z��.RemoveAt(i);
		}

	}
	private void ���e()
	{
		
		
		������.Play();

		�����@��.Image= Image.FromFile("���e.gif");
		Invalidate();
		timer.Stop();

	}
	private void �G�̓����蔻��()
	{
		for(int i = 0 ; i < �G�@�z��.Count ; i++) 
		{
			�A�C�R�� �G�@ = (�A�C�R��)�G�@�z��[i];
			
			//���[�U�[���G�ɐڐG���Ă��邩�ǂ����𔻒�
			if(�G�@.Bounds.IntersectsWith(�����@��.Bounds))
			{ 
				�G�@�z��.RemoveAt(i);
			
				���e();	break;
			}

			//���[�U�[�~�T�C�����G�ɐڐG���Ă��邩�ǂ����𔻒�
			for(int j = 0 ; j < �����~�T�C���z��.Count ; j++) 
			{
				�A�C�R�� �~�T�C�� = (�A�C�R��)�����~�T�C���z��[j];
				if(�G�@.Bounds.IntersectsWith(�~�T�C��.Bounds))
				{
					�������ˉ�=new Audio("��������.wav");
					�������ˉ�.Play();

					�_��=�_��+1;
					�����~�T�C���z��.RemoveAt(j);
					�G�@�z��.RemoveAt(i);
					break;
				}
			}
		}
	}
	private void �G�~�T�C���Ƃ̐ڐG����()
	{
		for(int i = 0 ; i < �G�~�T�C���z��.Count ; i++) 
		{

			�A�C�R�� �~�T�C�� = (�A�C�R��)�G�~�T�C���z��[i];
			if(�~�T�C��.Bounds.IntersectsWith(�����@��.Bounds))
			{
				�G�~�T�C���z��.RemoveAt(i);
				���e();	break;
			}
		}
	}

	private void �^�C�}������(object sender, EventArgs e) 
	{
		�G�̐���();
		�G�̃~�T�C������();
		�G�~�T�C���ړ�();
		�����~�T�C���ړ�();

		�G�@��ʊO�폜();
	    �G�̓����蔻��();
		�G�~�T�C���Ƃ̐ڐG����();
		Invalidate();
	}

	public bool ����() 
	{
		if(�����~�T�C���z��.Count >= 3) return false;
		 �������ˉ�.Stop();
		�������ˉ�.Play();

		int x = �����@��.X + (�����@��.Width / 2) - (�����~�T�C���摜.Width / 2);
		int y = �����@��.Y - �����~�T�C���摜.Height;
		�A�C�R�� �~�T�C�� = new �A�C�R��(�����~�T�C���摜, x, y);
		�����~�T�C���z��.Add(�~�T�C��);
		return true;
	}
	private void �J�n()
	{
		SetStyle(ControlStyles.DoubleBuffer |
			     ControlStyles.UserPaint    |
			     ControlStyles.AllPaintingInWmPaint, true);

		���    = new Bitmap(640, 480);
		random  = new Random();
		������=new Audio("������.wav");
		���蔭�ˉ�=new Audio("���蔭��.wav");
		�������ˉ�=new Audio("��������.wav");


		�����~�T�C���z�� = new ArrayList(); //for .NET 1.1
		//�����~�T�C���z�� = new List<�A�C�R��>(); //for .NET 2.0
		�G�@�z�� = new ArrayList(); //for .NET 1.1
		//�G�@�z�� = new List<�A�C�R��>(); //for .NET 2.0
		�G�~�T�C���z�� = new ArrayList(); //for .NET 1.1
		//�G�~�T�C���z�� = new List<�A�C�R��>(); //for .NET 2.0
		�����@�̉摜 = Image.FromFile("����.gif");
		�����~�T�C���摜 = Image.FromFile("�����~�T�C��.gif");
		�G�@�摜 = Image.FromFile("�G��.gif");
		�G�~�T�C���摜 = Image.FromFile("�G���~�T�C��.gif");
		�����@�� = new �A�C�R��(
			�����@�̉摜, ���.Width / 2 - �����@�̉摜.Width / 2,
			���.Height - �����@�̉摜.Height
			);

		�_�� = 0;
		
		timer           = new Timer();
		timer.Interval  = 30;
		timer.Tick     += new EventHandler(�^�C�}������);
		timer.Start();
	}
	public Shooting() 
	{
		�J�n();
	}
	static void Main() 
	{
		Form form = new Shooting();
		form.ClientSize = new Size(640, 480);
		Application.Run(form);
	}

	private void InitializeComponent()
	{
		// 
		// Shooting
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
		this.ClientSize = new System.Drawing.Size(292, 266);
		this.Name = "Shooting";
		this.Load += new System.EventHandler(this.Shooting_Load);

	}

	private void Shooting_Load(object sender, System.EventArgs e)
	{
	
	}

	
}