import { Link } from "react-router-dom";
const Cancel=()=>{
    return(<>
    <h1>Cancel</h1>
    <h2>Your payment was canceled!</h2>
    <Link to="/clientHome">Back</Link>
    </>)
}
export default Cancel;