using System;
using System.Drawing;


//画面上のアイコン

public class アイコン
{
	private Image image;
	private Point location;
	
	public アイコン(Image image, int x, int y) 
	{
		this.image = image;
		location = new Point();
		location.X = x;
		location.Y = y;
	}
	public アイコン(Image image)  : this(image, 0, 0) {}
	public アイコン()             : this(null)        {}

	public Image Image      {get {return image     ;} set {image = value;     }}
	
	public int X        	{get {return location.X;} set {location.X = value;}}
	public int Y        	{get {return location.Y;} set {location.Y = value;}}

	public int Width        {get {return image.Width ;}}
	public int Height       {get {return image.Height;}}
	
	public Rectangle Bounds	{get {return new Rectangle(X, Y, Width, Height);}}
	
}