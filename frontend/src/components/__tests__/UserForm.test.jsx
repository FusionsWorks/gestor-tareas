import { render, screen, fireEvent } from '@testing-library/react'
import { vi } from 'vitest'
import UserForm from '../UserForm'

describe('UserForm', () => {
  test('llama onSubmit con los datos correctos', () => {
    const onSubmit = vi.fn()
    render(<UserForm onSubmit={onSubmit} />)
    fireEvent.change(screen.getByPlaceholderText('Nombre'), { target: { value: 'Juan' } })
    fireEvent.change(screen.getByPlaceholderText('Email'),  { target: { value: 'juan@test.com' } })
    fireEvent.click(screen.getByText('Crear usuario'))
    expect(onSubmit).toHaveBeenCalledWith({ nombre: 'Juan', email: 'juan@test.com' })
  })

  test('no llama onSubmit si los campos están vacíos', () => {
    const onSubmit = vi.fn()
    render(<UserForm onSubmit={onSubmit} />)
    fireEvent.click(screen.getByText('Crear usuario'))
    expect(onSubmit).not.toHaveBeenCalled()
  })
})