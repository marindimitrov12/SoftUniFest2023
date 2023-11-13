import React from 'react'
import {Link}from 'react-router-dom'
import { useUserContext } from '../context/UserContext'
export default function Product(props){
    const{user}=useUserContext();
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
                             {user.role==='Client'&&<div className="text-center"><Link className="btn btn-outline-dark mt-auto" to={`/clientHome/${props.Name}/products`}>Buy</Link></div>}
                             {user.role==='Company'&&<div className="text-center"><Link className="btn btn-outline-dark mt-auto" to={`/companyHome/${props.Name}/products/${props.id}`}>Edit</Link></div>}
                         </div>
                   
                       
                     </div>
                     </div>
    </>
    )
}