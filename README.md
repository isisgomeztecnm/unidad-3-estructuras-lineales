# unidad-3-estructuras-lineales

Nombre: isis mayte gomez chavez 

Número de control:24410211

Carrera: ingenieria en sistemas 

Semestre: tercer semestre

Materia:estructura de datos 

Docente: Myrna Villegas Gaytán

## proyecto 1

//Sistema de gestión de pacientes de una clínica

Nivel:intermedio

Desarrollar un sistema para administrar la atención de pacientes en una clínica, el sistema deberá registrar pacientes y asignarles un nivel de prioridad 

_Emergencia 

_Urgente 

_Consulta general 

_Cita programada

Los pacientes deberán ser atendidos respetando las reglas de prioridades 

//Funciones:

_Registrar pacientes 

_Eliminar paciente 

_Buscar paciente por expediente 

_Mostrar pacientes en espera 

_Atender siguiente paciente 

_Asignar prioridad

_Mostrar historial de pacientes atendidos 

_Consultar cantidad de pacientes en espera

Estructura:

Lista enlazada>pacientes registrados 

Cola>pacientes en espera 

Cola prioridad> Emergencia 

El primero en entrar es el primero en salir 


## proyecto 2

//restaurante 

Nivel: intermedio 

Estructura: cola y lista enlazada

Este proyecto está directamente relacionado con una de las prácticas propuestas por el temario. Simular una lista de espera para la asignación de meses en un restaurante 

//Funciones:

-Registrar cliente 

-registrar número de personas 

-Agregar clientes a lista de espera 

-Asignar mesa

-Cancelar reservación 

-Buscar cliente

-Eliminar cliente 

-Mostrar lista de espera 

-Mostrar meses disponibles 

-Mostrar mesas ocupadas

## proyecto 3

//Simulador de llamadas telefónicas

Nivel: intermedio 

Estructura pila

El proyecto simula llamadas a funciones utilizando una pila 

//Funciones:

-Registrar llamada

-Atender llamada 

-Finalizar llamada 

-Mostrar llamadas activas

-Recuperar última llamada 

-Buscar llamada

-Eliminar llamada

Ejemplo:

PILA DE LLAMADAS 

1.Main()

2.Menuprincipal()

Registrarcliente()

Buscarcliente()

al regresar: 

Regresando de:

Buscarcliente()

Ahora:

Registrarcliente()

Esto permite relacionar la pila con el funcionamiento de la memoria de llamadas de métodos.


## proyecto 4

 //Calculadora de expresiones matemáticas 
 
Nivel: avanzado

Estructura: pilas 

Este sería uno de los proyectos más importantes de la unidad. El programa deberá convertir expresiones:

Infija

Postfija

El temario propone específicamente desarrollar una clase calculadora que convierta expresiones infijas a postfijas y posteriormente las evalúe utilizando pilas 

//Funciones:

-Validar expresión 

-Convertir infija + postfija

-Evaluar expresión postfija

-Manejar paréntesis 

-Manejar operadores 

-Detectar errores matemáticos 


## proyecto 5

//Sistema de historial de navegación web

nivel: intermedio avanzado 

Estructura: pila

Crear un navegador web simulado

El sistema deberá almacenar las páginas visitadas y permitir:

Página actual 

Historial

//Funciones:

-Visitar página

-Regresar

-Avanzar

-Mostrar historial 

-Eliminar historial 

-Buscar una página 

-Mostrar última página visitada

-extensión 


## proyecto 6



## proyecto 7

7.sistema de reproducción de música 

Nivel: intermedio-avanzado

Estructura: lista doblemente enlazada y lista circular.

Crear un reproductor musical simulado.

//Funciones:

Agregar canción 

Eliminar canción 

Buscar canción

Reproducir

Siguiente 

Anterior

Pausar 

Mostrar playlist 

Crear playlist

Reproducción circular

lista doble

Cancion A»« cancion B»«canción C»«cancion D


## proyecto 8

8. Sistema de atención de un banco

Nivel: Avanzado

Estructuras: Cola, cola circular y cola de prioridad.

Desarrollar un simulador de atención bancaria.

Tipos de clientes

* Cliente normal

* Cliente preferente

* Adulto mayor

* Persona con prioridad

Funciones

* Generar turno.

* Atender cliente.

* Cancelar turno.

* Mostrar clientes esperando.

* Mostrar clientes atendidos.

* Asignar prioridad.

* Mostrar tiempo estimado.

* Mostrar número de clientes en espera.

Ejemplo

TURNOS

* A001 → Cliente normal

* A002 → Cliente normal

* P001 → Cliente preferente

* A003 → Cliente normal

La cola de prioridad deberá permitir que el sistema atienda primero a los clientes que correspondan.


## proyecto 9


9. Sistema de control de procesos de un sistema operativo

Nivel: Avanzado

Estructuras: Cola, cola circular y lista enlazada.

Este proyecto permite relacionar **Estructuras de Datos con Sistemas Operativos**, algo que el propio programa de la asignatura recomienda al señalar que los sistemas operativos requieren estructuras de datos para manejar diferentes procesos.
**Cada proceso tendrá:**

PID
Nombre
Prioridad
Estado
Tiempo de ejecución

**Estados**
NUEVO
LISTO
EJECUTANDO
BLOQUEADO
TERMINADO
**Funciones**


* Crear proceso.


* Eliminar proceso.


* Ejecutar proceso.


* Bloquear proceso.


* Desbloquear proceso.


* Mostrar procesos.


* Buscar proceso.


* Administrar cola de procesos.
**Ejemplo**
COLA DE PROCESOS



P01 → P02 → P03 → P04
El proyecto puede posteriormente ampliarse con algoritmos de planificación.


## proyecto 10

10\. Sistema de gestión de pedidos de una tienda en línea

Nivel: Avanzado / Proyecto integrador

Este sería mi proyecto recomendado para cerrar la Unidad 3.

El estudiante desarrollará un sistema que administre pedidos utilizando diferentes estructuras lineales.

// Estructuras

Lista enlazada

\- Productos.

Cola

\- Pedidos pendientes.

Cola de prioridad

\- Pedidos urgentes.

Pila

\- Historial de operaciones.

//Producto

* ID

* Nombre

* Precio

* Existencias

* Categoría

//Pedido

* Número

* Cliente

* Fecha

* Productos

* Total

* Prioridad

* Estado


//Funciones principales

* Registrar producto.

* Buscar producto.

* Eliminar producto.

* Registrar pedido.

* Cancelar pedido.

* Procesar pedido.


