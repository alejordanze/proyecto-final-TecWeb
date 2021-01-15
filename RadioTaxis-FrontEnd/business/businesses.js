
const url = "https://localhost:5001/api/"

function seeBusiness(id){
    window.location.href = `/business/business.html?id=${id}`;
}

function editBusiness(id){
    window.location.href = `/business/business-form.html?id=${id}`;
}

function deleteBusiness(id){
    if(confirm(`Esta seguro que desea eliminar el Business con Id: ${id}`)){
        fetch(`${url}business/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${sessionStorage.getItem('jwt')}`
            }
        })
        .then(res => {
            return res.json();
        }).then(response => {
            console.log(response);
            window.location.reload();
        }).catch(error => {
            throw new Error(`We're sorry error: ${error}`)
        });   
    }
}

function newBusiness(){
    window.location.href = "/business/business-form.html";
}

async function orderBy(parameter){

    let response = await fetch(`${url}business?orderBy=${parameter}`, {
        headers: {
            'Authorization': `Bearer ${sessionStorage.getItem('jwt')}`
        }
    });
    try {
        if (response.status === 200) {
            let data = await response.json();
            document.querySelector('body .businesses .container .row').innerHTML = data.map(function (business) {
                return '<div class="col"><div class="card" style="width: 18rem;">'+ `<img src="${business.logoUrl}" class="card-img-top"></img>` + '<div class="card-body">' + `<h5 class="card-title">${business.name}</h5>`+ '</div>' + '<ul class="list-group list-group-flush">'+ `<li class="list-group-item">Fecha de fundacion: ${ business.foundationDate.split('T')[0]}</li>` + `<li class="list-group-item">Latitud: ${business.latitude}</li>` + `<li class="list-group-item">Latitud: ${business.longitude}</li>` + `<button class="button view" type="button" onclick="seeBusiness(${business.id})">Ver</button> <button class="button edit" type="button" onclick="editBusiness(${business.id})">Editar</button> <button class="button delete" type="button" onclick="deleteBusiness(${business.id})">Eliminar</button>` + '</div></div>';
            }).join('')
        } else {
            throw new error(await response.text())
        }
    } catch (error) {
        console.error(error);
    }
}

window.addEventListener('load', (event) => {

    async function getBusinesss() {
        try{
            const request = await fetch(`${url}business`, {
                headers: {
                    'Authorization': `Bearer ${sessionStorage.getItem('jwt')}`
                }
            })
            const json = await request.json();
            document.querySelector('body .businesses .container .row').innerHTML = json.map(function (business) {
                return '<div class="col"><div class="card" style="width: 18rem;">'+ `<img src="${business.logoUrl}" class="card-img-top"></img>` + '<div class="card-body">' + `<h5 class="card-title">${business.name}</h5>`+ '</div>' + '<ul class="list-group list-group-flush">'+ `<li class="list-group-item">Fecha de fundacion: ${ business.foundationDate.split('T')[0]}</li>` + `<li class="list-group-item">Latitud: ${business.latitude}</li>` + `<li class="list-group-item">Latitud: ${business.longitude}</li>` + `<button class="button view" type="button" onclick="seeBusiness(${business.id})">Ver</button> <button class="button edit" type="button" onclick="editBusiness(${business.id})">Editar</button> <button class="button delete" type="button" onclick="deleteBusiness(${business.id})">Eliminar</button>` + '</div></div>';
            }).join('')
        }
        catch(error){
            console.log(error);
        }
    }
  

    getBusinesss();
});