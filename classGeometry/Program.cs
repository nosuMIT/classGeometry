using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;
using Geometry;

namespace classGeometry
{
    class Program
    {
        static void Main(string[] args)
        {
            //string input = "6 3 12 1 14 6 11 8 5 8";
            //string input = "5 8 8 8 11 8 14 6 6 6 10 4 6 3 12 1";
            string input = "8 4 5 9 6 8 6 5 1 1 2 4 4 4 5 9";
            Polygon polygon = new Polygon(input);
            Console.WriteLine(polygon);
            Polygon сonvexPolygon = polygon.СonvexHull();
            Console.WriteLine("\nВыпуклая оболочка");
            Console.WriteLine(сonvexPolygon);
            Console.WriteLine("Площадь через треугольник\t\t " + сonvexPolygon.SquareV());
            Console.WriteLine("Площадь через трапеции \t\t\t" + сonvexPolygon.Square2());
            Console.WriteLine("Площадь через треугольники и вектора \t" + сonvexPolygon.SquareFromVector());
        }
    }
}




