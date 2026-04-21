import { useState, useEffect } from 'react'

const EMPTY = { titulo: '', descripcion: '', estado: 'pendiente', userId: '' }

export default function TaskForm({ onSubmit, editingTask, onCancel, users }) {
  const [form, setForm] = useState(EMPTY)

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

  const handleChange = e => setForm({ ...form, [e.target.name]: e.target.value })

  const handleSubmit = e => {
    e.preventDefault()
    if (!form.titulo.trim() || !form.userId) return
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