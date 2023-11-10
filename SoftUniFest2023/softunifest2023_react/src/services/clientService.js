const baseUrl = 'https://localhost:7257/';

export const register=async (firstname,lastname,email,password)=>{
    const response=await fetch(`${baseUrl}registerClient?FirstName=${firstname}&LastName=${lastname}&Email=${email}&Password=${password}`,{
        method:'POST'
    })
    return await response.json();
}
export const login=async (email,password)=>{
    const response=await fetch(`${baseUrl}loginClient?Email=${email}&Password=${password}`,
    {method:'POST'})
    return await response.json();
}
