# Prueba técnica ML

Magneto quiere reclutar la mayor cantidad de mutantes para poder luchar
contra los X-Men.
Te ha contratado a ti para que desarrolles un proyecto que detecte si un
humano es mutante basándose en su secuencia de ADN.
Para eso te ha pedido crear un programa con un método o función con la siguiente firma (En
alguno de los siguiente lenguajes: Java / Golang / C-C++ / Javascript (node) / Python / Ruby):

boolean isMutant(String[] dna); // Ejemplo Java

# Algoritmo

Para la detención de mutantes se desarrolló un algoritmo para buscar las secuencias de letras en la matriz de ADN de una forma eficiente recorriendo cada letra nitrogenada de dicha matriz desde la primera fila columna por columna hasta la última. Por cada letra se busca de forma paralela en ejecución en las direcciones especificadas en el enunciado de para que el tiempo de respuesta del algoritmo sea más óptimo y cabe resaltar que cuando se encuentren dos o más secuencias iguales de letras el algoritmo termina de procesar la matriz para evitar recorridos innecesarios.

Direcciones implementadas:

- Buscar hacia la derecha.

![image](https://user-images.githubusercontent.com/43219701/121989936-8eba5f80-cd62-11eb-9778-012b08653373.png)

- Buscar hacia abajo.

![image](https://user-images.githubusercontent.com/43219701/121989790-5286ff00-cd62-11eb-8894-6d9cf9f99e3b.png)

- Buscar diagonalmente derecha.

![image](https://user-images.githubusercontent.com/43219701/121990254-13a57900-cd63-11eb-8804-21158b6bbd84.png)

- Buscar diagonalmente izquierda.

![image](https://user-images.githubusercontent.com/43219701/121990085-cfb27400-cd62-11eb-9d13-8b37f36e3e13.png)


Es importante destacar que se implementó un patrón de estrategia para las direcciones de búsqueda y así hacer la aplicación extensible, por ejemplo si en un futuro se requiere hacer una búsqueda de secuencias de letras en "L" la implementación sería muy sencilla gracias a este patrón de comportamiento, esto también se hizo con el fin de respetar los principios SOLID.

# Arquitectura de componentes
![image](https://user-images.githubusercontent.com/43219701/121992019-659bce00-cd66-11eb-8b22-0eb9138c0198.png)

- Todos los componentes presentados anteriormente fueron creados en Azure cloud, los tengo en una cuenta gratuita en la cual puedo crear recursos con las capacidades más bajas
- Este proyecto contiene una API creada en .Net 5.0 que es un lenguaje multiplataforma y por tal motivo se dockerizó para posteriormente ser desplegada en un app services Linux que tiene mejor rendimiento que uno en windows.
- La base de datos utilizada es una Cosmos DB de tipo SQL.
- El app service tiene autoscale horizontal, cuando el número de peticiones por segundo crece el número de instancias también.
- La base de datos tiene autoscale vertical, cuando el número de registros guardados en la base de datos crece, la potencia de la base de datos también.

URL API en Azure:
[link](https://dna-ml.azurewebsites.net/swagger/index.html)

# Ejecución del proyecto localmente
