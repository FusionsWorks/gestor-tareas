import { useState, useEffect, useCallback } from 'react'
import { getTasks, createTask, updateTask, deleteTask } from '../services/taskService'

// Hook personalizado que encapsula toda la lógica de tareas.
// Expone los datos y las operaciones CRUD (crear, leer, actualizar y eliminar) para que los componentes
// no necesiten saber nada sobre fetch ni manejo de estado.

export function useTasks() {
  const [tasks, setTasks]     = useState([])
  const [loading, setLoading] = useState(false)
  const [error, setError]     = useState(null)
  const [filter, setFilter]   = useState('')

  // useCallback evita que fetchTasks sea una función nueva en cada render.
  // Solo se recrea cuando cambia el filtro, lo que a su vez dispara el useEffect.
  const fetchTasks = useCallback(async () => {
    setLoading(true)
    setError(null)
    try {
      const data = await getTasks(filter)
      setTasks(data)
    } catch (err) {
      setError(err.message)
    } finally {
      // finally garantiza que loading se apaga siempre,
      // incluso si el fetch falla.
      setLoading(false)
    }
  }, [filter])

  // Se ejecuta al montar y cada vez que fetchTasks cambia (es decir, cuando cambia el filtro).
  useEffect(() => {
    fetchTasks()
  }, [fetchTasks])

  // Cada mutación llama a fetchTasks después para mantener
  // la lista sincronizada con la base de datos.
  const addTask = async (data) => {
    await createTask(data)
    await fetchTasks()
  }

  const editTask = async (id, data) => {
    await updateTask(id, data)
    await fetchTasks()
  }

  const removeTask = async (id) => {
    await deleteTask(id)
    await fetchTasks()
  }

  return { tasks, loading, error, filter, setFilter, addTask, editTask, removeTask }
}