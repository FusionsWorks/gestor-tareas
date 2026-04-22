# Gestor de Tareas

Aplicación web fullstack para la gestión de tareas, desarrollada como prueba técnica. Permite crear, editar, eliminar y filtrar tareas, asociadas a usuarios, con persistencia en SQL Server.

---

## Tecnologías utilizadas

- **Frontend:** React.js (Vite), hooks funcionales, fetch API
- **Backend:** ASP.NET Core 8, Web API
- **Base de datos:** SQL Server (Express o LocalDB)
- **ORM:** Entity Framework Core 8
- **Documentación API:** Swagger / Swashbuckle
- **Control de versiones:** Git + GitHub

---

## Estructura del proyecto

```
gestor-tareas/
├── README.md
├── database/
│   ├── 001_schema.sql       # Script de creación de tablas
│   └── 002_seed.sql         # Datos de prueba (3 usuarios, 6 tareas)
├── backend/
│   └── GestorTareas.API/
│       ├── Controllers/     # TasksController, UsersController
│       ├── Services/        # ITaskService, TaskService
│       ├── Repositories/    # ITaskRepository, TaskRepository
│       ├── Models/          # User, TaskItem
│       ├── DTOs/            # TaskDto, CreateTaskDto, UpdateTaskDto, UserDto
│       ├── Data/            # AppDbContext (EF Core)
│       └── Program.cs
└── frontend/
        └── src/
            ├── components/  # TaskList, TaskCard, TaskForm, TaskFilter, UserForm
            ├── hooks/       # useTasks, useUsers
            └── services/    # taskService.js, userService.js
```

---

## Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- SQL Server Express o LocalDB
- Git

---

## Instrucciones para ejecutar localmente

### 1. Clonar el repositorio

```bash
git clone https://github.com/FusionsWorks/gestor-tareas.git
cd gestor-tareas
```

### 2. Configurar la base de datos

En `backend/GestorTareas.API/appsettings.json`, ajustá la connection string según tu instancia:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=GestorTareas;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> Si se usa LocalDB: `Server=(localdb)\\mssqllocaldb;Database=GestorTareas;Trusted_Connection=True;`

### 3. Levantar el backend

```bash
cd backend/GestorTareas.API
dotnet restore
dotnet ef database update   # crea la BD y aplica migraciones
dotnet run
```

La API queda disponible en `http://localhost:5204`.  
Swagger en `http://localhost:5204/swagger`.

### 4. Levantar el frontend

```bash
cd frontend
npm install
npm run dev
```

> Se debe de copiar `.env.example` y renombrarlo a `.env`. Verificar que el puerto coincida con el que levantó el backend en la máquina.

La app queda disponible en `http://localhost:5173`.

---

## Endpoints de la API

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/tasks` | Listar todas las tareas |
| GET | `/api/tasks?status={estado}` | Filtrar por estado |
| GET | `/api/tasks/{id}` | Obtener tarea por ID |
| POST | `/api/tasks` | Crear nueva tarea |
| PUT | `/api/tasks/{id}` | Actualizar tarea existente |
| DELETE | `/api/tasks/{id}` | Eliminar tarea |
| GET | `/api/users` | Listar usuarios |
| POST | `/api/users` | Crear usuario |

Los estados válidos son: `pendiente`, `en-progreso` y `completada`.

---

## Base de datos

El esquema se gestiona mediante migraciones de EF Core. Los archivos en `/database` son la representación equivalente en SQL puro, incluidos como documentación del esquema y datos de prueba.

Las tablas principales son:

- **Users:** id, nombre, email, fechaCreacion
- **Tasks:** id, titulo, descripcion, estado, userId (FK → Users), fechaCreacion

---

## Decisiones de diseño

**Arquitectura en capas (Controller → Service → Repository):** separa las responsabilidades de forma clara. El controller solo maneja HTTP, el service contiene la lógica de negocio, y el repository se encarga del acceso a datos. Esto facilita el testing y el mantenimiento.

**EF Core en lugar de SQL crudo:** permite gestionar el esquema con migraciones versionadas y reduce el boilerplate de acceso a datos. El esquema SQL equivalente se documenta en `/database` para cumplir con el entregable requerido.

**DTOs separados de los modelos:** evita exponer la estructura interna de la base de datos y permite controlar exactamente qué datos entran y salen de la API.

**Edición inline en el frontend:** en lugar de redirigir al formulario lateral, cada tarea expande su propio formulario de edición. Mejora la experiencia sin agregar complejidad.

**Hook `useTasks`:** encapsula todo el estado relacionado a tareas (lista, loading, error, filtro, operaciones CRUD) para mantener `App.jsx` limpio y declarativo.

**CORS configurado explícitamente:** el backend permite requests solo desde `http://localhost:5173` (Vite), evitando exponer la API a otros orígenes en desarrollo.

---
## Testing

Se implementaron tests unitarios para el frontend con **Vitest** y **React Testing Library**.

Los tests cubren:
- `TaskFilter` — renderizado de botones, callback onChange y clase active
- `TaskList` — mensaje de lista vacía y renderizado de tareas
- `UserForm` — submit con datos correctos y validación de campos vacíos

Para correr los tests:
```bash
cd frontend
npm test
```

Se intentó implementar tests unitarios con xUnit, Moq y FluentAssertions
para el `TaskService` en el backend. Por conflictos de versiones entre paquetes
en el entorno de desarrollo, no se incluyeron en el entregable final.

Los tests planificados cubrían:
- `GetAllAsync` — verifica que retorna todas las tareas
- `GetByIdAsync` — verifica retorno correcto e id inexistente (null)
- `CreateAsync` — verifica que mapea correctamente el DTO
- `DeleteAsync` — verifica true/false según si existe la tarea

---

## Herramientas de IA utilizadas

Se utilizó **Claude (Anthropic)** como asistente durante el desarrollo. Su uso fue principalmente para:

- Generar el esqueleto inicial de las capas del backend (modelos, DTOs, repositorio, servicio, controller)
- Resolver configuraciones de EF Core y migraciones
- Estructurar los componentes y hooks de React
- Redactar este README

Todas las decisiones técnicas fueron revisadas, ajustadas y pueden ser explicadas en detalle durante la instancia de devolución.