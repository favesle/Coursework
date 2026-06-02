import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7112/api',   // ← ваш порт 7112
  headers: { 'Content-Type': 'application/json' }
});

export default api;
