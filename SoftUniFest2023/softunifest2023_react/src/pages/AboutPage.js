import React from 'react'
import bgImg from "../logo512.png"
import {Link} from'react-router-dom'
import { useUserContext } from '../context/UserContext'
export default function AboutPage(){
    const {user}=useUserContext();
    return(<>
   
    <div className="about-page-container">
            <img src={bgImg} className="about-hero-image" />
            <div className="about-page-content">
                <h1>The only HR system you can deploy at lightning speed ⚡</h1>
                <p>Sloneek is a modern HR system that contains everything you need to manage the entire journey of employees and freelancers. Save 20 hours per week on HR processes and operations.</p>
             
             
            </div>
            <div className="about-page-cta">
                <h2>Sloneek is also liked by your colleagues and friends!<br />We're not playing games. Our results are confirmed by hundreds of positive references from satisfied clients.</h2>
               {user.role==="Client"&&<Link className="btn btn-primary" to="/clientHome">Back</Link>}
               {user.role==="Company"&&<Link className="btn btn-primary" to="/companyHome">Back</Link>}
            </div>
        </div>
    </>)
}