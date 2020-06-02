using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace classGeometry
{
    class Vector
    {
        int x, y;
        /// <summary>
        /// Создание вектора по координатам вектора
        /// </summary>
        /// <param name="x">координата x</param>
        /// <param name="y">координата y</param>
        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// Создание вектора по точкам
        /// </summary>
        /// <param name="start">Точка начала вектора</param>
        /// <param name="end">Точка конца вектора</param>
        public Vector(Point start, Point end)
        {
            x = end.x - start.x;
            y = end.y - start.y;
        }

        public double Len()
        {
            return Math.Sqrt(x * x + y * y);
        }

        public int scalar(Vector a)
        {
            return this.x * a.x + this.y * a.y;
        }

        public bool RightTurn(Vector a)
        {
            return Psescalar(a) < 0;
        }

        public bool LeftTurn(Vector a)
        {
            return Psescalar(a) > 0;
        }

        public int Psescalar(Vector a)
        {
            return this.x * a.y - this.y * a.x;
        }

        public double Angle(Vector a)
        {
            return Math.Acos(this.scalar(a) / (this.Len() * a.Len()));
        }
    }
    public class Point
    {
        internal int x;
        internal int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(string s)
        {
            try
            {
                string[] temp = s.Split();
                x = Convert.ToInt32(temp[0]);
                y = Convert.ToInt32(temp[1]);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public override string ToString()
        {
            return string.Format("({0}; {1})", x, y);
        }
    }

    public class Polygon
    {
        List<Point> listPoint = new List<Point>();
        public Polygon() { }
        public int Count { get { return listPoint.Count; } }
        public System.Drawing.Point GetPoint(int index)
        {
            System.Drawing.Point p = new System.Drawing.Point();
            try
            {
                p = new System.Drawing.Point(listPoint[index].x, listPoint[index].y);
              
            }
            catch { }
            return p;
        }
        public Polygon(string s)
        {
            try
            {
                string[] temp = s.Split();
                for (int i = 0; i < temp.Length; i += 2)
                {
                    Point t = new Point(Convert.ToInt32(temp[i]), Convert.ToInt32(temp[i + 1]));
                    //Point t = new Point(temp[i] + " " + temp[i + 1]);
                    listPoint.Add(t);
                }
            }
            catch
            {
                MessageBox.Show("не получилося)))");
            }
        }

        public void Add(Point p)
        {
            listPoint.Add(p);
        }

        /// <summary>
        /// Сравнение двух точек
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int ComparisonMinXY(Point a, Point b)
        {
            if ((a.x < b.x) || (a.x == b.x && a.y < b.y))
                return -1;
            else
                return 1;
        }

        /// <summary>
        /// Построение выпуклой оболочки
        /// </summary>
        public Polygon СonvexHull()
        {
            //отсортировать все точки по координате x, при равенстве по y
            listPoint.Sort(ComparisonMinXY);

            //самая левая точка точно находится в оболочке
            Point startPoint = listPoint[0];
            //самая правя точка точно находится в оболочке
            Point endPoint = listPoint.Last();
            //построим вернюю половину оболочки
            List<Point> upHull = new List<Point>();
            upHull.Add(startPoint);
            //построим нижнюю половину оболочки
            List<Point> downHull = new List<Point>();
            downHull.Add(startPoint);

            //просмотрим все точки слева на право 
            for (int i = 1; i < listPoint.Count; i++)
            {
                Point current = listPoint[i];
                //если точки начало, текущая и конец образуют поворот против часовой стрелки (правый), 
                //то точка может войти в верхнюю оболочку
                if (RightTurn(startPoint, current, endPoint))
                {
                    //выкинуть из верхней оболочки все точки, 
                    //которые не образуют правый поворот на  последних 2-х точках списка и текущей
                    while (upHull.Count > 1 && LeftTurn(upHull[upHull.Count - 2], upHull.Last(), current))
                        upHull.RemoveAt(upHull.Count - 1);
                    //закинуть текущую точку в список
                    upHull.Add(current);
                }
                //аналогично для нижней оболочки
                if (LeftTurn(startPoint, current, endPoint))
                {
                    //выкинуть из нижней оболочки все точки, 
                    //которые не образуют левый поворот на  последних 2-х точках списка и текущей
                    while (downHull.Count > 1 && RightTurn(downHull[downHull.Count - 2], downHull.Last(), current))
                        downHull.RemoveAt(downHull.Count - 1);
                    //закинуть текущую точку в список
                    downHull.Add(current);
                }
            }
            //собрать из верхней и нижней оболочки одну последовательность
            Polygon polygon = new Polygon();
            foreach (var p in upHull)
                polygon.Add(p);
            polygon.Add(endPoint);
            //нижнюю оболочку надо записать вобратном порядке
            for (int i = downHull.Count - 1; i > 0; i--)
                polygon.Add(downHull[i]);
            return polygon;
        }
        /// <summary>
        /// образуют ли три точки правый поворот (по часовой стрелке)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        private bool RightTurn(Point p1, Point p2, Point p3)
        {
            Vector v1 = new Vector(p1, p2);
            Vector v2 = new Vector(p2, p3);
            return v1.RightTurn(v2);
        }

        /// <summary>
        /// образуют ли три точки левый поворот (против часовой стрелке)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        private bool LeftTurn(Point p1, Point p2, Point p3)
        {
            Vector v1 = new Vector(p1, p2);
            Vector v2 = new Vector(p2, p3);
            return v1.LeftTurn(v2);
        }

        /// <summary>
        /// вычисление площади произвольного n-угольник через треугольники и формулу Герона
        /// </summary>
        /// <returns>возвращает площадь с маленькой точностью</returns>
        public double SquareV()
        {
            int n = listPoint.Count;
            double s = 0;
            for (int i = 1; i < n - 1; i++)
            {
                Triangle tr = new Triangle(listPoint[0], listPoint[i], listPoint[i + 1]);
                s += tr.Square();
            }
            return s;
        }


        /// <summary>
        /// вычисление площади произвольного n-угольник через трапеции
        /// </summary>
        /// <returns>возвращает удвоенную беззнаковую площадь</returns>
        public int Square2()
        {

            Point[] tempArr = new Point[listPoint.Count];
            listPoint.CopyTo(tempArr);
            List<Point> temp = tempArr.ToList();
            temp.Add(listPoint[0]);
            int s = 0;
            for (int i = 1; i < temp.Count; i++)
            {
                s += SquareTrap(temp[i - 1], temp[i]);
            }
            return Math.Abs(s);
        }

        // вычисление площади трапеции на основании ребра n-угольника
        private int SquareTrap(Point t1, Point t2)
        {
            return (t1.y + t2.y) * (t2.x - t1.x);
        }

        /// <summary>
        /// вычисления площади n-угольника через векторное произведение
        /// </summary>
        /// <returns></returns>
        public int SquareFromVector()
        {
            int n = listPoint.Count;
            int s = 0;
            for (int i = 1; i < n - 1; i++)
            {
                Triangle tr = new Triangle(listPoint[0], listPoint[i], listPoint[i + 1]);
                s += tr.SquareVector();
            }
            return s;


        }
        public override string ToString()
        {

            return "число точек = " + listPoint.Count + "\n" + string.Join("\n", listPoint);

        }

    }

    /// <summary>
    /// Класс треугольник
    /// </summary>
    class Triangle
    {
        Point t1, t2, t3;

        public Triangle(Point t1, Point t2, Point t3)
        {
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
        }

        public double Square()
        {
            double a = Length(t1, t2);
            double b = Length(t2, t3);
            double c = Length(t3, t1);
            double p = this.Perimetr() / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
        /// <summary>
        /// вычисление площади треугольник через векторное/псевдовекторное произведение
        /// </summary>
        /// <returns>возвращает удвоенную знаковую площадь</returns>
        public int SquareVector()
        {
            Vector a = new Vector(t1, t2);
            Vector b = new Vector(t2, t3);
            return a.Psescalar(b);
        }
        public double Perimetr()
        {
            double a = Length(t1, t2);
            double b = Length(t2, t3);
            double c = Length(t3, t1);
            return a + b + c;
        }
        private double Length(Point t1, Point t2)
        {
            return Math.Sqrt((t1.x - t2.x) * (t1.x - t2.x) + (t1.y - t2.y) * (t1.y - t2.y));
        }
    }
}
