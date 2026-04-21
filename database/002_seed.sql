USE GestorTareas;

INSERT INTO dbo.Users (Nombre, Email) VALUES
    ('Ana García',     'ana@example.com'),
    ('Carlos López',   'carlos@example.com'),
    ('María Fernández','maria@example.com');

INSERT INTO dbo.Tasks (Titulo, Descripcion, Estado, UserId) VALUES
    ('Diseñar base de datos',    'Crear el esquema ERD y las migraciones',           'completada',  1),
    ('Implementar API REST',     'Endpoints CRUD para tareas con ASP.NET Core',       'en-progreso', 1),
    ('Crear componentes React',  'TaskList, TaskForm y TaskFilter',                   'pendiente',   2),
    ('Configurar CORS',          'Permitir requests desde el frontend en Vite',       'completada',  2),
    ('Escribir tests unitarios', 'Cubrir services y repositories con xUnit',          'pendiente',   3),
    ('Documentar el README',     'Instrucciones de instalación y decisiones técnicas','pendiente',   3);