# CSFormat - Procesador de Archivos de Texto

CSFormat es una aplicación de escritorio desarrollada en C# que permite formatear archivos de texto según una estructura definida en un archivo de formato CSV. La aplicación está diseñada para ser eficiente en el manejo de archivos grandes, utilizando técnicas modernas de programación para optimizar el uso de memoria.

## Características

- Interfaz gráfica intuitiva y fácil de usar
- Procesamiento eficiente de archivos de gran tamaño
- Soporte para archivos de formato CSV personalizables
- Validación de datos de entrada
- Retroalimentación en tiempo real del progreso
- Generación de archivos de salida formateados

## Estructura del Proyecto

```
CSFormat/
├── CSFormat.csproj      # Archivo de proyecto .NET
├── CSFormat.sln         # Solución de Visual Studio
├── CSFormat.cs          # Clase principal del formulario
├── CSFormat.Designer.cs # Código generado por el diseñador
├── FileProcessor.cs     # Lógica de procesamiento de archivos
└── README.md            # Este archivo
```

## Requisitos del Sistema

- .NET 8.0 SDK o superior
- Windows 10/11 (debido a que es una aplicación Windows Forms)
- 100 MB de espacio en disco (dependiendo del tamaño de los archivos a procesar)
- 2 GB de RAM (recomendado para archivos grandes)

## Cómo compilar y ejecutar

### Requisitos previos

Asegúrate de tener instalado el [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) o superior.

### Compilación

1. Clona el repositorio o descarga el código fuente
2. Abre una terminal en el directorio del proyecto
3. Ejecuta el siguiente comando para restaurar las dependencias y compilar:

```bash
dotnet build
```

### Ejecución

Para ejecutar la aplicación, usa el siguiente comando:

```bash
dotnet run
```

O ejecuta directamente el archivo compilado desde:
```
bin\Debug\net8.0-windows\CSFormat.exe
```

## Uso

1. **Seleccionar archivo de formato**: Archivo CSV que define la estructura de los campos
   - Formato: `NombreCampo,Longitud` (un campo por línea)
   - Ejemplo:
     ```
     Fecha,8
     Tid,6
     Codigo_Auth,10
     ```

2. **Seleccionar archivo de entrada**: Archivo de texto plano con los datos a formatear
   - Los datos deben estar en formato de ancho fijo según el formato especificado

3. **Especificar separador**: Carácter que se usará para separar los campos en el archivo de salida

4. **Procesar**: Haz clic en "Procesar" para generar el archivo de salida

## Ejemplo

1. **Archivo de formato** (`formato.csv`):
   ```
   Fecha,8
   Tid,6
   Codigo_Auth,10
   ```

2. **Archivo de entrada** (`datos.txt`):
   ```
   20250511123456ABCD1234
   20250512222222BBBB5678
   ```

3. **Archivo de salida** (`datos_formatted.txt`):
   ```
   Fecha,Tid,Codigo_Auth
   20250511,123456,ABCD1234
   20250512,222222,BBBB5678
   ```

## Rendimiento

La aplicación está optimizada para manejar archivos grandes mediante:
- Procesamiento por líneas usando `yield return`
- Uso de `StreamReader` y `StreamWriter` con búfer personalizado
- Asignación eficiente de memoria
- Procesamiento en segundo plano para mantener la interfaz de usuario receptiva

## Licencia

Este proyecto está bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más información.

## Contribuciones

Las contribuciones son bienvenidas. Por favor, lee las [pautas de contribución](CONTRIBUTING.md) antes de enviar un pull request.

## Contacto

Si tienes preguntas o sugerencias, por favor abre un issue en el repositorio.
