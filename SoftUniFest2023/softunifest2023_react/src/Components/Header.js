import React from 'react'
import {Link}from 'react-router-dom'
import { useUserContext } from '../context/UserContext'
export default function Header (){

    const{userLogout}=useUserContext();
    const{user}=useUserContext();
    const handleClick=()=>{
      userLogout();
      localStorage.clear();
      
    }
    return (<>
     
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container px-4 px-lg-5">
                <a className="navbar-brand" href="#!">Start Bootstrap</a>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation"><span className="navbar-toggler-icon"></span></button>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0 ms-lg-4">
                        <li className="nav-item">
                            {user.role==="Company"&&<Link className="nav-link active" to="/companyHome">Home</Link>}
                            {user.role==="Client"&&<Link className="nav-link active" to="/clientHome">Home</Link>}
                            </li>
                        <li className="nav-item">
                            {user.role==="Client"&&<Link className="nav-link" to="/clientHome/About">About</Link>}
                            {user.role==="Company"&&<Link className="nav-link" to="/companyHome/About">About</Link>}
                        </li>
                        <li className="nav-item dropdown">
                            <a className="nav-link dropdown-toggle" id="navbarDropdown" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Shop</a>
                            <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                                {user.role==='Company'&&<li><a className="dropdown-item" ><Link to="/companyHome/addProduct">AddProduct</Link></a></li>}
                                <li><hr className="dropdown-divider" /></li>
                                <li><a className="dropdown-item" href="#!">Popular Items</a></li>
                                <li><a className="dropdown-item" href="#!">New Arrivals</a></li>
                            </ul>
                        </li>
                    </ul>
                    <form className="d-flex">
                        <button className="btn btn-outline-dark" type="submit">
                            <i className="bi-cart-fill me-1"></i>
                            Cart
                            <span className="badge bg-dark text-white ms-1 rounded-pill">0</span>
                        </button>
                    </form>
                    <div className='logout'>
                    <Link className='btn btn-outline-dark'to='/' onClick={handleClick}>Logout</Link>
                    </div>
                    
                </div>
            </div>
        </nav>
        <header className="bg-dark py-5">
            <div className="container px-4 px-lg-5 my-5">
                <div className="text-center text-white">
                   {user.role==='Client'&& <h1 className="display-4 fw-bolder">Shop in style</h1>}
                   {user.role==='Company'&& <h1 className="display-4 fw-bolder">Your offers</h1>}
                    <p className="lead fw-normal text-white-50 mb-0">With this shop hompeage template</p>
                </div>
            </div>
        </header>
    </>)
}