const baseUrl = 'https://localhost:7257/';

export const getAllProducts=async (name,accessToken)=>{
    const response=await fetch(`${baseUrl}getByName?companyName=${name}`,{
        method:'GET',
        headers:{
            'Authorization': `bearer ${accessToken}`
        }
    })
    return await response.json();
}
export const getAllProductsByCompanyId=async (id,accessToken)=>{
    const response=await fetch(`${baseUrl}getAll?companyId=${id}`,{
        method:'GET',
        headers:{
            'Authorization': `bearer ${accessToken}`
        }
    })
    return await response.json();
}
export const createProduct=async(compId,name,desc,price,accessToken)=>{
    const response=await fetch(`${baseUrl}create?companyId=${compId}&Name=${name}&Description=${desc}&Price=${price}`,{
        method:'POST',
        headers:{
            'Authorization': `bearer ${accessToken}`
        }
    })
   
}
export const editProduct=async(prodId,name,desc,price,accessToken)=>{
    const response=await fetch(`${baseUrl}edit?productId=${prodId}&Name=${name}&Description=${desc}&Price=${price}`,{
        method:'PUT',
        headers:{
            'Authorization':`bearer ${accessToken}`
        }
    })
    return await response.json();

}
export const getPriceId=async(prodId,accessToken)=>{
    const response=await fetch(`${baseUrl}getPriceId?prodId=${prodId}`,{
        method:'GET',
        headers:{
            'Authorization':`bearer ${accessToken}`
        }
    })
    return await response.json();
}