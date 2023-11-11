import logo from './logo.svg';
import './App.css';
import { UserProvider } from './context/UserContext';
import { BrowserRouter,Routes, Route } from 'react-router-dom';
import ClientHome from './pages/ClientHome';
import Layout from './Components/Layout';
import Login from './pages/Login';
import Register from './pages/Register';
import CompanyHome from './pages/CompanyHome';
import CompanyProducts from './pages/CompanyProducts';
function App() {
  return (
    <UserProvider>
    <BrowserRouter >
    <Routes>
      <Route path="/" element={<Register/>}/>
      <Route path="/login" element={<Login/>}/>
      <Route path="/clientHome" element={<Layout/>}>
      <Route index element={<ClientHome/>}/>
      <Route path="/clientHome/:CompanyName/products" element={<CompanyProducts/>}/>
      </Route>
      <Route path="/companyHome"element={<Layout/>}>
        <Route index element={<CompanyHome/>}/>
      </Route>
    </Routes>
    </BrowserRouter>
    </UserProvider>
  );
}

export default App;
