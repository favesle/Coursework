import { Outlet, Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const Layout = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <div className="flex h-screen bg-gradient-to-br from-pink-50 to-blue-50">
      {/* Sidebar */}
      <aside className="w-64 bg-white shadow-md">
        <div className="p-4 text-xl font-bold border-b border-pink-100">Hotel System</div>
        <nav className="p-4">
          <ul className="space-y-2">
            <li><Link to="/dashboard" className="block py-2 px-3 rounded hover:bg-pink-100 transition">Главная</Link></li>
            <li><Link to="/clients" className="block py-2 px-3 rounded hover:bg-pink-100 transition">Клиенты</Link></li>
            <li><Link to="/rooms" className="block py-2 px-3 rounded hover:bg-pink-100 transition">Номера</Link></li>
            <li><Link to="/bookings" className="block py-2 px-3 rounded hover:bg-pink-100 transition">Бронирования</Link></li>
            <li><Link to="/stays" className="block py-2 px-3 rounded hover:bg-pink-100 transition">Проживания</Link></li>
            <li><Link to="/services" className="block py-2 px-3 rounded hover:bg-pink-100 transition">Услуги</Link></li>
            <li><Link to="/payments" className="block py-2 px-3 rounded hover:bg-pink-100 transition">Платежи</Link></li>
            <li><Link to="/reports" className="block py-2 px-3 rounded hover:bg-pink-100 transition">Отчёты</Link></li>
          </ul>
        </nav>
      </aside>

      {/* Main content */}
      <div className="flex-1 flex flex-col">
        <header className="bg-white shadow p-4 flex justify-between items-center border-b-2 border-blue-200">
          <span>Добро пожаловать, {user?.username} ({user?.role})</span>
          <button onClick={handleLogout} className="text-pink-500 hover:text-pink-700">Выйти</button>
        </header>
        <main className="p-6 overflow-auto">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default Layout;
