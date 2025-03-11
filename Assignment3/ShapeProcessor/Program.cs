using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeProcessor
{
    abstract class Shape
    {
        protected double width;
        protected double height;
        protected Shape()
        {
            this.width = 0;
            this.height = 0;
        }
        protected Shape(double width, double height)
        {
            this.width = width;
            this.height = height;
        }   
        protected Shape(double length)
        {
            this.width=length;
            this.height=length;
        }
        virtual public void describe()
        {
            Console.WriteLine("这是一个未知形状");
        }
        public abstract double GetArea();
        public abstract bool Verify();
    }
    class Rectangle:Shape
    {
        public Rectangle() : base() { }
        public Rectangle(double width, double height) : base(width, height) { }
        public override double GetArea()
        {
            return width * height;
        }
        public override bool Verify()
        {
            return width>0 && height>0;
        }
        public override void describe()
        {
            Console.WriteLine($"这是一个宽为{width}，长为{height}的矩形");
        }
    }
    class Square:Shape
    {
        public Square() : base() { }
        public Square(double length):base(length) { }
        public override double GetArea()
        {
            return width*height;
        }
        public override bool Verify()
        {
            return width>0 && width==height;
        }
        public override void describe()
        {
            Console.WriteLine($"这是一个边长为{width}的正方形");
        }
    }
    class Triangle : Shape
    {
        public Triangle() : base() { }
        public Triangle(double width,double height) : base(width,height) { }
        public override double GetArea()
        {
            return height*width/2;
        }
        public override bool Verify()
        {
            return width > 0 && height > 0;
        }
        public override void describe()
        {
            Console.WriteLine($"这是一个宽为{width}，高为{height}的三角形");
        }
    }
    class Circle : Shape
    {
        double diameter;
        public Circle() : base() { }
        public Circle(double diameter) : base(diameter)
        {
            this.diameter = diameter;
        }
        public override double GetArea()
        {
            return diameter * diameter * Math.PI / 4;
        }
        public override bool Verify()
        {
            return diameter > 0;
        }
        public override void describe()
        {
            Console.WriteLine($"这是一个直径为{diameter}的圆形");
        }
    }


    class ShapeFactory
    {
        public static int GenerateRandomSeed()
        {
            return Guid.NewGuid().GetHashCode();
        }

        private static double NextDouble(Random ran, double minValue, double maxValue)
        {
            return Math.Round(ran.NextDouble() * (maxValue - minValue) + minValue,2);
        }
        public static Shape CreateRandomShape()
        {
            Shape s;
            Random ran = new Random(GenerateRandomSeed());
            int n = ran.Next(4);
            double a = NextDouble(ran, 0, 10), b = NextDouble(ran, 0, 10);
            switch (n)
            {
                case 0:
                    s = new Rectangle(a,b);
                    break;
                case 1:
                    s = new Square(a);
                    break;
                case 2:
                    s = new Triangle(a,b);
                    break;
                case 3:
                    s = new Circle(a);
                    break;
                default:
                    s=new Rectangle(a,b);
                    break;
            }
            return s;
        }
    }

    internal class Program 
    {
        static void Main(string[] args)
        {
            double sumArea = 0;
            for(int i=1;i<=10 ;i++)
            {
                Shape s = ShapeFactory.CreateRandomShape();
                Console.WriteLine($"生成第{i}个形状：");
                s.describe();
                Console.WriteLine($"形状验证的结果：{s.Verify()}");
                sumArea += s.GetArea();
            }
            Console.WriteLine($"所有形状的面积和为{sumArea}");
        }
    }
}
