import { useState, useEffect } from 'react'
import { getUsers, createUser } from '../services/userService'

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

  useEffect(() => { fetchUsers() }, [])

  const addUser = async (data) => {
    await createUser(data)
    await fetchUsers()
  }

  return { users, loading, error, addUser }
}