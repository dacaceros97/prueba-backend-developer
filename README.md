# Prueba Backend Developer

Este proyecto es una API RESTful para la gestión de usuarios, departamentos y proyectos, construida con ASP.NET Core.

## Requisitos Previos

Se necesita tener instalado lo siguiente:

- [.NET 6.0 o superior](https://dotnet.microsoft.com/download) ✅
- [MySQL](https://dev.mysql.com/downloads/installer/) 🐬
- Visual Studio Code

## Configuración
### Para construir el proyecto
- dotnet build
### Para correr el proyecto
- dotnet run

## 📚 API Endpoints
- Registro: POST /api/auth/register
- Login: POST /api/auth/login
### Gestión de Usuarios:
- Obtener todos: GET /api/users
- Crear: POST /api/users
- Actualizar: PUT /api/users/{id}
- Eliminar: DELETE /api/users/{id}
### Gestión de Departamentos:
- Obtener todos: GET /api/departments
- Crear: POST /api/departments
- Actualizar: PUT /api/departments/{id}
- Eliminar: DELETE /api/departments/{id}
### Gestión de Proyectos:
- Obtener todos: GET /api/projects
- Crear: POST /api/projects
- Actualizar: PUT /api/projects/{id}
- Eliminar: DELETE /api/projects/{id}
