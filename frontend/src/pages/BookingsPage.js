import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { useAuth } from '../context/AuthContext';

const BookingsPage = () => {
  const [bookings, setBookings] = useState([]);
  const [clients, setClients] = useState([]);
  const [rooms, setRooms] = useState([]);
  const [form, setForm] = useState({ clientId: '', roomId: '', checkIn: '', checkOut: '' });
  const { user } = useAuth();
  const canManage = user?.role === 'Admin' || user?.role === 'Manager';

  const fetchData = async () => {
    const [bookRes, clientRes, roomRes] = await Promise.all([api.get('/bookings'), api.get('/clients'), api.get('/rooms')]);
    setBookings(bookRes.data);
    setClients(clientRes.data);
    setRooms(roomRes.data);
  };
  useEffect(() => { fetchData(); }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    await api.post('/bookings', form);
    fetchData();
    setForm({ clientId: '', roomId: '', checkIn: '', checkOut: '' });
  };

  const cancelBooking = async (id) => {
    await api.patch(`/bookings/${id}/cancel`);
    fetchData();
  };

  return (
    <div>
      <h1 className="text-2xl mb-4 text-pink-700">Бронирования</h1>
      {canManage && (
        <form onSubmit={handleSubmit} className="mb-6 flex flex-wrap gap-2 bg-white p-4 rounded shadow border-t-2 border-blue-200">
          <select value={form.clientId} onChange={e => setForm({...form, clientId: e.target.value})} className="border p-1 rounded focus:ring-blue-300 focus:border-blue-300" required>
            <option value="">Клиент</option>
            {clients.map(c => <option key={c.id} value={c.id}>{c.fullName}</option>)}
          </select>
          <select value={form.roomId} onChange={e => setForm({...form, roomId: e.target.value})} className="border p-1 rounded focus:ring-blue-300 focus:border-blue-300" required>
            <option value="">Номер</option>
            {rooms.filter(r => r.status === 'Available').map(r => <option key={r.id} value={r.id}>{r.roomNumber}</option>)}
          </select>
          <input type="date" value={form.checkIn} onChange={e => setForm({...form, checkIn: e.target.value})} className="border p-1 rounded focus:ring-blue-300 focus:border-blue-300" required />
          <input type="date" value={form.checkOut} onChange={e => setForm({...form, checkOut: e.target.value})} className="border p-1 rounded focus:ring-blue-300 focus:border-blue-300" required />
          <button type="submit" className="bg-pink-400 text-white px-3 py-1 rounded hover:bg-pink-500">Забронировать</button>
        </form>
      )}
      <table className="min-w-full bg-white rounded shadow">
        <thead className="bg-pink-50 text-pink-800">
          <tr><th className="p-2">ID</th><th>Клиент</th><th>Номер</th><th>Заезд</th><th>Выезд</th><th>Статус</th>{canManage && <th></th>}</tr>
        </thead>
        <tbody>
          {bookings.map(b => (
            <tr key={b.id} className="border-b hover:bg-pink-50">
              <td className="p-2">{b.id}</td>
              <td>{b.clientName}</td>
              <td>{b.roomNumber}</td>
              <td>{new Date(b.checkIn).toLocaleDateString()}</td>
              <td>{new Date(b.checkOut).toLocaleDateString()}</td>
              <td>{b.status === 'Active' ? 'Активно' : 'Отменено'}</td>
              {canManage && b.status === 'Active' && <td><button onClick={() => cancelBooking(b.id)} className="text-blue-500 hover:text-blue-700">Отменить</button></td>}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default BookingsPage;
