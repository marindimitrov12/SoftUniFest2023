import {useState} from 'react'
import {Link,useNavigate}from 'react-router-dom'
import { login } from '../services/clientService'
import { useUserContext } from '../context/UserContext';

export default function Login(){
    const[loginFormData,setLoginFormData]=useState({ email: "", password: "" });
    const navigate=useNavigate();
    const {userLogin}=useUserContext();
    const handleSubmit=async(e)=>{
        e.preventDefault()
        await onSubmit()
    }
    
    const onSubmit = async () => {
        login(loginFormData.email,loginFormData.password)
      .then((res)=>{
        console.log(res);
        userLogin(res);
        navigate("/Home");
      })
     };
    const handleChange=(e)=>{
        const { name, value } = e.target
        setLoginFormData(prev => ({
            ...prev,
            [name]: value
        }))
    }
    return (<div className="login-container">
    <h1>Sign in to your account</h1>
    <form onSubmit={handleSubmit}  className="login-form">
        <input
            name="email"
            onChange={handleChange}
            type="email"
            placeholder="Email address"
            value={loginFormData.email}
        />
        <input
            name="password"
            onChange={handleChange}
            type="password"
            placeholder="Password"
            value={loginFormData.password}
        />
        <button>Log in</button>
        <Link className='login-link' to="/">Back to Register</Link>
    </form>
</div>
)
}