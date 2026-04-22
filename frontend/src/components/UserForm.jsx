import { useState } from 'react'

// Estado vacío definido fuera para reutilizar en el reset post-submit.

const EMPTY = { nombre: '', email: '' }

// Formulario de creación de usuarios.
// Diseño simple — solo nombre y email, que son los campos requeridos por la API.

export default function UserForm({ onSubmit }) {
  const [form, setForm] = useState(EMPTY)

  const handleChange = e => setForm({ ...form, [e.target.name]: e.target.value })

  const handleSubmit = e => {
    e.preventDefault()
    if (!form.nombre.trim() || !form.email.trim()) return
    onSubmit(form)
    // Resetea el formulario tras el envío exitoso.
    setForm(EMPTY)
  }

  return (
    <form className="task-form" style={{ marginTop: '1rem' }} onSubmit={handleSubmit}>
      <h2>Nuevo usuario</h2>
      <input name="nombre" placeholder="Nombre" value={form.nombre} onChange={handleChange} required />
      <input name="email"  placeholder="Email"  value={form.email}  onChange={handleChange} required type="email" />
      <button type="submit">Crear usuario</button>
    </form>
  )
}