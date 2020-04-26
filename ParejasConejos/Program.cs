using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParejasConejos
{
    class Program
    {
        private static Stopwatch Stop1;
        static void Main(string[] args)
        {

            Task<int>[] tareas = new Task<int>[100];
            Stop1 = Stopwatch.StartNew();
            for (int i = 0; i < tareas.Length; i++)
            {
                tareas[i] = Task.Factory.StartNew<int>((para) =>
                   {
                       int p = (int)para;
                       int a = Fibonacci(p);
                       Console.WriteLine("Cant conejos: {0}",a);
                       return a;
                   }, i);

            }
            Stop1.Stop();
            Task<double> prom = Task.Factory.StartNew<double>(() =>
            {
                int sum = 0;
                for (int i = 0; i < tareas.Length; i++)
                {
                    sum += tareas[i].Result;

                }
                return sum / tareas.Length;
            });

            Task.WaitAll();
            int indice = Task.WaitAny(tareas);
            Task<int> primera = tareas[indice];
            Console.WriteLine("Cantidad de conejos que termino primero: {0}", primera.Result);
            Console.WriteLine("Tiempo que demoro: {0}", Stop1.ElapsedMilliseconds);
            Console.WriteLine("Promedio de conejos: {0}", prom.Result);
            Console.ReadKey();
        }        
        
        public static int Fibonacci(int n)
        {
            int a = 0;
            int b = 1;
            // In N steps compute Fibonacci sequence iteratively.
            for (int i = 0; i < n; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }
            return a;
        }
    }
}
