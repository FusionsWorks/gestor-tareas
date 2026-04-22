import TaskCard from './TaskCard'

// Componente de lista — recibe las tareas ya filtradas desde el hook useTasks.
// No filtra ni transforma datos, solo renderiza una TaskCard por tarea.

export default function TaskList({ tasks, onDelete, onEdit, users }) {
  // Mensaje vacío cuando no hay tareas para el filtro activo.
  if (tasks.length === 0) {
    return <p className="empty">No hay tareas para mostrar.</p>
  }

  return (
    <ul className="task-list">
      {tasks.map(task => (
        // key={task.id} permite a React identificar cada elemento
        // y hacer actualizaciones eficientes del DOM.
        <TaskCard key={task.id} task={task} onDelete={onDelete} onEdit={onEdit} users={users} />
      ))}
    </ul>
  )
}