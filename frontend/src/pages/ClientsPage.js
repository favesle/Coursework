import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { useAuth } from '../context/AuthContext';

const ClientsPage = () => {
  const [clients, setClients] = useState([]);
  const [form, setForm] = useState({ fullName: '', passport: '', phone: '', email: '' });
  const { user } = useAuth();
  const isAdmin = user?.role === 'Admin';

  const fetchClients = () => {
    api.get('/clients').then(res => setClients(res.data));
  };

  useEffect(() => { fetchClients(); }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await api.post('/clients', form);
      fetchClients();
      setForm({ fullName: '', passport: '', phone: '', email: '' });
    } catch (err) { alert('Ошибка'); }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Удалить клиента?')) {
      await api.delete(`/clients/${id}`);
      fetchClients();
    }
  };

  return (
    <div>
      <h1 className="text-2xl mb-4 text-pink-700">Клиенты</h1>
      {isAdmin && (
        <form onSubmit={handleSubmit} className="mb-6 flex flex-wrap gap-2 bg-white p-4 rounded shadow border-t-2 border-pink-200">
          <input placeholder="ФИО" value={form.fullName} onChange={e => setForm({...form, fullName: e.target.value})} className="border p-1 rounded focus:ring-pink-300" />
          <input placeholder="Паспорт" value={form.passport} onChange={e => setForm({...form, passport: e.target.value})} className="border p-1 rounded focus:ring-pink-300" />
          <input placeholder="Телефон" value={form.phone} onChange={e => setForm({...form, phone: e.target.value})} className="border p-1 rounded focus:ring-pink-300" />
          <input placeholder="Email" value={form.email} onChange={e => setForm({...form, email: e.target.value})} className="border p-1 rounded focus:ring-pink-300" />
          <button type="submit" className="bg-pink-400 text-white px-3 py-1 rounded hover:bg-pink-500 transition">Добавить</button>
        </form>
      )}
      <div className="overflow-x-auto">
        <table className="min-w-full bg-white rounded shadow">
          <thead className="bg-pink-50 text-pink-800">
            <tr>
              <th className="p-2">ID</th><th>ФИО</th><th>Паспорт</th><th>Телефон</th><th>Email</th>{isAdmin && <th></th>}
            </tr>
          </thead>
          <tbody>
            {clients.map(c => (
              <tr key={c.id} className="border-b hover:bg-pink-50">
                <td className="p-2">{c.id}</td>
                <td>{c.fullName}</td>
                <td>{c.passport}</td>
                <td>{c.phone}</td>
                <td>{c.email}</td>
                {isAdmin && <td><button onClick={() => handleDelete(c.id)} className="text-red-500 hover:text-red-700">Удалить</button></td>}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default ClientsPage;