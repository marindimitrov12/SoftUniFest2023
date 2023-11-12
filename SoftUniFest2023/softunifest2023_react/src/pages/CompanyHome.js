import {useState,useEffect} from 'react'
import Product from '../Components/Product';
import { useUserContext } from '../context/UserContext';
import { getAllProductsByCompanyId } from '../services/productService';
export default function CompanyHome(){
    const [products,setProducts]=useState(null);
    const {user}=useUserContext();
    console.log(user);
    useEffect(()=>{
        getAllProductsByCompanyId(user.id,user.accessToken)
      .then((res)=>{
        console.log(res);
        setProducts(res);
      })
    },[])

    return(<>
        <section className="py-5">
               <div className="container px-4 px-lg-5 mt-5">
                   <div className="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                      
                         
                       {products===null?<h1>Loading...</h1>:products.map(p=><Product 
                       Name={p.name}
                       Email={p.description}
                       Price={p.price}
                       key={p.id}/>)}
                      
                      
                   </div>
               </div>
           </section>
                       
       </>)
}