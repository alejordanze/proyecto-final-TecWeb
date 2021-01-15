const url = "https://localhost:5001/api/"

function validateFields(user){
    return user.email != '' && user.password != '' && user.confirmPassword != '';
}

async function register(event){
    event.preventDefault();
    let user = {    
        email: event.currentTarget.email.value,
        password: event.currentTarget.password.value,
        confirmPassword: event.currentTarget.confirmPassword.value,
    }

    console.log(user);

    if(user.password == user.confirmPassword && validateFields(user)){
        let response = await fetch(`${url}auth/user`, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        let result = await response.json();
        console.log(result);
        if(result.isSuccess){
            window.location.href = "/login/login.html"
        }
        else{
            alert(`${result.errors}`)
        }
    } else {
        alert("Existen campos vacios o las contraseñas no coinciden");
    }
}

function showPasswords(){
    var pwd = document.getElementById("password");
    var conf = document.getElementById("confirmPassword");
    if (pwd.type === "password" && conf.type == "password") {
        pwd.type = "text";
        conf.type = "text";
    } else {
        pwd.type = "password";
        conf.type = "password";
    }
}

window.addEventListener('load', (event) => {

    document.getElementById("form").addEventListener("submit", register);

});