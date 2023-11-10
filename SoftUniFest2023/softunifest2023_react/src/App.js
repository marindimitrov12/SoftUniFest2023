import logo from './logo.svg';
import './App.css';
import { UserProvider } from './context/UserContext';
import { BrowserRouter,Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Layout from './Components/Layout';
import Login from './pages/Login';
import Register from './pages/Register';
function App() {
  return (
    <UserProvider>
    <BrowserRouter >
    <Routes>
      <Route path="/" element={<Register/>}/>
      <Route path="/login" element={<Login/>}/>
      <Route path="/Home" element={<Layout/>}>
      <Route index element={<Home/>}/>
      </Route>
    </Routes>
    </BrowserRouter>
    </UserProvider>
  );
}

export default App;
