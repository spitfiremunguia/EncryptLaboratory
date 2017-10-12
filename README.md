# Cifrado de Datos
Programa que realiza el cifrado y descifrado de un archivo seleccionado mediante una implementación básica del algoritmo DES.
## Instrucciones de uso
A continuación se muestra la forma en que deben ser ingresados los parámetros desde la línea de comandos para el cifrado y descifrado de un archivo.
### Cifrado de un archivo 
Para cifrar un archivo se sigue la siguiente sintaxis
```
Cipher.exe -c -f[Path]
```
Donde ```-c``` indica que se desea cifrar, ```-f``` indica que está por indicarse el archivo y ```[Path]``` es la ruta absoluta del archivo que se desea cifrar.
###### Nota
El archivo cifrado, se escribirá en el mismo directorio del archivo original.
### Descifrado de un archivo 
Para descifrar un archivo se sigue la siguiente sintaxis
```
Cipher.exe -d -f[Path]
```
Donde ```-c``` indica que se desea descifrar, ```-f``` indica que está por indicarse el archivo y ```[Path]``` es la ruta absoluta del archivo que se desea descifrar.
###### Nota
El archivo descifrado, se escribirá en el mismo directorio del archivo cifrado.
##### Nota
Se recomienda ejecutar el programa con permisos de administrador.
