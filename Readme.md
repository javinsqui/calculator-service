# Calculator Service

Proyecto de ejemplo de una sencilla calculadora realizado con c#, WebAPI y un proyecto de consola.

## Comenzando 🚀

La solución consta de tres proyectos 

* CalculatorService.Client

  * Proyecto de consola con un menú simple que permite realizar las operaciones de Suma, Resta, ... 

* CalculatorService.Server

  * Proyecto Web/API que realiza las operaciones en el Servidor.

* CalculatorService.Shared

  * Proyecto de Biblioteca de clases que incluye algunas clases compartidas por los dos proyectos anteriores.


### Pre-requisitos 📋

El proyecto ha sido realizado utilizando Visual Studio 2019


### Instalación 🔧

Una vez descargado el proyecto se puede abrir con Visual Studio haciendo doble clic en el archivo de solucion *CalculatorService.sln*


## Probar el proyecto ⚙️

Desde Visual Studio basta con pulsar F5 (en modo depuración) o Ctrl+F5 (sin depuración). Esto hace que se ejecuten simultaneamente los dos proyectos principales "Client" y "Server".


## Construido con 🛠�?

El proyecto ha sido realizado enteramente con C#. Tan solo se utiliza una librería de terceros para las llamadas a Web/API REST

* [RestSharp](https://restsharp.dev/) 

