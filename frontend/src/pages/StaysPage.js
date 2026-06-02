import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { useAuth } from '../context/AuthContext';

const StaysPage = () => {
  const [stays, setStays] = useState([]);
  const [clients, setClients] = useState([]);
  const [rooms, setRooms] = useState([]);
  const [services, setServices] = useState([]);
  const [form, setForm] = useState({ clientId: '', roomId: '', startDate: '', endDate: '', services: [] });
  const { user } = useAuth();
  const canManage = user?.role === 'Admin' || user?.role === 'Manager';

  const fetchData = async () => {
    const [stayRes, clientRes, roomRes, servRes] = await Promise.all([api.get('/stays'), api.get('/clients'), api.get('/rooms'), api.get('/services')]);
    setStays(stayRes.data);
    setClients(clientRes.data);
    setRooms(roomRes.data);
    setServices(servRes.data);
  };
  useEffect(() => { fetchData(); }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    await api.post('/stays', { ...form, services: form.services.filter(s => s.serviceId) });
    fetchData();
    setForm({ clientId: '', roomId: '', startDate: '', endDate: '', services: [] });
  };

  const checkout = async (id) => {
    await api.patch(`/stays/${id}/checkout`);
    fetchData();
  };

  return (
    <div>
      <h1 className="text-2xl mb-4 text-pink-700">Проживания</h1>
      {canManage && (
        <form onSubmit={handleSubmit} className="mb-6 bg-white p-4 rounded shadow border-t-2 border-pink-200 space-y-2">
          <div className="flex flex-wrap gap-2">
            <select value={form.clientId} onChange={e => setForm({...form, clientId: e.target.value})} className="border p-1 rounded focus:ring-blue-300" required>
              <option value="">Клиент</option>
              {clients.map(c => <option key={c.id} value={c.id}>{c.fullName}</option>)}
            </select>
            <select value={form.roomId} onChange={e => setForm({...form, roomId: e.target.value})} className="border p-1 rounded focus:ring-blue-300" required>
              <option value="">Номер</option>
              {rooms.filter(r => r.status === 'Available').map(r => <option key={r.id} value={r.id}>{r.roomNumber}</option>)}
            </select>
            <input type="date" value={form.startDate} onChange={e => setForm({...form, startDate: e.target.value})} className="border p-1 rounded focus:ring-blue-300" required />
            <input type="date" value={form.endDate} onChange={e => setForm({...form, endDate: e.target.value})} className="border p-1 rounded focus:ring-blue-300" required />
          </div>
          <div>
            <label className="block text-sm">Услуги:</label>
            {services.map(s => (
              <label key={s.id} className="inline-flex items-center mr-4">
                <input type="checkbox" value={s.id} onChange={e => {
                  const checked = e.target.checked;
                  setForm(prev => ({
                    ...prev,
                    services: checked ? [...prev.services, { serviceId: s.id, quantity: 1 }] : prev.services.filter(ss => ss.serviceId !== s.id)
                  }));
                }} className="mr-1 accent-pink-500" /> {s.name} ({s.price}₽)
              </label>
            ))}
          </div>
          <button type="submit" className="bg-pink-400 text-white px-3 py-1 rounded hover:bg-pink-500">Заселить</button>
        </form>
      )}
      <table className="min-w-full bg-white rounded shadow">
        <thead className="bg-pink-50 text-pink-800">
          <tr><th>ID</th><th>Клиент</th><th>Номер</th><th>Заезд</th><th>Выезд</th><th>Услуги</th><th></th></tr>
        </thead>
        <tbody>
          {stays.map(s => (
            <tr key={s.id} className="border-b hover:bg-pink-50">
              <td className="p-2">{s.id}</td>
              <td>{s.clientFullName}</td>
              <td>{s.roomNumber}</td>
              <td>{new Date(s.startDate).toLocaleDateString()}</td>
              <td>{s.endDate ? new Date(s.endDate).toLocaleDateString() : '—'}</td>
              <td>{s.services?.map(ss => `${ss.serviceName} x${ss.quantity}`).join(', ') || '—'}</td>
              <td>{canManage && !s.endDate && <button onClick={() => checkout(s.id)} className="text-blue-500 hover:text-blue-700">Выселить</button>}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default StaysPage;