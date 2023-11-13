import {useState} from 'react'
import { editProduct } from '../services/productService';
import { useParams,useNavigate ,Link} from 'react-router-dom';
import { useUserContext } from '../context/UserContext';
export default function EditOffer(){

    const [editFormData,setEditFormData]=useState({name:'',desc:'',price:''});
    const {id}=useParams();
    const navigate=useNavigate();
    const {user}=useUserContext();
    const handleSubmit=async (e)=>{
      e.preventDefault();
      await onSubmit();
    }
    const onSubmit=async()=>{
        editProduct(id,editFormData.name,editFormData.desc,editFormData.price,user.accessToken)
        .then((res)=>{
           console.log(res);
        })
        navigate("/companyHome")
    }
    const handleChange=(e)=>{
      const{name,value}=e.target;
    setEditFormData((prev)=>({
    ...prev,
    [name]:[value]
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
         <input
            name="price"
            onChange={handleChange}
            type="number"
            placeholder="Price"
            value={editFormData.price}
        />
        <button>Add</button>
        <Link className='login-link' to="/companyHome">Back to HomePage</Link>
    </form>
</div>)
}