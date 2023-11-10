import {useState} from 'react'
import {useUserContext} from '../context/UserContext'
import {Link,useNavigate}from 'react-router-dom'
import { register } from '../services/clientService'
export default function Register (){
    const[registerFormData,setRegisterFormData]=useState({ firstname:"",lastname:"",
    email: "", password: "" });
    const { userLogin } = useUserContext();
    const navigate=useNavigate();
    const handleSubmit=async(e)=>{
        e.preventDefault()
        await onSubmit()
    }
    
    const onSubmit = async () => {
        register(registerFormData.firstname,registerFormData.lastname,registerFormData.email,registerFormData.password)
      .then((res)=>{
        console.log(res);
        userLogin(res);
        navigate("/Home");
      })
     };
    const handleChange=(e)=>{
        const { name, value } = e.target
        setRegisterFormData(prev => ({
            ...prev,
            [name]: value
        }))
    }
    return ( <div className="login-container">
    <h1>Register</h1>
    <form onSubmit={handleSubmit} className="login-form">
        <input
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
        />
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