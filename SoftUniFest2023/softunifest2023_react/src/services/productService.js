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