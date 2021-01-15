const queryparams = window.location.search;
const businessId = queryparams.split('=')[1];
const url = "https://localhost:5001/api/"
let business = {}


function asignValues(){
    const name = document.getElementById("Title");
    name.innerHTML = business.name;

    const img = document.getElementById("logo");
    img.src = business.logoUrl;

    const foundationDate = document.getElementById("foundationDate");
    foundationDate.innerHTML = "Fecha de Fundación: " + business.foundationDate.split("T")[0];

    const latitude = document.getElementById("latitude");
    latitude.innerHTML = "Latitud: " + business.latitude;
    
    const longitude = document.getElementById("longitude");
    longitude.innerHTML = "Productor: " + business.longitude;

    // document.querySelector('body .data .drivers').innerHTML = business.drivers.length > 0 ? (business.drivers.map(function (drivers) {
    //     return '<ul class="drivers">' + '<li> Nombre:' + drivers.fullName + '</li><li> Fecha de Nacimiento: '+ drivers.birthDate.split('T')[0] +'</li><li>Genero: ' + drivers.gender + '</li>' + '</ul>';
    // }).join('')) : '<label class="label">No hay conductores asignados</label>'

}



window.addEventListener('load', (event) => {

    function getBusiness(id) {
        fetch(`${url}business/${id}`, {
            headers: {
                'Authorization': `Bearer ${sessionStorage.getItem('jwt')}`
            }
        })
        .then(res => {
            return res.json();
        })
        .then(response => {
            business = response;
            console.log(business);
            asignValues();
        })
        .catch(error => {
            throw new Error(`There was an error: ${error}`)
        })     
    }

    getBusiness(businessId);
});