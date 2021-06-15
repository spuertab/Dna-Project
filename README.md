# ALGORITMO

Para la detención de mutantes se desarrolló un algoritmo para buscar las secuencias de letras en la matriz de ADN de una forma eficiente recorriendo cada letra nitrogenada de dicha matriz desde la primera fila columna por columna hasta la última. Por cada letra se busca de forma paralela en ejecución en las direcciones especificadas en el enunciado de para que el tiempo de respuesta del algoritmo sea más óptimo y cabe resaltar que cuando se encuentren dos o más secuencias iguales de letras el algoritmo termina de procesar la matriz para evitar recorridos innecesarios.

Direcciones implementadas:

- Buscar hacia la derecha
- Buscar hacia abajo
- Buscar diagonalmente derecha
- Buscar diagonalmente izquierda
- Es importante destacar que se implementó un patrón de estrategia para las direcciones de búsqueda y así hacer la aplicación extensible, por ejemplo si en un futuro se requiere hacer una búsqueda de secuencias de letras en "L" la implementación sería muy sencilla gracias a este patrón de comportamiento, esto también se hizo con el fin de respetar los principios SOLID.
