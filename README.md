# TravelBooking

TravelBooking es una aplicación web de reservas de viajes que permite a los usuarios buscar y reservar vuelos de manera eficiente y segura, utilizando .NET MVC.

## Características

- **Búsqueda de Rutas**: Los usuarios pueden buscar rutas de buses según su lugar de salida y destino.
- **Reservas**: Permite a los usuarios seleccionar asientos y realizar reservas de forma sencilla.
- **Gestión de Pagos**: Integración con un sistema de pago seguro para procesar las transacciones.
- **Confirmación de Reserva**: Los usuarios reciben una confirmación de sus reservas y detalles de pago.

## Tecnologías Utilizadas

- **.NET Framework**: Para el desarrollo de la aplicación.
- **MVC**: Arquitectura Model-View-Controller para organizar el código.
- **JavaScript**: Para validaciones y mejoras en la experiencia de usuario.
- **Ajax**: Para la comunicación asincrónica con el servidor.
- **SQL Server**: Para el almacenamiento de datos.

## Estructura del Proyecto

- **Modelos**:
  - **Rutas de Buses**: Información sobre buses, horarios, precios y destinos.
  - **Usuarios**: Datos sobre usuarios registrados, historial de compras y datos personales.
  - **Reservas**: Detalles de las reservas, incluyendo usuario, ruta, asiento seleccionado y estado del pago.

- **Vistas**:
  - **Página Principal**
  - **Resultados de Búsqueda**
  - **Detalles de Ruta**
  - **Formulario de Reserva**
  - **Confirmación de Reserva**

- **Controladores**:
  - **Controlador de Búsqueda**
  - **Controlador de Reserva**
  - **Controlador de Pago**
  - **Controlador de Usuario**

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

