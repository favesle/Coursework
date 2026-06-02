import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { useAuth } from '../context/AuthContext';

const ServicesPage = () => {
  const [services, setServices] = useState([]);
  const [form, setForm] = useState({ name: '', price: 0, description: '' });
  const { user } = useAuth();
  const isAdmin = user?.role === 'Admin';

  const fetchServices = () => api.get('/services').then(res => setServices(res.data));
  useEffect(() => { fetchServices(); }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    await api.post('/services', form);
    fetchServices();
    setForm({ name: '', price: 0, description: '' });
  };

  const handleDelete = async (id) => {
    if (window.confirm('Удалить услугу?')) await api.delete(`/services/${id}`);
    fetchServices();
  };

  return (
    <div>
      <h1 className="text-2xl mb-4">Дополнительные услуги</h1>
      {isAdmin && (
        <form onSubmit={handleSubmit} className="mb-6 flex flex-wrap gap-2 bg-white p-4 rounded shadow">
          <input placeholder="Название" value={form.name} onChange={e => setForm({...form, name: e.target.value})} className="border p-1 rounded" />
          <input type="number" placeholder="Цена" value={form.price} onChange={e => setForm({...form, price: +e.target.value})} className="border p-1 rounded w-24" />
          <input placeholder="Описание" value={form.description} onChange={e => setForm({...form, description: e.target.value})} className="border p-1 rounded flex-1" />
          <button type="submit" className="bg-blue-500 text-white px-3 py-1 rounded">Добавить</button>
        </form>
      )}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {services.map(s => (
          <div key={s.id} className="bg-white rounded shadow p-4">
            <h3 className="text-xl font-bold">{s.name}</h3>
            <p className="text-gray-600">{s.description}</p>
            <p className="text-lg font-semibold">{s.price} ₽</p>
            {isAdmin && <button onClick={() => handleDelete(s.id)} className="text-red-500 mt-2">Удалить</button>}
          </div>
        ))}
      </div>
    </div>
  );
};

export default ServicesPage;