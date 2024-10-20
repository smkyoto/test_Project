using System;
using System.Collections; //for .NET 1.1
//using System.Collections.Generic; //for .NET 2.0
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;


public class Shooting : Form
{
	private Timer timer;
	private Bitmap 画面;
	private アイコン 味方機体;
	private ArrayList 味方ミサイル配列; //for .NET 1.1
	//private List<アイコン> 味方ミサイル配列; //for .NET 2.0
	private ArrayList 敵機配列; //for .NET 1.1
	//private List<アイコン> 敵機配列; //for .NET 2.0
	private ArrayList 敵ミサイル配列; //for .NET 1.1
	//private List<アイコン> 敵ミサイル配列; //for .NET 2.0
	private Random random;
	private Image 味方機体画像;
	private Image 味方ミサイル画像;
	private Image 敵機画像;
	private Image 敵ミサイル画像;
	private int   点数=0;
	private Audio 爆発音,相手発射音,味方発射音;

	private void アイコン配列表示(Graphics g, ArrayList A)      //for .NET 1.1
	// private void アイコン配列表示(Graphics g, List A)   //for .NET 2.0
	{
		for(int i = 0 ; i < A.Count ; i++)
		{
			アイコン M = (アイコン)A[i];
			g.DrawImage(M.Image, M.X, M.Y);
		}
	}
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint (e);

		Graphics g = Graphics.FromImage(画面);
		
		Brush brush = new SolidBrush(Color.White);
		
		g.FillRectangle(brush, 0, 0, 画面.Width, 画面.Height);

		g.DrawImage(味方機体.Image, 味方機体.X, 味方機体.Y);
		アイコン配列表示(g, 味方ミサイル配列);
		アイコン配列表示(g, 敵機配列);
		アイコン配列表示(g, 敵ミサイル配列);
		
		e.Graphics.DrawImage(画面, 0, 0);
		//点数表示
		Brush brush2= new SolidBrush(Color.Black);
		Font font =new Font("Arial",20,FontStyle.Underline | FontStyle.Bold);
		e.Graphics.DrawString(点数.ToString(),font,brush2,10,10);
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown (e);
		switch (e.KeyCode)
		{
			case Keys.Left  : 味方機体.X -= 10; break;
			case Keys.Right : 味方機体.X += 10; break;
			case Keys.Space : 発射()          ; break;
			case Keys.Escape: 開始()          ; break;
		}
		Invalidate();
	}

	private void 敵の生成()
	{
		if(敵機配列.Count < 5) 
		{
			アイコン 敵機 = new アイコン(
				敵機画像, random.Next(画面.Width - 敵機画像.Width), 0);
			敵機配列.Add(敵機);
		}
	}

	private void 敵のミサイル発射()
	{
		if(random.Next(100) < 5) 
		{
			
			
			相手発射音.Stop(); 	      
			相手発射音.Play();

			int index = random.Next(敵機配列.Count);
			アイコン 敵機 = (アイコン)敵機配列[index];
			アイコン ミサイル = new アイコン(
				敵ミサイル画像,
				敵機.X + 敵機.Width / 2 - 敵機.Width / 2,
				敵機.Y + 敵機.Height
				);
			敵ミサイル配列.Add(ミサイル);
		
		}
	}

	private void 敵ミサイル移動()
	{
		for(int i = 0 ; i < 敵ミサイル配列.Count ; i++) 
		{
			アイコン ミサイル = (アイコン)敵ミサイル配列[i];
			ミサイル.Y += 5;
			if (ミサイル.Y >= 画面.Height) 敵ミサイル配列.RemoveAt(i);
		}
	}
	private void 味方ミサイル移動()
	{
		for(int i = 0 ; i < 味方ミサイル配列.Count ; i++)
		{
			アイコン ミサイル = (アイコン)味方ミサイル配列[i];
			ミサイル.Y -= 5;
			if(ミサイル.Y + ミサイル.Height < 0) 味方ミサイル配列.RemoveAt(i);
		}
	}
	private void 敵機画面外削除()
	{
		for(int i = 0 ; i < 敵機配列.Count ; i++) 
		{
			アイコン 敵機 = (アイコン)敵機配列[i];
			敵機.Y += 4;

			if(i % 2 == 0) 敵機.X += 2;
			else           敵機.X -= 2;

			if     (敵機.X < 0)                       敵機配列.RemoveAt(i);
			else if(敵機.X + 敵機.Width > 画面.Width) 敵機配列.RemoveAt(i);
			else if(敵機.Y > 画面.Height)		 	  敵機配列.RemoveAt(i);
		}

	}
	private void 着弾()
	{
		
		
		爆発音.Play();

		味方機体.Image= Image.FromFile("着弾.gif");
		Invalidate();
		timer.Stop();

	}
	private void 敵の当たり判定()
	{
		for(int i = 0 ; i < 敵機配列.Count ; i++) 
		{
			アイコン 敵機 = (アイコン)敵機配列[i];
			
			//ユーザーが敵に接触しているかどうかを判定
			if(敵機.Bounds.IntersectsWith(味方機体.Bounds))
			{ 
				敵機配列.RemoveAt(i);
			
				着弾();	break;
			}

			//ユーザーミサイルが敵に接触しているかどうかを判定
			for(int j = 0 ; j < 味方ミサイル配列.Count ; j++) 
			{
				アイコン ミサイル = (アイコン)味方ミサイル配列[j];
				if(敵機.Bounds.IntersectsWith(ミサイル.Bounds))
				{
					味方発射音=new Audio("味方発射.wav");
					味方発射音.Play();

					点数=点数+1;
					味方ミサイル配列.RemoveAt(j);
					敵機配列.RemoveAt(i);
					break;
				}
			}
		}
	}
	private void 敵ミサイルとの接触判定()
	{
		for(int i = 0 ; i < 敵ミサイル配列.Count ; i++) 
		{

			アイコン ミサイル = (アイコン)敵ミサイル配列[i];
			if(ミサイル.Bounds.IntersectsWith(味方機体.Bounds))
			{
				敵ミサイル配列.RemoveAt(i);
				着弾();	break;
			}
		}
	}

	private void タイマ割込み(object sender, EventArgs e) 
	{
		敵の生成();
		敵のミサイル発射();
		敵ミサイル移動();
		味方ミサイル移動();

		敵機画面外削除();
	    敵の当たり判定();
		敵ミサイルとの接触判定();
		Invalidate();
	}

	public bool 発射() 
	{
		if(味方ミサイル配列.Count >= 3) return false;
		 味方発射音.Stop();
		味方発射音.Play();

		int x = 味方機体.X + (味方機体.Width / 2) - (味方ミサイル画像.Width / 2);
		int y = 味方機体.Y - 味方ミサイル画像.Height;
		アイコン ミサイル = new アイコン(味方ミサイル画像, x, y);
		味方ミサイル配列.Add(ミサイル);
		return true;
	}
	private void 開始()
	{
		SetStyle(ControlStyles.DoubleBuffer |
			     ControlStyles.UserPaint    |
			     ControlStyles.AllPaintingInWmPaint, true);

		画面    = new Bitmap(640, 480);
		random  = new Random();
		爆発音=new Audio("爆発音.wav");
		相手発射音=new Audio("相手発射.wav");
		味方発射音=new Audio("味方発射.wav");


		味方ミサイル配列 = new ArrayList(); //for .NET 1.1
		//味方ミサイル配列 = new List<アイコン>(); //for .NET 2.0
		敵機配列 = new ArrayList(); //for .NET 1.1
		//敵機配列 = new List<アイコン>(); //for .NET 2.0
		敵ミサイル配列 = new ArrayList(); //for .NET 1.1
		//敵ミサイル配列 = new List<アイコン>(); //for .NET 2.0
		味方機体画像 = Image.FromFile("味方.gif");
		味方ミサイル画像 = Image.FromFile("味方ミサイル.gif");
		敵機画像 = Image.FromFile("敵方.gif");
		敵ミサイル画像 = Image.FromFile("敵方ミサイル.gif");
		味方機体 = new アイコン(
			味方機体画像, 画面.Width / 2 - 味方機体画像.Width / 2,
			画面.Height - 味方機体画像.Height
			);

		点数 = 0;
		
		timer           = new Timer();
		timer.Interval  = 30;
		timer.Tick     += new EventHandler(タイマ割込み);
		timer.Start();
	}
	public Shooting() 
	{
		開始();
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