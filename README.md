# API REST para gestión de cooperativa

## Se usó

- C# / .NET 10 (ASP.NET Core Minimal APIs)
- Entity Framework Core
- SQL Server
- Swagger (documentación y pruebas)

## Endpoints

- GET `/socios` → Lista todos los socios
- GET `/socios/{id}` → Obtiene un socio por su ID
- POST `/socios` → Crea un socio
- PUT `/socios/{id}` → Actualiza nombre y municipio de un socio
- DELETE `/socios/{id}` → Elimina un socio

## Cómo ejecutarlo

1. Tener instalado el SDK de .NET 10 y SQL Server.
2. Crear la base de datos con el script `database.sql`.
3. Configurar la cadena de conexión con user-secrets:

   ```bash
   dotnet user-secrets set "ConnectionStrings:Cooperativa" "Server=localhost;Database=Cooperativa;User Id=sa;Password=TU_PASSWORD;TrustServerCertificate=True;"
   ```

4. Ejecutar en una terminal dentro de la carpeta del proyecto:

   ```bash
   dotnet run
   ```

5. Abrir `http://localhost:5184/swagger` para probar los endpoints.

## Seguridad

Las credenciales de la base de datos no se guardan en el repositorio: se manejan con user-secrets de .NET.
