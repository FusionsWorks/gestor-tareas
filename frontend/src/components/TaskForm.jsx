import { useState, useEffect } from 'react'

// Estado vacío definido fuera del componente para reutilizarlo
// al resetear el formulario sin crear un objeto nuevo cada vez.

const EMPTY = { titulo: '', descripcion: '', estado: 'pendiente', userId: '' }

// Formulario compartido para crear y editar tareas.
// Si recibe editingTask, entra en modo edición y precarga los valores.
// Si no, actúa como formulario de creación.
export default function TaskForm({ onSubmit, editingTask, onCancel, users }) {
  const [form, setForm] = useState(EMPTY)

  // Cuando cambia la tarea en edición, se sincronizan los campos del formulario.
  // Si editingTask es null (se canceló o se guardó), se resetea a vacío.

  useEffect(() => {
    if (editingTask) {
      setForm({
        titulo:      editingTask.titulo,
        descripcion: editingTask.descripcion,
        estado:      editingTask.estado,
        userId:      editingTask.userId
      })
    } else {
      setForm(EMPTY)
    }
  }, [editingTask])

  // Manejador genérico — usa el atributo name del input para actualizar
  // el campo correspondiente sin necesitar un handler por campo.
  const handleChange = e => setForm({ ...form, [e.target.name]: e.target.value })

  const handleSubmit = e => {
    e.preventDefault()
    // Validación mínima: título no vacío y usuario seleccionado.
    if (!form.titulo.trim() || !form.userId) return
    // userId llega como string desde el select — se convierte a número.
    onSubmit({ ...form, userId: Number(form.userId) })
    setForm(EMPTY)
  }

  return (
    <form className="task-form" onSubmit={handleSubmit}>
      <h2>{editingTask ? 'Editar tarea' : 'Nueva tarea'}</h2>

      <input
        name="titulo"
        placeholder="Título"
        value={form.titulo}
        onChange={handleChange}
        required
      />

      <textarea
        name="descripcion"
        placeholder="Descripción"
        value={form.descripcion}
        onChange={handleChange}
        rows={3}
      />

      <select name="estado" value={form.estado} onChange={handleChange}>
        <option value="pendiente">Pendiente</option>
        <option value="en-progreso">En progreso</option>
        <option value="completada">Completada</option>
      </select>

      <select name="userId" value={form.userId} onChange={handleChange} required>
        <option value="">-- Seleccionar usuario --</option>
        {users.map(u => (
          <option key={u.id} value={u.id}>{u.nombre}</option>
        ))}
      </select>

      <div className="form-actions">
        <button type="submit">{editingTask ? 'Guardar cambios' : 'Crear tarea'}</button>
        {editingTask && (
          <button type="button" className="btn-cancel" onClick={onCancel}>
            Cancelar
          </button>
        )}
      </div>
    </form>
  )
}