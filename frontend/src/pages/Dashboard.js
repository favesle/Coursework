import React, { useEffect, useState } from 'react';
import api from '../services/api';

const Dashboard = () => {
  const [report, setReport] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    api.get('/reports/dashboard')
      .then(res => setReport(res.data))
      .catch(err => {
        console.error('Ошибка загрузки отчёта:', err);
        setError('Не удалось загрузить данные. Возможно, недостаточно прав.');
      })
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <div>Загрузка...</div>;
  if (error) return <div className="text-red-500 p-4 bg-red-50 rounded">{error}</div>;
  if (!report) return <div>Нет данных</div>;

  return (
    <div>
      <h1 className="text-2xl font-bold mb-6 text-pink-700">Панель управления</h1>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mb-6">
        <div className="bg-white p-4 rounded shadow border-t-4 border-pink-300">
          <h3 className="text-gray-500">Всего номеров</h3>
          <p className="text-2xl font-bold">{report.totalRooms}</p>
        </div>
        <div className="bg-white p-4 rounded shadow border-t-4 border-blue-300">
          <h3 className="text-gray-500">Свободно</h3>
          <p className="text-2xl font-bold text-green-600">{report.availableRooms}</p>
        </div>
        <div className="bg-white p-4 rounded shadow border-t-4 border-red-300">
          <h3 className="text-gray-500">Занято</h3>
          <p className="text-2xl font-bold text-red-600">{report.occupiedRooms}</p>
        </div>
        <div className="bg-white p-4 rounded shadow border-t-4 border-blue-300">
          <h3 className="text-gray-500">Общий доход</h3>
          <p className="text-2xl font-bold">{report.totalRevenue} ₽</p>
        </div>
      </div>
      <div className="bg-white p-4 rounded shadow border-t-4 border-pink-300">
        <h3 className="text-lg font-semibold mb-2">Активных бронирований: {report.activeBookings}</h3>
        <h3 className="text-lg font-semibold">Клиентов: {report.totalClients}</h3>
      </div>
    </div>
  );
};

export default Dashboard;