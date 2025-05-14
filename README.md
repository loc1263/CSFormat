# CSFormat - Procesador de Archivos de Texto

CSFormat es una aplicación de escritorio desarrollada en C# que permite formatear archivos de texto según una estructura definida en un archivo de formato CSV.

## Estructura del Proyecto

```
CSFormat/
├── bin/                     # Archivos binarios de compilación
├── obj/                     # Archivos temporales de compilación
├── publish/                 # Versiones publicadas de la aplicación
├── CSFormat.csproj          # Archivo de proyecto .NET
├── CSFormat.sln             # Solución de Visual Studio
├── CSFormat.cs              # Clase principal del formulario
├── CSFormat.Designer.cs     # Código generado por el diseñador
├── FileProcessor.cs         # Lógica de procesamiento de archivos
├── formato.csv              # Archivo de ejemplo de formato
├── ejemplo_formatted.txt    # Ejemplo de salida formateada
└── README.md                # Documentación del proyecto
```

## Requisitos del Sistema

- .NET 8.0 SDK o superior
- Windows 10/11 (aplicación Windows Forms)
- Conexión a Internet (para restaurar paquetes NuGet)

## Dependencias

El proyecto utiliza las siguientes dependencias de NuGet:

- `Microsoft.NET.Sdk` - SDK base de .NET
- `Microsoft.WindowsDesktop.App` - Para aplicaciones de escritorio Windows

Estas dependencias se restaurarán automáticamente al ejecutar `dotnet restore`.

## Configuración del Entorno

1. Instalar [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) o superior
2. Clonar el repositorio:
   ```bash
   git clone [url-del-repositorio]
   cd CSFormat
   ```
3. Restaurar paquetes NuGet:
   ```bash
   dotnet restore
   ```

## Compilación

Para compilar el proyecto, ejecuta:

```bash
dotnet build
```

## Ejecución

Para ejecutar la aplicación en modo desarrollo:

```bash
dotnet run
```

Para publicar una versión autónoma:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

El ejecutable se generará en `bin/Release/net8.0-windows/win-x64/publish/`

## Uso

1. **Archivo de formato** (`formato.csv`):
   ```
   Fecha,8
   Codigo,6
   Telefono,10
   ```
   - Formato: `NombreCampo,Longitud` (un campo por línea)

2. **Archivo de entrada**: Archivo de texto con datos de ancho fijo
3. **Archivo de salida**: Archivo CSV con los datos formateados

## Ejemplo

1. **Archivo de entrada** (`datos.txt`):
   ```
   20250511123456ABCD1234
   20250512222222BBBB5678
   ```

2. **Archivo de salida** (`datos_formatted.txt`):
   ```
   Fecha,Codigo,Telefono
   20250511,123456,ABCD1234
   20250512,222222,BBBB5678
   ```

## Rendimiento

- Uso de `StreamReader` y `StreamWriter` con búfer personalizado
- Procesamiento por líneas usando `yield return`

## Solución de Problemas

Si encuentras problemas con los paquetes NuGet:
1. Limpia la caché de NuGet:
   ```bash
   dotnet nuget locals all --clear
   ```
2. Restaura los paquetes:
   ```bash
   dotnet restore
   ```
