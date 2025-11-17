namespace Listas;
public class Cola<T>
{
    private Nodo<T> frente;
    private Nodo<T> final;

    public Cola()
    {
        frente = null;
        final = null;
    }

    
    public bool EstaVacia()
    {
        return frente == null;
    }

   
    public void Encolar(T dato)
    {
        Nodo<T> nuevo = new Nodo<T>(dato);

        if (EstaVacia())
        {
            frente = nuevo;
            final = nuevo;
        }
        else
        {
            final.Siguiente = nuevo;
            final = nuevo;
        }
    }

    
    public T Desencolar()
    {
        if (EstaVacia())
            throw new InvalidOperationException("No se puede desencolar: la cola está vacía.");

        T dato = frente.Valor;
        frente = frente.Siguiente;

        if (frente == null)
            final = null; 

        return dato;
    }

    public T VerFrente()
    {
        if (EstaVacia())
            throw new InvalidOperationException("La cola está vacía.");

        return frente.Valor;
    }

    
    public void Mostrar()
    {
        if (EstaVacia())
        {
            Console.WriteLine("La cola está vacía.");
            return;
        }

        Nodo<T> actual = frente;
        while (actual != null)
        {
            Console.WriteLine(actual.Valor);
            actual = actual.Siguiente;
        }
    }
}
