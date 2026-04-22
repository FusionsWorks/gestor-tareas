import { useState } from 'react'

// El estado de edición es local a cada card (useState interno).
// Es puramente decisión de diseño: en lugar de levantar el estado al App y pasar
// editingTask como prop, cada TaskCard es autónoma.
// Ventaja: App.jsx queda más simple y cada card no interfiere con las demás.

const ESTADO_CLASS = {
  'pendiente':   'badge badge-pendiente',
  'en-progreso': 'badge badge-progreso',
  'completada':  'badge badge-completada'
}

export default function TaskCard({ task, onEdit, onDelete, users }) {
    // Controla si esta card específica está en modo edición
  const [editing, setEditing] = useState(false)
  const [form, setForm] = useState({
     // Copia local del formulario — inicializada con los valores actuales de la tarea
    titulo:      task.titulo,
    descripcion: task.descripcion,
    estado:      task.estado,
    userId:      task.userId
  })

  const handleChange = e => setForm({ ...form, [e.target.name]: e.target.value })

  const handleSave = async () => {
    await onEdit(task.id, { ...form, userId: Number(form.userId) })
    setEditing(false)
  }

  if (editing) {
    return (
      <li className="task-card task-card--editing">
        <input
          name="titulo"
          value={form.titulo}
          onChange={handleChange}
          placeholder="Título"
        />
        <textarea
          name="descripcion"
          value={form.descripcion}
          onChange={handleChange}
          rows={2}
          placeholder="Descripción"
        />
        <div className="edit-row">
          <select name="estado" value={form.estado} onChange={handleChange}>
            <option value="pendiente">Pendiente</option>
            <option value="en-progreso">En progreso</option>
            <option value="completada">Completada</option>
          </select>
          <select name="userId" value={form.userId} onChange={handleChange}>
            {(users ?? []).map(u => (
              <option key={u.id} value={u.id}>{u.nombre}</option>
            ))}
          </select>
        </div>
        <div className="form-actions">
          <button onClick={handleSave}>Guardar</button>
          <button className="btn-cancel" onClick={() => setEditing(false)}>Cancelar</button>
        </div>
      </li>
    )
  }

  return (
    <li className="task-card">
      <div className="task-header">
        <h3>{task.titulo}</h3>
        <span className={ESTADO_CLASS[task.estado]}>{task.estado}</span>
      </div>
      {task.descripcion && <p className="task-desc">{task.descripcion}</p>}
      <div className="task-footer">
        <span className="task-user">👤 {task.userNombre ?? `Usuario #${task.userId}`}</span>
        <div className="task-actions">
          <button onClick={() => setEditing(true)}>Editar</button>
          <button className="btn-danger" onClick={() => onDelete(task.id)}>Eliminar</button>
        </div>
      </div>
    </li>
  )
}