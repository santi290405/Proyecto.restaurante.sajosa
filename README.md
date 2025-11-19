🏨 Proyecto: Gestión de Restaurante

📌 Descripción

Este proyecto es una aplicación de consola en C# para la gestión integral de un restaurante. Permite manejar menús, platos, clientes y pedidos utilizando listas enlazadas y colas personalizadas, evitando el uso de List<T> de C#.

El objetivo principal es:

Practicar estructuras de datos dinámicas.

Controlar la memoria y la organización de los datos.

Implementar funcionalidades reales de un restaurante en un sistema de consola.

⚙ Funcionalidades
Gestión de Platos

Agregar, eliminar y buscar platos en el menú.

Cada plato puede tener nombre, descripción y precio.

Gestión de Menús

Crear menús vinculando varios platos.

Modificar menús existentes y eliminar menús obsoletos.

Gestión de Clientes

Registrar clientes con su información básica (cédula, nombre, etc.).

Consultar clientes y sus pedidos.

Gestión de Pedidos

Crear pedidos asociados a un cliente y a los platos seleccionados.

Despachar pedidos siguiendo el orden de llegada (cola de pedidos).

Visualizar pedidos pendientes y despachados.

Editar pedidos antes de ser despachados.

Calcular ganancias del día sumando los pedidos despachados.

🗂 Estructura del Proyecto
/ProyectoRestaurante
│
├─ /Listas
│   └─ ListaEnlazada<T>.cs      # Implementación de listas enlazadas
│   └─ Nodo<T>.cs               # Nodo de las listas enlazadas
│   └─ Cola<T>.cs               # Implementación de colas personalizadas
│
├─ /Servicios
│   └─ GestorPlatos.cs          # Funciones de gestión de platos
│   └─ GestorMenus.cs           # Funciones de gestión de menús
│   └─ GestorClientes.cs        # Funciones de gestión de clientes
│   └─ GestorDePedidos.cs       # Funciones de gestión de pedidos (cola y lista enlazada)
│
└─ Program.cs                   # Punto de entrada y menú principal

🔧 Estructuras de Datos
Lista Enlazada
public class Nodo<T>
{
    public T Valor;
    public Nodo<T> Siguiente;
    public Nodo(T valor) { Valor = valor; Siguiente = null; }
}

public class ListaEnlazada<T>
{
    public Nodo<T> Cabeza;

    public void Agregar(T valor) { ... }
    public void Imprimir() { ... }
}

Cola Personalizada (para pedidos)
public class Cola<T>
{
    private Nodo<T> frente;
    private Nodo<T> final;

    public void Encolar(T valor) { ... }
    public T Desencolar() { ... }
    public bool EstaVacia() { ... }
}


Beneficios:

Control total sobre la memoria y la estructura de datos.

Permite operaciones dinámicas sin depender de librerías externas.

Base sólida para estructuras más complejas en el futuro.

🖼 Visualización del Menú de Consola
===================================
      GESTIÓN DE RESTAURANTE
===================================
1. Gestión de Platos
2. Gestión de Menús
3. Gestión de Clientes
4. Gestión de Pedidos
5. Salir
===================================
Seleccione una opción: _

Ejemplo: Gestión de Pedidos
=== PEDIDOS ===
1. Crear Pedido
2. Despachar Pedido
3. Ver Siguiente Pedido
4. Mostrar Pedidos Pendientes
5. Mostrar Pedidos Despachados
6. Editar Pedido
7. Volver
Seleccione una opción: _


Beneficios:

Control total sobre la memoria y la estructura de datos.

Permite operaciones dinámicas sin depender de librerías externas.

Base para estructuras más complejas en el futuro.


Se registran los Restaurantes de la siguiente manera: <img width="1280" height="431" alt="image" src="https://github.com/user-attachments/assets/3fd64f4c-4ccc-4ee8-90a4-9e8caac8b5d8" />
Se registran los Clientes de la siguiente manera: <img width="529" height="247" alt="image" src="https://github.com/user-attachments/assets/b773b9c5-c1b2-458a-9642-1c600d48e5c9" />
Se registran los Platos de la siguiente manera: <img width="736" height="195" alt="image" src="https://github.com/user-attachments/assets/97c8157e-f3bc-4929-8029-10fee42c3dcd" />





