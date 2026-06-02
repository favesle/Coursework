import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { useAuth } from '../context/AuthContext';

const RoomsPage = () => {
  const [rooms, setRooms] = useState([]);
  const [form, setForm] = useState({ roomNumber: '', type: '', capacity: 1, price: 0 });
  const { user } = useAuth();
  const isAdmin = user?.role === 'Admin';

  const fetchRooms = () => api.get('/rooms').then(res => setRooms(res.data));
  useEffect(() => { fetchRooms(); }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    await api.post('/rooms', form);
    fetchRooms();
    setForm({ roomNumber: '', type: '', capacity: 1, price: 0 });
  };

  const handleDelete = async (id) => {
    if (window.confirm('Удалить номер?')) await api.delete(`/rooms/${id}`);
    fetchRooms();
  };

  const updateStatus = async (id, status) => {
    await api.patch(`/rooms/${id}/status`, { status });
    fetchRooms();
  };

  const statusColors = {
    Available: 'bg-green-100 text-green-800',
    Occupied: 'bg-red-100 text-red-800',
    Cleaning: 'bg-blue-100 text-blue-800',  // сделали голубым
    Maintenance: 'bg-gray-100 text-gray-800'
  };

  return (
    <div>
      <h1 className="text-2xl mb-4 text-pink-700">Номера</h1>
      {isAdmin && (
        <form onSubmit={handleSubmit} className="mb-6 flex flex-wrap gap-2 bg-white p-4 rounded shadow border-t-2 border-blue-200">
          <input placeholder="Номер" value={form.roomNumber} onChange={e => setForm({...form, roomNumber: e.target.value})} className="border p-1 rounded focus:ring-blue-300 focus:border-blue-300" />
          <input placeholder="Тип" value={form.type} onChange={e => setForm({...form, type: e.target.value})} className="border p-1 rounded focus:ring-blue-300 focus:border-blue-300" />
          <input type="number" placeholder="Вместимость" value={form.capacity} onChange={e => setForm({...form, capacity: +e.target.value})} className="border p-1 rounded w-24" />
          <input type="number" placeholder="Цена" value={form.price} onChange={e => setForm({...form, price: +e.target.value})} className="border p-1 rounded w-24" />
          <button type="submit" className="bg-pink-400 text-white px-3 py-1 rounded hover:bg-pink-500">Добавить</button>
        </form>
      )}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {rooms.map(room => (
          <div key={room.id} className="bg-white rounded shadow p-4 border-t-2 border-pink-200">
            <div className="flex justify-between items-start">
              <div>
                <h3 className="text-xl font-bold">№{room.roomNumber}</h3>
                <p className="text-gray-600">{room.type} | Вместимость: {room.capacity}</p>
                <p className="text-lg font-semibold">{room.price} ₽/ночь</p>
              </div>
              <span className={`px-2 py-1 rounded text-xs font-semibold ${statusColors[room.status]}`}>
                {room.status === 'Available' ? 'Свободен' : room.status === 'Occupied' ? 'Занят' : room.status === 'Cleaning' ? 'Уборка' : 'Ремонт'}
              </span>
            </div>
            {(isAdmin || user?.role === 'Staff') && (
              <div className="mt-3 flex gap-2">
                <select onChange={(e) => updateStatus(room.id, e.target.value)} value={room.status} className="border rounded p-1 text-sm focus:ring-blue-300 focus:border-blue-300">
                  <option value="Available">Свободен</option>
                  <option value="Occupied">Занят</option>
                  <option value="Cleaning">Уборка</option>
                  <option value="Maintenance">Ремонт</option>
                </select>
                {isAdmin && <button onClick={() => handleDelete(room.id)} className="text-red-500 text-sm hover:text-red-700">Удалить</button>}
              </div>
            )}
          </div>
        ))}
      </div>
    </div>
  );
};

export default RoomsPage;