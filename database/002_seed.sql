USE GestorTareas;

INSERT INTO Users (Nombre, Email, FechaCreacion) VALUES 
    ('Ana García',      'ana@example.com',    GETDATE()),
    ('Carlos López',    'carlos@example.com', GETDATE()),
    ('María Fernández', 'maria@example.com',  GETDATE());

INSERT INTO Tasks (Titulo, Descripcion, Estado, UserId, FechaCreacion) VALUES
    ('Diseñar base de datos',    'Crear el esquema ERD y las migraciones',            'completada',  1, GETDATE()),
    ('Implementar API REST',     'Endpoints CRUD para tareas con ASP.NET Core',        'en-progreso', 1, GETDATE()),
    ('Crear componentes React',  'TaskList, TaskForm y TaskFilter',                    'pendiente',   2, GETDATE()),
    ('Configurar CORS',          'Permitir requests desde el frontend en Vite',        'completada',  2, GETDATE()),
    ('Escribir tests unitarios', 'Cubrir services y repositories con xUnit',           'pendiente',   3, GETDATE()),
    ('Documentar el README',     'Instrucciones de instalación y decisiones técnicas', 'pendiente',   3, GETDATE());