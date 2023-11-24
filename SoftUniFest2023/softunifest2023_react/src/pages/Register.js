import {useState} from 'react'
import {useUserContext} from '../context/UserContext'
import {Link,useNavigate}from 'react-router-dom'
import { register } from '../services/clientService'
import { registerCompany } from '../services/companyService'
export default function Register (){
    const[registerFormData,setRegisterFormData]=useState({ firstname:"",lastname:"",
    email: "", password: "" });
    const [registrationType, setRegistrationType] = useState('company');
    const { userLogin } = useUserContext();
    const navigate=useNavigate();
    const handleSubmit=async(e)=>{
        e.preventDefault()
        await onSubmit()
    }
   
    const onSubmit = async () => {
        if(registrationType==='client'){
            register(registerFormData.firstname,registerFormData.lastname,registerFormData.email,registerFormData.password)
            .then((res)=>{
              console.log(res);
              userLogin(res);
              localStorage.setItem('myContext',res.accessToken);
              localStorage.setItem('Role',res.role);
              navigate("/clientHome");
            });
        }
        else{
           registerCompany(registerFormData.firstname,registerFormData.email,registerFormData.password)
           .then((res)=>{
             console.log(res);
             userLogin(res);
             localStorage.setItem('myContext',res.accessToken);
             localStorage.setItem('Role',res.role);
             navigate("/companyHome")
           });
        }
       
     };
     const onCompanyClick = ()=>{
        setRegistrationType('company');
    }
    const onClientClick = () => {
        setRegistrationType('client');
    }
    const handleChange=(e)=>{
        const { name, value } = e.target
        setRegisterFormData(prev => ({
            ...prev,
            [name]: value
        }))
    }
    return ( <div className="login-container">
    <h1>Register</h1>
    <div className="inputWrapper">
                    <input type="radio" name="registrationType" id="company" onClick={onCompanyClick}/>
                    <label htmlFor="company">Company</label>
                    <input type="radio" name="registrationType" id="client" onClick={onClientClick}/>
                    <label htmlFor="client">Client</label>
                </div>
    <form onSubmit={handleSubmit} className="login-form">
       {registrationType==='client'&&<> <input
            name="firstname"
            onChange={handleChange}
            type="text"
            placeholder="FirstName"
            value={registerFormData.firstname}
        />
        <input
            name="lastname"
            onChange={handleChange}
            type="text"
            placeholder="LastName"
            value={registerFormData.lastname}
        /></>}
        {registrationType==='company'&&
        <input
        name="firstname"
        onChange={handleChange}
        type="text"
        placeholder="Name"
        value={registerFormData.firstname}
    />}
         <input
            name="email"
            onChange={handleChange}
            type="email"
            placeholder="Email"
            value={registerFormData.email}
        />
         <input
            name="password"
            onChange={handleChange}
            type="password"
            placeholder="Password"
            value={registerFormData.password}
        />
        
        <button>Register</button>
        <Link className="register-link"to='/login'>Alredy have account</Link>
    </form>
</div>)
}