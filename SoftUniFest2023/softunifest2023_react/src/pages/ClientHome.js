import {useState,useEffect} from 'react'
import { getAllCompanies } from '../services/companyService';
import Company from '../Components/Company';
import { useUserContext } from '../context/UserContext';
export default function ClientHome(){

    const [companies,setCompanies]=useState(null);
    const user=localStorage.getItem('myContext');
    

    useEffect(()=>{
        getAllCompanies(user)
        .then((res)=>{
           console.log(res);
           setCompanies(res);
        })
    },[]);

    return (<>
     <section className="py-5">
            <div className="container px-4 px-lg-5 mt-5">
                <div className="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                   
                      
                    {companies===null?<h1>Loading...</h1>:companies.map(c=><Company 
                    Name={c.name}
                    Email={c.email}
                    key={c.id}/>)}
                   
                   
                </div>
            </div>
        </section>
                    
    </>)
}