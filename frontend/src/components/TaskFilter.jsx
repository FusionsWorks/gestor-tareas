const ESTADOS = ['', 'pendiente', 'en-progreso', 'completada']
const LABELS  = { '': 'Todas', 'pendiente': 'Pendiente', 'en-progreso': 'En progreso', 'completada': 'Completada' }

// Constantes fuera del componente para evitar recrearlas en cada render.
// ESTADOS define el orden de los botones; LABELS mapea cada valor a su etiqueta legible.

// Componente de filtro — recibe el filtro activo y un callback para cambiarlo.
// No maneja estado propio: es un componente controlado puro.
export default function TaskFilter({ filter, onChange }) {
  return (
    <div className="filter-bar">
      {ESTADOS.map(e => (
         // La clase 'active' se aplica solo al botón que coincide con el filtro actual.
        <button
          key={e}
          className={`filter-btn ${filter === e ? 'active' : ''}`}
          onClick={() => onChange(e)}
        >
          {LABELS[e]}
        </button>
      ))}
    </div>
  )
}