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