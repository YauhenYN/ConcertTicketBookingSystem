import { useEffect } from "react";

export default function OAuthRedirect(){
    useEffect(() => {
        window.location = localStorage.getItem("lastLocation")
    }, [])
    return <></>
}