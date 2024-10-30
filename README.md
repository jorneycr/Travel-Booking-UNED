# Sistema de Reservas de Viajes

Este es un sistema de reservas de viajes en autobús, desarrollado con **ASP.NET Core MVC** y **Entity Framework Core**. La aplicación permite a los usuarios registrarse, iniciar sesión y gestionar sus reservas de viajes. También incluye un sistema de autenticación basado en **ASP.NET Identity**.

## Funcionalidades

1. **Registro de Usuarios**: Los usuarios pueden registrarse en la plataforma proporcionando su nombre, apellido, email y contraseña.
2. **Inicio de Sesión**: Los usuarios registrados pueden iniciar sesión con su correo electrónico y contraseña.
3. **Reservas de Viajes**: Una vez autenticado, el usuario puede buscar rutas de autobús y realizar reservas.
4. **Historial de Reservas**: Los usuarios pueden ver un historial de todas sus reservas pasadas.
5. **Sistema de Roles**: Diferentes roles de usuario (como administrador y cliente) permiten gestionar la aplicación según permisos específicos.

## Tecnologías Utilizadas

- **ASP.NET Core MVC**: Framework principal de la aplicación.
- **Entity Framework Core**: ORM utilizado para interactuar con la base de datos SQL Server.
- **ASP.NET Identity**: Para manejar la autenticación y el registro de usuarios.
- **SQL Server**: Base de datos relacional para almacenar usuarios, rutas y reservas.

## Instalación

1. Clona el repositorio:

```js
git clone https://github.com/jorneycr/Travel-Booking-UNED
```

2. Restaura las dependencias

```js
dotnet restore
```

3. Ejecución del Proyecto

```js
dotnet run
```

## Migraciones de Base de Datos usando la CLI de .NET

1. Crea una nueva migración

```js
dotnet ef migrations add NombreDeLaMigracion
```

2. Aplica las migraciones

```js
dotnet ef database update
```
