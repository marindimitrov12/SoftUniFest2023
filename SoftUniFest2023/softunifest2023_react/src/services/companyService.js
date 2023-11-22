const baseUrl = 'https://localhost:7257/';

export const registerCompany=async (name,email,password)=>{
    const response=await fetch(`${baseUrl}registerCompany?Name=${name}&Email=${email}&Password=${password}`,{
        method:'POST'
    })
    return await response.json();
}
export const loginCompany=async (email,password)=>{
    const response=await fetch(`${baseUrl}loginCompany?Email=${email}&Password=${password}`,
    {method:'POST'})
    return await response.json();
}
export const getAllCompanies=async (accessToken)=>{
    const response=await fetch(`${baseUrl}getAllVendors`,{
        method:'GET',
        headers:{
            'Authorization': `bearer ${accessToken}`
        }
    })
    return await response.json();
}
export const createStripeProduct=async (name,description,price,accessToken)=>{
const response=await fetch(`${baseUrl}createStripeProduct`,{
    method:'POST',
    headers:{
        'Authorization':`bearer ${accessToken}`,
        'Content-Type': 'application/json'
    },
    body:JSON.stringify({
        name:name,
        description:description,
        price:price
    }),
})
return await response.json();
}
export const editStripeProduct=async (name,oldName,description,accessToken)=>{
    const response=await fetch(`${baseUrl}updateStripeProduct`,{
     method:'PUT',
     headers:{
        
        'Content-Type': 'application/json'
     },
     body:JSON.stringify({
        name:name,
        oldName:oldName,
        description:description,
        price:0

     }),
    })
    return await response.json();
}