USE GestorTareas;

-- Usuarios de prueba
INSERT INTO dbo.Users (Nombre, Email, FechaCreacion) VALUES
    ('Ana García',      'ana@example.com',     GETDATE()),
    ('Carlos López',    'carlos@example.com',  GETDATE()),
    ('María Fernández', 'maria@example.com',   GETDATE());

-- Tareas de prueba (usan los IDs recién insertados)
INSERT INTO dbo.Tasks (Titulo, Descripcion, Estado, UserId, FechaCreacion)
SELECT 'Diseñar base de datos',    'Crear el esquema ERD y las migraciones',            'completada',  Id, GETDATE() FROM dbo.Users WHERE Email = 'ana@example.com'
UNION ALL
SELECT 'Implementar API REST',     'Endpoints CRUD para tareas con ASP.NET Core',       'en-progreso', Id, GETDATE() FROM dbo.Users WHERE Email = 'ana@example.com'
UNION ALL
SELECT 'Crear componentes React',  'TaskList, TaskForm y TaskFilter',                   'pendiente',   Id, GETDATE() FROM dbo.Users WHERE Email = 'carlos@example.com'
UNION ALL
SELECT 'Configurar CORS',          'Permitir requests desde el frontend en Vite',       'completada',  Id, GETDATE() FROM dbo.Users WHERE Email = 'carlos@example.com'
UNION ALL
SELECT 'Escribir tests unitarios', 'Cubrir services y repositories con xUnit',          'pendiente',   Id, GETDATE() FROM dbo.Users WHERE Email = 'maria@example.com'
UNION ALL
SELECT 'Documentar el README',     'Instrucciones de instalación y decisiones técnicas','pendiente',   Id, GETDATE() FROM dbo.Users WHERE Email = 'maria@example.com';