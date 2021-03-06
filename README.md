# Prueba técnica ML

Magneto quiere reclutar la mayor cantidad de mutantes para poder luchar
contra los X-Men.
Te ha contratado a ti para que desarrolles un proyecto que detecte si un
humano es mutante basándose en su secuencia de ADN.
Para eso te ha pedido crear un programa con un método o función con la siguiente firma (En
alguno de los siguiente lenguajes: Java / Golang / C-C++ / Javascript (node) / Python / Ruby):

boolean isMutant(String[] dna); // Ejemplo Java

# Algoritmo

Para la detención de mutantes se desarrolló un algoritmo para buscar las secuencias de letras en la matriz de ADN de una forma eficiente recorriendo cada letra nitrogenada de dicha matriz desde la primera fila, columna por columna hasta la última. Por cada letra se busca en las direcciones especificadas en el enunciado paralelamente para que el tiempo de respuesta del algoritmo sea más óptimo y cabe resaltar que cuando se encuentran las dos secuencias iguales de letras el algoritmo termina de procesar la matriz para evitar recorridos innecesarios.

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
https://ml-dna-api.azurewebsites.net/swagger/index.html

# Ejecución del proyecto localmente
Clonar el proyecto
```
git clone https://github.com/spuertab/Dna-Project.git
```

Hay tres alternativas para ejecutar el proyecto localmente:

Con visual studio 2019
- Abrir la solución que está en la carpeta /src/Dna-Project.Api en visual studio 2019 y ejecutarla con IIS Express.

Con docker:
-  Después de tener el proyecto clonado ir a la carpeta /src y luego ejecutar con PowerShell u otra consola de comandos las siguientes lineas (se debe tener instalado docker en la máquina donde se clonó el proyecto)
```
docker build -t dnaproject/dockerapi .
docker run -it --rm -p 8080:80 dnaproject/dockerapi
```
- Entrar a http://localhost:8080/swagger/index.html

Con dotnet CLI
- Después de tener el proyecto clonado ir a la carpeta /src y luego ejecutar con PowerShell u otra consola de comandos las siguientes lineas (se debe tener instalado .Net en la maquina donde se clonó el proyecto)
```
dotnet publish
cd  "Dna-Project.Api\bin\Debug\net5.0"    
dotnet Dna-Project.Api.dll --environment "Development"
```
- Entrar a https://localhost:5001/swagger/index.html

La base de datos se creó en Azure, por lo tanto no requiere ser instalada localmente ya que el proyecto tiene configurada las credenciales y las key para poder hacer la conexión al servicio en la nube.

# Swagger
La API tiene configurado el pluggin de swagger para su documentación:

![image](https://user-images.githubusercontent.com/43219701/121994396-cf1ddb80-cd6a-11eb-8c1c-dd715d8698c4.png)

Swagger te brinda también una interfaz para ejecutar los endpoints implementados que son el POST /mutant y el GET /stats 

# Pruebas de carga y estres

Se realizaron pruebas de carga y estres en Jmeter al endpoint POST /mutant de 1 a 300 usuarios conectados generando peticiones por un tiempo de 10 minutos y los resultados fueron los siguientes:

![image](https://user-images.githubusercontent.com/43219701/122073252-e76b1600-cdbd-11eb-9f66-3a14c077895f.png)

![image](https://user-images.githubusercontent.com/43219701/122073319-f356d800-cdbd-11eb-9854-adf95c93bd99.png)

Las peticiones verdes son de ADN que no son mutantes y los rojos de los que son mutantes

![image](https://user-images.githubusercontent.com/43219701/122073423-049fe480-cdbe-11eb-99b4-355a59607376.png)

Estas pruebas de carga se hicieron con los recursos más mínimos del app service y de la base de datos, si se aumenta el número de usuarios conectados tanto el app service como la base de datos se van a auto-escalar pero como mi cuenta de azure es free, tiene un límite y cuando sobrepase el límite la cuenta se va a bloquear para evitar sobre costos de infraestructura.
