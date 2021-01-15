const queryparams = window.location.search;
const businessId = queryparams.split('=')[1];
const url = "https://localhost:5001/api/"
let business = {}

function cancel(){
    window.location.href = "/business/businesses.html";
}

function asignValues(){
    const name = document.getElementById("name");
    name.value = business.name;

    const foundationDate = document.getElementById("foundationDate");
    const date = business.foundationDate.split("T")[0];
    business.foundationDate = date;
    foundationDate.value = business.foundationDate;

    const latitude = document.getElementById("latitude");
    latitude.value = business.latitude;

    const longitude = document.getElementById("longitude");
    longitude.value = business.longitude;

    const logo = document.getElementById("logo");
    logo.value = business.logoUrl;

    loadImage(business.logoUrl);
}

function loadImage(url){
    var _img = document.getElementById('logo-img');
    var newImg = new Image;
    newImg.onload = function() {
        _img.src = this.src;
    }
    newImg.src = url;
}

function getNewValues( ){
    const name = document.getElementById("name");
    business.name = name.value;

    const foundationDate = document.getElementById("foundationDate");
    business.foundationDate = foundationDate.value ;

    const latitude = document.getElementById("latitude");
    business.latitude = latitude.value;

    const longitude = document.getElementById("longitude");
    business.longitude = longitude.value;

    const logo = document.getElementById("logo");
    business.logoUrl = logo.value;
}

window.addEventListener('load', (event) => {

    document.getElementById("logo").onchange = function (){
        loadImage(this.value);
    }

    function getBusiness(id) {
        fetch(`${url}business/${id}`, {
            headers: {
                'Authorization': `Bearer ${sessionStorage.getItem('jwt')}`
            },
        })
        .then(res => {
            return res.json();
        })
        .then(response => {
            business = response;
            asignValues();
        })
        .catch(error => {
            throw new Error(`There was an error: ${error}`)
        })     
    }

    // const form = document.getElementById("form");
    // form.onsubmit = function(e){
    //     e.preventDefault();
    //     saveMovie();
    //     window.location.href = "/movies/movies.html"
    // }
    document.getElementById("form").addEventListener("submit", saveBusiness);

    if(businessId){
        getBusiness(businessId);
    }
});



function saveBusiness(event){
    event.preventDefault()
    let method = "POST";
    let endpoint = `${url}business`
    getNewValues();

    if(businessId){
        method = "PUT";
        endpoint = `${url}business/${business.id}`
    }

    console.log(business);

    fetch(endpoint, {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${sessionStorage.getItem('jwt')}`
        },
        method: method,
        body: JSON.stringify(business),
    }).then(res => {
        return res.json();
    }).then(response => {
        console.log(response);
        window.location.href = "/business/businesses.html";
    }).catch(error => {
        throw new Error(`We're sorry for the error: ${error}`)
    });
}

