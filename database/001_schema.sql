-- Tabla de usuarios
CREATE TABLE Users (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Nombre        NVARCHAR(100)  NOT NULL,
    Email         NVARCHAR(150)  NOT NULL UNIQUE,
    FechaCreacion DATETIME2      NOT NULL DEFAULT GETUTCDATE()
);

-- Tabla de tareas
CREATE TABLE Tasks (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Titulo        NVARCHAR(200)  NOT NULL,
    Descripcion   NVARCHAR(1000) NOT NULL DEFAULT '',
    Estado        NVARCHAR(20)   NOT NULL DEFAULT 'pendiente'
                  CHECK (Estado IN ('pendiente', 'en-progreso', 'completada')),
    UserId        INT            NOT NULL,
    FechaCreacion DATETIME2      NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_Tasks_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);