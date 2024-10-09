# Prueba Backend Developer

Este proyecto es una API RESTful para la gestión de usuarios, departamentos y proyectos, construida con ASP.NET Core.

## Requisitos Previos

Asegúrate de tener instalado lo siguiente:

- [.NET 6.0 o superior](https://dotnet.microsoft.com/download) ✅
- [MySQL](https://dev.mysql.com/downloads/installer/) 🐬

## Configuración


📚 API Endpoints
Registro de Usuario: POST /api/users/register
Autenticación de Usuario: POST /api/users/authenticate
Gestión de Departamentos:
Obtener todos: GET /api/departments
Crear: POST /api/departments
Actualizar: PUT /api/departments/{id}
Eliminar: DELETE /api/departments/{id}
Gestión de Proyectos:
Obtener todos: GET /api/projects
Crear: POST /api/projects
Actualizar: PUT /api/projects/{id}
Eliminar: DELETE /api/projects/{id}
