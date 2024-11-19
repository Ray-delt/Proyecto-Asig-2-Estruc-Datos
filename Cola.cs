using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_teorica
{
    internal class Cola<T>
    {
        private LinkedList<T> elementos;

        public Cola()
        {
            elementos = new LinkedList<T>();

        }
        public void encola(T elemento)
        {
            elementos.AddLast(elemento);
        }
        public T atendida()
        {
            if (EsVacia())
            {
                throw new InvalidOperationException("La cola está vacía.");
            }

            T valor = elementos.First.Value;
            elementos.RemoveFirst();
            return valor;
        }
        public void eliminar()
        {
            elementos.RemoveFirst();
        }

        public void imprimir()
        {
            if (EsVacia())
            {
                Console.WriteLine("La cola está vacía.");
                return;
            }

            Console.WriteLine("Elementos en la cola:");
            foreach (var elemento in elementos)
            {
                Console.WriteLine(elemento);
            }
        }
        public T Peek()
        {
            if (EsVacia())
            {
                throw new InvalidOperationException("La cola está vacía.");
            }

            return elementos.First.Value;
        }
        public bool EsVacia()
        {
            return elementos.Count == 0;
        }
        public int Count()
        {
            return elementos.Count;
        }
        


    }

}

