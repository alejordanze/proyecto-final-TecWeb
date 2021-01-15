const url = "https://localhost:5001/api/"
function login(event){
    event.preventDefault();
    user = {
        email: event.currentTarget.email.value,
        password: event.currentTarget.password.value
    }

    console.log(user);
    fetch(`${url}auth/login`, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    }).then(res => {
        return res.json();
    }).then(response => {
        console.log(response);
        if(response.isSuccess){
            sessionStorage.setItem("jwt", response.message);
            window.location.href = "/business/businesses.html";
        } else{
            alert(`${response.message}`)
        }
        
        
    });
}

function showPassword(){
    var pwd = document.getElementById("password");
    if (pwd.type === "password") {
        pwd.type = "text";
    } else {
        pwd.type = "password";
    }
}


window.addEventListener('load', (event) => {

    document.getElementById("form").addEventListener("submit", login);

});