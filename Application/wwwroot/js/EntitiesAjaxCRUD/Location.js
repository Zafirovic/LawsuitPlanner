$.ajax({
    url: '/Location/ListLawsuitCities',
    type: "GET",
    success: function(data){
        $("#lokacija").html(data);
    },
    error: function(err){
        console.log(err);
    }
})

function searchCity(sortOrder)
{
    $.ajax({
        url: '/Location/ListLawsuitCities',
        type: "GET",
        data: { sortOrder: sortOrder},
        success: function(data){
            $("#lokacija").html(data);
        },
        error: function(err){
            console.log(err);
        }
    })
}

function insertCity()
{
    var city = $(".cityInput").val();
    $(".insertedCity").hide();

    //validating city inputs
    cityInsertValidation(city);

    $.ajax({
        type: "POST",
        url: "/Location/CreateLocation",
        data: { cityName: city },
        success: function(id){
            city = "<tr><td>"+id+"</td><td><span id="+id+" contenteditable>"+city+"</span></td><td>"+
                   "<button type='button' class='btn btn-outline-primary waves-effect'"+
                   "onclick='changeCity("+id+")'>Izmeni</button></td>"+
                   "<td><button type='button' class='btn btn-danger waves-effect'"+
                   "onclick='deleteCity("+id+")'>Izbrisi</button></td></tr>";
            $(".cityTable").prepend(city);
        },
        error: function(req, status, error){
            alert(error);
        }
    });
}

function deleteCity(id)
{
    var result = confirm("Da li ste sigurni da zelite da obrisete?");
    if (result)
    {
        $.ajax({
            type: "POST",
            url: "/Location/DeleteLocation",
            data: { id: id },
            success: function(data){
                $("#lokacija").html(data);
            },
            error: function(req, status, error){
                alert(error);
            }
        });
    }
}

function changeCity(id)
{
    var name = $("#"+id).html();

    //validating city inputs for update
    changeCityValidation(name);

    $.ajax({
        type: "post",
        url: "/Location/EditLocation",
        data: { id: id, cityName: name },
        success: function(updatedName){
            if (updatedName == "")
                alert("Niste izvrsili nikakve promene u datom polju!")
            else
                alert("Naziv grada je izmenjen u " + updatedName);
        },
        error: function(req, status, error){
            alert(error);
        }
    });
}

function cityInsertValidation(city)
{
    if (city == "")
    {
        alert("Potrebno je da unesete naziv grada");
        return;
    }
    else if (city.length > 40)
    {
        alert("Naziv grada ne moze imati vise od 40 karaktera");
        return;
    }
}

function changeCityValidation(name)
{
    if (name.length == 0)
    {
        alert("Polje ne sme ostati prazno. Postavlja se na izmeni dok ne izvrsite promenu");
        $("#"+id).html("Izmeni");
        return;
    }
    else if (name.length > 40)
    {
        alert("Naziv grada ne moze imati vise od 40 karaktera");
        return;
    }
}