public class Pila<T>
{
    private Nodo<T> cima;

    public Pila()
    {
        cima = null;
    }

    public bool EstaVacia()
    {
        return cima == null;
    }

    public void Apilar(T valor)
    {
        Nodo<T> nuevo = new Nodo<T>(valor);
        nuevo.Siguiente = cima;
        cima = nuevo;
    }

    public T Desapilar()
    {
        if (EstaVacia())
            throw new InvalidOperationException("La pila está vacía.");

        T valor = cima.Valor;
        cima = cima.Siguiente;
        return valor;
    }

    public T Cima()
    {
        if (EstaVacia())
            throw new InvalidOperationException("La pila está vacía.");
        return cima.Valor;
    }

    public void MostrarPila(Action<T> accion)
    {
        Nodo<T> actual = cima;
        while (actual != null)
        {
            accion(actual.Valor);
            actual = actual.Siguiente;
        }
    }
}
