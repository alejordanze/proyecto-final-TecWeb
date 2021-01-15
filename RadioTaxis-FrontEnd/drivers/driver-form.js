const queryparams = window.location.search;
const ids = queryparams.split('&');
let temporalBusinessId = -1;
let temporalId = -1;

if(ids.length > 1){
    console.log("entra2");
    temporalBusinessId = ids[0].split("=")[1];
    temporalId = ids[1].split("=")[1];
} else {
    console.log("entra");
    if(ids.length == 1){
        temporalBusinessId = ids[0].split("=")[1];
    }
}

const businessId = temporalBusinessId != -1 ? temporalBusinessId : undefined;
const id = temporalId != -1 ? temporalId : undefined;

console.log(businessId, id);

const url = "https://localhost:5001/api/"
let driver = {}

function asignValues(){
    const fullName = document.getElementById("fullName");
    fullName.value = driver.fullName;

    const birthDate = document.getElementById("birthDate");
    const date = driver.birthDate.split("T")[0];
    driver.birthDate = date;
    birthDate.value = driver.birthDate;

    const gender = document.getElementById("gender");
    gender.value = driver.gender;

    const placa = document.getElementById("placa");
    placa.value = driver.placa;

    const ci = document.getElementById("ci");
    ci.value = driver.ci;

    const expedido = document.getElementById("expedido");
    expedido.value = driver.expedido;

    const categoria = document.getElementById("categoria");
    categoria.value = driver.categoria;
}

function getNewValues(){
    const fullName = document.getElementById("fullName");
    driver.fullName = fullName.value;

    driver.businessId = Number(businessId);

    const birthDate = document.getElementById("birthDate");
    driver.birthDate = birthDate.value;

    const gender = document.getElementById("gender");
    driver.gender = gender.value;

    const placa = document.getElementById("placa");
    driver.placa = placa.value;

    const expedido = document.getElementById("expedido");
    driver.expedido = expedido.value;

    const ci = document.getElementById("ci");
    driver.ci = Number(ci.value);

    const categoria = document.getElementById("categoria");
    driver.categoria = categoria.value;
}

function cancel(){
    window.location.href = `/business/business.html?id=${businessId}`;
}

window.addEventListener('load', (event) => {

    function getDriver(businessId, id) {
        fetch(`${url}business/${businessId}/drivers/${id}`, {
            headers: {
                'Authorization': `Bearer ${sessionStorage.getItem("jwt")}`
            }
        })
        .then(res => {
            return res.json();
        })
        .then(response => {
            driver = response;
            asignValues();
        })
        .catch(error => {
            throw new Error(`There was an error: ${error}`)
        })     
    }
    
    document.getElementById("form").addEventListener("submit", saveDriver);

    if(businessId && id){
        getDriver(businessId, id);
    }
});

function saveDriver(event){
    event.preventDefault()
    let method = "POST";
    let endpoint = `${url}business/${businessId}/drivers`
    getNewValues();

    if(businessId & id){
        method = "PUT";
        endpoint = `${url}business/${businessId}/drivers/${id}`
    }

    console.log(driver);

    fetch(endpoint, {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${sessionStorage.getItem('jwt')}`
        },
        method: method,
        body: JSON.stringify(driver),
    }).then(res => {
        return res.json();
    }).then(response => {
        console.log(response);
        window.location.href = `/business/business.html?id=${businessId}`;
    }).catch(error => {
        throw new Error(`We're sorry for the error: ${error}`)
    });
}
