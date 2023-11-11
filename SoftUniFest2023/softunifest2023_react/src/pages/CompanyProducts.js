import {useState,useEffect} from 'react'
import { getAllProducts } from '../services/productService';
import { useUserContext } from '../context/UserContext';
import { useParams } from 'react-router-dom';
import Product from '../Components/Product';
export default function CompanyProducts(){

    const [products,setProducts]=useState(null);
    const{user}=useUserContext();
    const comp=useParams();
   
    useEffect(()=>{
        getAllProducts(comp.CompanyName,user.accessToken)
        .then((res)=>{
           console.log(res);
           setProducts(res);
        })
    },[]);

    return (<>
     <section className="py-5">
            <div className="container px-4 px-lg-5 mt-5">
                <div className="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
    {products===null?<h1>Loading</h1>:products.map(p=><Product 
    Name={p.name}
    Desc={p.description}
    Price={p.price}
    key={p.id}/>)}
           </div>
            </div>
        </section>
    </>)
}