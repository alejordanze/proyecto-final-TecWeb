const queryParameters = window.location.search;
const idBusiness = queryParameters.split('=')[1];
const urlAPI = "https://localhost:5001/api/"

function seeDriver(id){
    window.location.href = `/drivers/driver.html?businessId=${idBusiness}&id=${id}`;
}

function editDriver(id){
    window.location.href = `/drivers/driver-form.html?businessId=${idBusiness}&id=${id}`;
}

function deleteDriver(id){
    if(confirm(`Esta seguro que desea eliminar el Driver con Id: ${id}`)){
        fetch(`${url}business/${idBusiness}/drivers/${id}`, {
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

function newDriver(){
    window.location.href = `/drivers/driver-form.html?businessId=${idBusiness}`;
}

window.addEventListener('load', (event) => {

    async function getDrivers(businessId) {
        try{
            const request = await fetch(`${urlAPI}business/${businessId}/drivers`,
            {
                headers: {
                    'Authorization': `Bearer ${sessionStorage.getItem("jwt")}`
                }
            });
            
            const json = await request.json();
            console.log(json);
            document.querySelector('body .table .tbody').innerHTML = json.map(function (driver) {
                return '<tr class="driver">' + '<td>' + driver.fullName + '</td><td>'+ driver.birthDate.split('T')[0] +'</td><td>'+ driver.categoria +'</td><td>'+ driver.ci + ' ' + driver.expedido +'</td><td>'+ driver.placa +'</td><td>' + driver.gender + '</td>' + `<td> <button class="button edit" type="button" onclick="editDriver(${driver.id})">Editar</button> <button class="button delete" type="button" onclick="deleteDriver(${driver.id})">Eliminar</button></td>` + '</tr>';
            }).join('')
        }
        catch(error){
            console.log(error);
        }
    }
  

    getDrivers(idBusiness);
});