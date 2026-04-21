const BASE_URL = `${import.meta.env.VITE_API_URL}/api/tasks`

export async function getTasks(status = '') {
  const url = status ? `${BASE_URL}?status=${status}` : BASE_URL
  const res = await fetch(url)
  if (!res.ok) throw new Error('Error al obtener tareas')
  return res.json()
}

export async function getTaskById(id) {
  const res = await fetch(`${BASE_URL}/${id}`)
  if (!res.ok) throw new Error('Tarea no encontrada')
  return res.json()
}

export async function createTask(data) {
  const res = await fetch(BASE_URL, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data)
  })
  if (!res.ok) throw new Error('Error al crear tarea')
  return res.json()
}

export async function updateTask(id, data) {
  const res = await fetch(`${BASE_URL}/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data)
  })
  if (!res.ok) throw new Error('Error al actualizar tarea')
  return res.json()
}

export async function deleteTask(id) {
  const res = await fetch(`${BASE_URL}/${id}`, { method: 'DELETE' })
  if (!res.ok) throw new Error('Error al eliminar tarea')
}