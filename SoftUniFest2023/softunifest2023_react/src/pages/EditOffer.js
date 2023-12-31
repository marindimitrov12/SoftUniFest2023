import {useState} from 'react'
import { editProduct } from '../services/productService';
import { useParams,useNavigate ,Link} from 'react-router-dom';
import { useUserContext } from '../context/UserContext';
import { editStripeProduct } from '../services/companyService';
export default function EditOffer(){

    const [editFormData,setEditFormData]=useState({name:'',desc:''});
    const {id}=useParams();
    const ids=id.split('|');
    
    const navigate=useNavigate();
    const {user}=useUserContext();
    const handleSubmit=async (e)=>{
      e.preventDefault();
      await onSubmit();
    }
  
    const onSubmit=async()=>{
        
        editStripeProduct(editFormData.name,ids[1],editFormData.desc,user.accessToken)
        .then((res)=>{
            console.log(res);
        })
       
        editProduct(ids[0],editFormData.name,editFormData.desc,editFormData.price,user.accessToken)
        .then((res)=>{
           console.log(res);
        })
        navigate("/companyHome")
    }
    const handleChange=(e)=>{
        console.log(editFormData.name)
      const{name,value}=e.target;
    setEditFormData((prev)=>({
    ...prev,
    [name]:value
    }))
    }
    return ( <div className="login-container">
    <h1>Edit Product</h1>
    <form onSubmit={handleSubmit}  className="login-form">
   
      
        <input
            name="name"
            onChange={handleChange}
            type="text"
            placeholder="Name"
            value={editFormData.name}
        />
       
         <input
            name="desc"
            onChange={handleChange}
            type="text"
            placeholder="Description"
            value={editFormData.desc}
        />
        
        <button>Add</button>
        <Link className='login-link' to="/companyHome">Back to HomePage</Link>
    </form>
</div>)
}