import React, { useEffect, useState } from 'react';
import api from '../services/api';

const ReportsPage = () => {
  const [report, setReport] = useState(null);
  useEffect(() => {
    api.get('/reports/dashboard').then(res => setReport(res.data));
  }, []);

  if (!report) return <div>Загрузка...</div>;

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">Отчёты</h1>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        <div className="bg-white p-4 rounded shadow">
          <h2 className="text-xl mb-2">Статус номеров</h2>
          <ul>
            <li>Свободно: {report.availableRooms}</li>
            <li>Занято: {report.occupiedRooms}</li>
            <li>Уборка: {report.cleaningRooms}</li>
            <li>Ремонт: {report.maintenanceRooms}</li>
          </ul>
        </div>
        <div className="bg-white p-4 rounded shadow">
          <h2 className="text-xl mb-2">Доход</h2>
          <p className="text-3xl font-bold text-green-600">{report.totalRevenue} ₽</p>
        </div>
        <div className="bg-white p-4 rounded shadow col-span-2">
          <h2 className="text-xl mb-2">Динамика по месяцам</h2>
          <table className="min-w-full">
            <thead>
              <tr><th>Год-Месяц</th><th>Доход</th></tr>
            </thead>
            <tbody>
              {report.monthlyRevenue?.map(m => (
                <tr key={`${m.year}-${m.month}`}><td>{m.year}-{m.month}</td><td>{m.revenue} ₽</td></tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};

export default ReportsPage;