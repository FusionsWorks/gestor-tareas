import { render, screen } from '@testing-library/react'
import TaskList from '../TaskList'

// Testing para TaskList

const mockTasks = [
  { id: 1, titulo: 'Tarea uno', descripcion: 'Desc', estado: 'pendiente', userId: 1, userNombre: 'Ana' },
  { id: 2, titulo: 'Tarea dos', descripcion: 'Desc', estado: 'completada', userId: 1, userNombre: 'Ana' }
]

describe('TaskList', () => {
  test('muestra mensaje cuando no hay tareas', () => {
    render(<TaskList tasks={[]} onEdit={() => {}} onDelete={() => {}} users={[]} />)
    expect(screen.getByText('No hay tareas para mostrar.')).toBeInTheDocument()
  })

  test('renderiza una card por cada tarea', () => {
    render(<TaskList tasks={mockTasks} onEdit={() => {}} onDelete={() => {}} users={[]} />)
    expect(screen.getByText('Tarea uno')).toBeInTheDocument()
    expect(screen.getByText('Tarea dos')).toBeInTheDocument()
  })
})