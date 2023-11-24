import {useState,useEffect} from 'react'
import {Link}from 'react-router-dom'
import { loadStripe } from '@stripe/stripe-js';
import { useUserContext } from '../context/UserContext'
import { getPriceId } from '../services/productService';
let stripePromise ;


const getStripe=async ()=>{
    if(!stripePromise){
        stripePromise = await loadStripe(process.env.REACT_APP_STRIPE_KEY);
    }
    return stripePromise;
};
export default function Product(props){
    const[error,setError]=useState(null);
    const[priceId,setPriceId]=useState(null);
    const user=localStorage.getItem('myContext');
    const role=localStorage.getItem('Role');
    console.log(user)
    
    useEffect(()=>{
        getPriceId(props.id,user)
        .then((res)=>{
          console.log(res.priceId);
          
          setPriceId(res.priceId);
        });
    },[]);
   const item={
    price:`${priceId}`,
    quantity:1
   };
   const checkoutOptions={
    lineItems:[item],
    mode:"payment",
    successUrl:`${window.location.origin}/clientHome/success`,
    cancelUrl:`${window.location.origin}/clientHome/cancel`,
   };
   const redirectToCheckout=async ()=>{
     const stripe=await getStripe();
     const {error}=await stripe.redirectToCheckout(checkoutOptions)
     if(error){
       setError(error.message);
     }
   };
   if(error){
     alert(error);
   }

    return(<>
      <div className="col mb-5">
    <div className="card h-100">
                         
                         <img className="card-img-top" src="https://dummyimage.com/450x300/dee2e6/6c757d.jpg" alt="..." />
                        
                         <div className="card-body p-4">
                             <div className="text-center">
                               
                                 <h5 className="fw-bolder">{props.Name}</h5>
                                 {`${props.Desc}`} 
                                 {`${props.Price}`}
                             </div>
                             
                         </div>
                         <div className="card-footer p-4 pt-0 border-top-0 bg-transparent">
                             {role==='Client'&&<div className="text-center"><Link className="btn btn-outline-dark mt-auto" to={`/clientHome/${props.Name}/products`}onClick={redirectToCheckout}>Buy</Link></div>}
                             {role==='Company'&&<div className="text-center"><Link className="btn btn-outline-dark mt-auto" to={`/companyHome/${props.Name}/products/${props.id}|${props.Name}`}>Edit</Link></div>}
                         </div>
                   
                       
                     </div>
                     </div>
    </>
    )
}