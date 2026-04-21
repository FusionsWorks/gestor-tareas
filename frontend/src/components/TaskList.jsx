import TaskCard from './TaskCard'

export default function TaskList({ tasks, onDelete, onEdit, users }) {
  if (tasks.length === 0) {
    return <p className="empty">No hay tareas para mostrar.</p>
  }

  return (
    <ul className="task-list">
      {tasks.map(task => (
        <TaskCard key={task.id} task={task} onDelete={onDelete} onEdit={onEdit} users={users} />
      ))}
    </ul>
  )
}