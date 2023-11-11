import React from 'react'
import {Link} from 'react-router-dom'
export default function Company(props){
    return(<>
    <div className="col mb-5">
    <div className="card h-100">
                         
                         <img className="card-img-top" src="https://dummyimage.com/450x300/dee2e6/6c757d.jpg" alt="..." />
                        
                         <div className="card-body p-4">
                             <div className="text-center">
                               
                                 <h5 className="fw-bolder">{props.Name}</h5>
                           
                                 {`${props.Email}`}
                             </div>
                             
                         </div>
                         <div className="card-footer p-4 pt-0 border-top-0 bg-transparent">
                             <div className="text-center"><Link className="btn btn-outline-dark mt-auto" to={`/clientHome/${props.Name}/products`}>View offers</Link></div>
                         </div>
                   
                       
                     </div>
                     </div>
    </>)
}