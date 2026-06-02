import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { useAuth } from '../context/AuthContext';

const PaymentsPage = () => {
  const [payments, setPayments] = useState([]);
  const [stays, setStays] = useState([]);
  const [form, setForm] = useState({ stayId: '', amount: '', paymentMethod: 'Card' });
  const { user } = useAuth();
  const canManage = user?.role === 'Admin' || user?.role === 'Manager';

  const fetchData = async () => {
    const [payRes, stayRes] = await Promise.all([api.get('/payments'), api.get('/stays')]);
    setPayments(payRes.data);
    setStays(stayRes.data);
  };
  useEffect(() => { fetchData(); }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    await api.post('/payments', { ...form, amount: +form.amount });
    fetchData();
    setForm({ stayId: '', amount: '', paymentMethod: 'Card' });
  };

  return (
    <div>
      <h1 className="text-2xl mb-4">Платежи</h1>
      {canManage && (
        <form onSubmit={handleSubmit} className="mb-6 flex flex-wrap gap-2 bg-white p-4 rounded shadow">
          <select value={form.stayId} onChange={e => setForm({...form, stayId: e.target.value})} className="border p-1 rounded" required>
            <option value="">Проживание</option>
            {stays.map(s => <option key={s.id} value={s.id}>{s.clientFullName} - {s.roomNumber}</option>)}
          </select>
          <input type="number" placeholder="Сумма" value={form.amount} onChange={e => setForm({...form, amount: e.target.value})} className="border p-1 rounded w-32" required />
          <select value={form.paymentMethod} onChange={e => setForm({...form, paymentMethod: e.target.value})} className="border p-1 rounded">
            <option value="Cash">Наличные</option><option value="Card">Карта</option><option value="Bank">Банковский перевод</option>
          </select>
          <button type="submit" className="bg-blue-500 text-white px-3 py-1 rounded">Записать платёж</button>
        </form>
      )}
      <table className="min-w-full bg-white rounded shadow">
        <thead className="bg-gray-200">
          <tr><th>ID</th><th>Проживание</th><th>Сумма</th><th>Метод</th><th>Дата</th></tr>
        </thead>
        <tbody>
          {payments.map(p => (
            <tr key={p.id} className="border-b">
              <td className="p-2">{p.id}</td>
              <td>{p.stayId}</td>
              <td>{p.amount} ₽</td>
              <td>{p.paymentMethod}</td>
              <td>{new Date(p.paymentDate).toLocaleDateString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default PaymentsPage;