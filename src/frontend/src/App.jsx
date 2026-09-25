import { useEffect, useState } from 'react'
import catalogApi from './api/catalogApi'
import ordersApi from './api/ordersApi'
import './App.css'

function App() {
  const [events, setEvents] = useState([])
  const [selectedTicket, setSelectedTicket] = useState(null)
  const [form, setForm] = useState({ customerName: '', customerEmail: '', quantity: 1 })
  const [message, setMessage] = useState(null)

  useEffect(() => {
    catalogApi.get('/api/events')
      .then(res => setEvents(res.data))
      .catch(() => setMessage('No se pudo conectar con CatalogApi'))
  }, [])

  const handleBuyClick = (event, ticketType) => {
    setSelectedTicket({ eventId: event.id, eventName: event.name, ...ticketType })
    setMessage(null)
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    try {
      const res = await ordersApi.post('/api/orders', {
        customerName: form.customerName,
        customerEmail: form.customerEmail,
        eventId: selectedTicket.eventId,
        ticketTypeId: selectedTicket.id,
        quantity: Number(form.quantity)
      })
      setMessage(`Orden creada: ${res.data.quantity} x ${res.data.ticketTypeName} - Total $${res.data.totalPrice}`)
      setSelectedTicket(null)
      setForm({ customerName: '', customerEmail: '', quantity: 1 })
    } catch (err) {
      setMessage(err.response?.data ?? 'Error al crear la orden')
    }
  }

  return (
    <div className="app">
      <h1>TicketFlow</h1>

      {message && <p className="message">{message}</p>}

      <div className="events">
        {events.map(event => (
          <div key={event.id} className="event-card">
            <h2>{event.name}</h2>
            <p>{event.venue}</p>
            <ul>
              {event.ticketTypes.map(ticket => (
                <li key={ticket.id}>
                  {ticket.name} - ${ticket.price} ({ticket.quantityAvailable} disponibles)
                  <button onClick={() => handleBuyClick(event, ticket)}>Comprar</button>
                </li>
              ))}
            </ul>
          </div>
        ))}
      </div>

      {selectedTicket && (
        <form onSubmit={handleSubmit} className="order-form">
          <h3>Comprar {selectedTicket.name} - {selectedTicket.eventName}</h3>
          <input
            placeholder="Nombre"
            value={form.customerName}
            onChange={e => setForm({ ...form, customerName: e.target.value })}
            required
          />
          <input
            type="email"
            placeholder="Correo"
            value={form.customerEmail}
            onChange={e => setForm({ ...form, customerEmail: e.target.value })}
            required
          />
          <input
            type="number"
            min="1"
            value={form.quantity}
            onChange={e => setForm({ ...form, quantity: e.target.value })}
            required
          />
          <button type="submit">Confirmar compra</button>
        </form>
      )}
    </div>
  )
}

export default App
