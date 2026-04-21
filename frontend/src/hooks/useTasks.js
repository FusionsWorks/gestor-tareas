import { useState, useEffect, useCallback } from 'react'
import { getTasks, createTask, updateTask, deleteTask } from '../services/taskService'

export function useTasks() {
  const [tasks, setTasks]     = useState([])
  const [loading, setLoading] = useState(false)
  const [error, setError]     = useState(null)
  const [filter, setFilter]   = useState('')

  const fetchTasks = useCallback(async () => {
    setLoading(true)
    setError(null)
    try {
      const data = await getTasks(filter)
      setTasks(data)
    } catch (err) {
      setError(err.message)
    } finally {
      setLoading(false)
    }
  }, [filter])

  useEffect(() => {
    fetchTasks()
  }, [fetchTasks])

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