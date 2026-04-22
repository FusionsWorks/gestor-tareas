import { render, screen, fireEvent } from '@testing-library/react'
import { vi } from 'vitest'
import TaskFilter from '../TaskFilter'

describe('TaskFilter', () => {
  test('renderiza los 4 botones de filtro', () => {
    render(<TaskFilter filter="" onChange={() => {}} />)
    expect(screen.getByText('Todas')).toBeInTheDocument()
    expect(screen.getByText('Pendiente')).toBeInTheDocument()
    expect(screen.getByText('En progreso')).toBeInTheDocument()
    expect(screen.getByText('Completada')).toBeInTheDocument()
  })

  test('llama onChange con el estado correcto al hacer click', () => {
    const onChange = vi.fn()
    render(<TaskFilter filter="" onChange={onChange} />)
    fireEvent.click(screen.getByText('Pendiente'))
    expect(onChange).toHaveBeenCalledWith('pendiente')
  })

  test('aplica clase active al filtro seleccionado', () => {
    render(<TaskFilter filter="pendiente" onChange={() => {}} />)
    expect(screen.getByText('Pendiente')).toHaveClass('active')
  })
})