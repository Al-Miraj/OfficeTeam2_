import React from 'react'; 
import { Routes, Route, Navigate, BrowserRouter } from 'react-router-dom'; 
import Login from './components/Login'; 
import Calendar from './components/CalendarComponent'; 
import Layout from './Models/Layout';

const App: React.FC = () => {
  const isAuthenticated = localStorage.getItem("isAuthenticated") === "true"; 

  console.log("isAuthenticated:", isAuthenticated); // Dit toont de status van inloggen in de console


  /*return (
    <Routes>
      <Route 
        index
        element={!isAuthenticated ? <Login /> : <Navigate to="/calendar" />} 
      />
      
      <Route 
        path="/calendar" 
        element={isAuthenticated ? <Calendar /> : <Navigate to="/" />} 
      />
    </Routes>
  );*/
  return (
    <Routes>
        <Route index element={<Login />}></Route>
        <Route path="/calendar" element={<Calendar />}/>
      </Routes>
  )
};

export default App;
