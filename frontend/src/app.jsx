import { useTasks } from './hooks/useTasks'
import { useUsers } from './hooks/useUsers'
import TaskFilter from './components/TaskFilter'
import TaskForm from './components/TaskForm'
import TaskList from './components/TaskList'
import UserForm from './components/UserForm'

export default function App() {
  const { tasks, loading, error, filter, setFilter, addTask, editTask, removeTask } = useTasks()
  const { users, addUser } = useUsers()

  return (
    <div className="app">
      <header className="app-header">
        <h1>Gestor de Tareas</h1>
      </header>

      <main className="app-main">
        <aside className="app-sidebar">
          <TaskForm
            onSubmit={addTask}
            users={users}
          />
          <UserForm onSubmit={addUser} />
        </aside>

        <section className="app-content">
          <TaskFilter filter={filter} onChange={setFilter} />

          {loading && <p className="status">Cargando...</p>}
          {error   && <p className="status error">Error: {error}</p>}

          {!loading && !error && (
            <TaskList
              tasks={tasks}
              onEdit={editTask}
              onDelete={removeTask}
              users={users}
            />
          )}
        </section>
      </main>
    </div>
  )
}