const ESTADOS = ['', 'pendiente', 'en-progreso', 'completada']
const LABELS  = { '': 'Todas', 'pendiente': 'Pendiente', 'en-progreso': 'En progreso', 'completada': 'Completada' }

export default function TaskFilter({ filter, onChange }) {
  return (
    <div className="filter-bar">
      {ESTADOS.map(e => (
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