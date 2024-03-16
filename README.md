# GameCharacterpedia

---

## Descripción

GameCharacterpedia es una aplicación web diseñada para los entusiastas de los videojuegos que desean mantener un registro de sus personajes favoritos. Con GameCharacterpedia, los usuarios pueden explorar una amplia base de datos de personajes de videojuegos y guardar sus favoritos para acceder fácilmente a ellos en cualquier momento.

La aplicación ofrece una experiencia personalizada al permitir que los usuarios inicien sesión y mantengan una lista de personajes favoritos asociada a su cuenta. Esto facilita el acceso rápido a los personajes preferidos y proporciona una forma conveniente de administrar su colección.

---

## Funcionalidades clave

1. **Exploración de personajes:** Los usuarios pueden buscar y explorar una amplia variedad de personajes de videojuegos utilizando filtro por nombre del personaje.

2. **Guardado de favoritos:** Los usuarios registrados pueden guardar sus personajes favoritos en la sección "Favorites" para acceder fácilmente a ellos más tarde.

3. **Inicio de sesión seguro:** La aplicación proporciona un sistema de inicio de sesión seguro para que los usuarios puedan acceder a sus cuentas de manera protegida.

4. **Gestión de la cuenta:** Los usuarios pueden administrar su perfil, cambiar su contraseña y gestionar la lista de personajes favoritos asociados a su cuenta.

---

## Tecnologías utilizadas

- **Frontend:** HTML, CSS Isolation, Blazor Webassembly
- **Backend:** C# con .NET Core
- **Base de datos:** SQL Server para almacenar información de usuario y personajes favoritos
- **Autenticación:** JSON Web Tokens (JWT) para autenticación de usuarios
- **Patrones de diseño:** Unit of Work, Repository, Mediator
- **Validación de datos:** Fluent Validation
- **Mapeo de objetos:** AutoMapper
- **Registro de eventos y trazabilidad:** Serilog
- **Pruebas unitarias:** XUnit.net, Entity Framework In-Memory
---

## Despliegue
1. Se utilizo Azure para el despliegue de la web api, el cliente y la creacion de la base de datos

---

## Instrucciones de instalación

1. Clona este repositorio en tu máquina local.
2. Hacerle rebuild a la solución del proyecto (si no funciona el frontend hacerle rebuild a el proyecto cliente).
3. Configura tu base de datos usando entity framework usando add-migration y despues update-database.
4. En el proyecto se esta utilizando un storage de firebase para las imagenes, en caso de querer cambiarlo ir al proyecto infraestructure -> FirebaseService y modificarlo (si no se cambia funciona igual).
5. Ejecutar tanto el proyecto API y Client para probar la aplicación completa.

---

## Créditos

Este proyecto fue desarrollado por Cristian Delgado y Luca Figueroa.

---

¡Disfruta utilizando GameCharacterpedia para mantener un registro de tus personajes favoritos de videojuegos! Si tienes alguna pregunta o sugerencia, no dudes en ponerte en contacto.
