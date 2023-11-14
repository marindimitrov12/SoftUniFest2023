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
import AddOffer from './pages/AddOffer';
import EditOffer from './pages/EditOffer';
import AboutPage from './pages/AboutPage';
import 'bootstrap/dist/css/bootstrap.min.css';
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
      <Route path="/clientHome/About"element={<AboutPage/>}/>
      </Route>
    
      <Route path="/companyHome"element={<Layout/>}>
        <Route index element={<CompanyHome/>}/>
        <Route path="/companyHome/addProduct" element={<AddOffer/>}/>
        <Route path="/companyHome/:CompanyName/products/:id" element={<EditOffer/>}/>
        <Route path="/companyHome/About"element={<AboutPage/>}/>
      </Route>
    </Routes>
    </BrowserRouter>
    </UserProvider>
  );
}

export default App;
