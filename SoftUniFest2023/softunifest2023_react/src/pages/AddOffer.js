import {useState} from 'react'
import { createProduct } from '../services/productService';
import { useUserContext } from '../context/UserContext';
import { useNavigate } from 'react-router-dom';
import {Link}from 'react-router-dom';
import {createStripeProduct}from'../services/companyService'
export default function AddOffer(){
    const [addProductFormData,setAddProductFormData]=useState({name:'',desc:'',price:0});
    const {user}=useUserContext();
    
    const navigate=useNavigate();
    const handleSubmit=async(e)=>{
       e.preventDefault();
       await onSubmit();
    }
    const onSubmit=async()=>{
        createStripeProduct(addProductFormData.name,addProductFormData.desc,addProductFormData.price,user.accessToken);
        createProduct(user.id,addProductFormData.name,addProductFormData.desc,addProductFormData.price,user.accessToken);
        navigate("/companyHome");
    }
    const handleChange=(e)=>{
        const { name, value } = e.target
        setAddProductFormData(prev => ({
            ...prev,
            [name]: value
        }))
    }
    return(<>
    <div className="login-container">
    <h1>Add Product</h1>
    <form onSubmit={handleSubmit}  className="login-form">
   
      
        <input
            name="name"
            onChange={handleChange}
            type="text"
            placeholder="Name"
            value={addProductFormData.name}
        />
         <input
            name="desc"
            onChange={handleChange}
            type="text"
            placeholder="Description"
            value={addProductFormData.desc}
        />
         <input
            name="price"
            onChange={handleChange}
            type="number"
            placeholder="Price"
            value={addProductFormData.price}
        />
        <button>Add</button>
        <Link className='login-link' to="/companyHome">Back to HomePage</Link>
    </form>
</div>
    </>)
}