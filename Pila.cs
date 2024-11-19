using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_teorica
{
    internal class Pila
    {
        private int tope;
        protected const int max = 25;
        object[] elementos;
        int i = 0;

        public Pila() 
        {
            tope = -1;
            elementos = new object[max];

        }

        private bool vacia() { return tope == -1; }
        private bool llena()
        {
            return tope == max - 1;
        }
        public int gettope() 
        {
            return tope;
        }
        public int cantidad()
        {
            return i;
        }
        //{c1,c2,c3,c4}
        public void push(object dato) 
        {
            if (llena())
            {
                Console.WriteLine("la pila esta llena");
                Console.ReadKey();
            }
            else
            {
                i++;
                elementos[++tope] = dato;
            }
        }

        public object pop()
        {
            if (vacia())
            {
                return null;
            }
            else
            {
                //i--;
                return elementos[tope--];
            }
        }

        public object poptope()
        {
            if (vacia())
            {
                return null;
            }
            else
            {
                return elementos[tope];
            }

        }
        public void imprimir()
        {
            if (vacia())
            {

            }
            else 
            {
                object d;
                Pila pa = new Pila();
                while (!vacia())
                {
                    d = pop();
                    Console.WriteLine(d + " ");
                    pa.push(d);

                }
                while (!pa.vacia())
                {
                    push(pa.pop());

                }
                Console.ReadKey();
            }
        }
    }
}
