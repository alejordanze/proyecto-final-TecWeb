function logout(){
    if(sessionStorage.getItem("jwt")){
        sessionStorage.removeItem('jwt');
    }

    window.location.href = "/login/login.html"
}