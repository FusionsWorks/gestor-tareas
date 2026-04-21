const BASE_URL = `${import.meta.env.VITE_API_URL}/api/users`

export async function getUsers() {
  const res = await fetch(BASE_URL)
  if (!res.ok) throw new Error('Error al obtener usuarios')
  return res.json()
}

export async function createUser(data) {
  const res = await fetch(BASE_URL, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data)
  })
  if (!res.ok) throw new Error('Error al crear usuario')
  return res.json()
}