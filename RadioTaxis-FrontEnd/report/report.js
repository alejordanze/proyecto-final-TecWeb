const url = "https://localhost:5001/api/";
data = [];

window.addEventListener('load', (event) => {

    async function getBusinesss() {
        try{
            const request = await fetch(`${url}business/report`, {
                headers: {
                    'Authorization': `Bearer ${sessionStorage.getItem('jwt')}`
                },
            })
            const json = await request.json();
            data = json;
            console.log(json);
            document.querySelector('body .businesses .container .row').innerHTML = json.map(function (business) {
                return '<div class="col"><div class="card" style="width: 28rem;">' + '<div class="card-body">' + `<h5 class="card-title">${business.name}</h5>`+ '</div>' + '<ul class="list-group list-group-flush">'+ `${business.drivers.map(function(category){ 
                    return `<li class="list-group-item">Categoria: ${category.category} - Cantidad: ${category.quantity} ${category.drivers.map(function (driver){ 
                        return `<ul class="list-group list-group-flush"><li class="list-group-item">Nombre: ${driver.fullName}</li>
                        <li class="list-group-item">Placa: ${driver.placa}</li>
                        <li class="list-group-item">Ci: ${driver.ci} ${driver.expedido}</li></ul>`
                    }).join('')}</li>`
                }).join('')}
                </ul>` + '</div></div>';}).join('')
               
        }
        catch(error){
            console.log(error);
        }
    }

    getBusinesss();
});
