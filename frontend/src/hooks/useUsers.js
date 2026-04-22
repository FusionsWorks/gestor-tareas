import { useState, useEffect } from 'react'
import { getUsers, createUser } from '../services/userService'

// Hook personalizado para gestión de usuarios.
// Sigue el mismo patrón que useTasks: encapsula fetch, estado y operaciones.

export function useUsers() {
  const [users, setUsers]     = useState([])
  const [loading, setLoading] = useState(false)
  const [error, setError]     = useState(null)

  const fetchUsers = async () => {
    setLoading(true)
    try {
      const data = await getUsers()
      setUsers(data)
    } catch (err) {
      setError(err.message)
    } finally {
      setLoading(false)
    }
  }

  // Carga los usuarios una sola vez al montar el componente que use este hook.
  useEffect(() => { fetchUsers() }, [])

  // Crea un usuario y recarga la lista para reflejar el nuevo registro.
  const addUser = async (data) => {
    await createUser(data)
    await fetchUsers()
  }

  return { users, loading, error, addUser }
}