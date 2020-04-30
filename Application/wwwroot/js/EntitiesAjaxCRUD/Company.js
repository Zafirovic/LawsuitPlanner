$.ajax({
    url: '/Company/ListCompanies',
    type: "GET",
    success: function(data){
        $("#kompanija").html(data);
    },
    error: function(err){
        console.log(err);
    }
})

function insertCompany()
{
    var name = $(".companyName").val();
    var address = $(".companyAddress").val();

    if(name == "" || address == "")
    {
        alert("Potrebno je da unesete vrednost za sva polja");
        return;
    }

    var data = {
        name: name,
        address: address,
        companies: null
    };
    $(".insertedCompany").hide();

    $.ajax({
        type: "POST",
        url: "/Company/CreateCompany",
        data: data,
        success: function(model){
            company = "<tr><td>"+model.id+"</td><td><span class="+model.id+" contenteditable>"+model.name+"</span></td><td>"+
                   "<span class="+model.id+" contenteditable>"+model.address+"</span></td><td>"+
                   "<button type='button' class='btn btn-outline-primary waves-effect'"+
                   "onclick='changeCompany("+model.id+")'>Izmeni kompaniju</button></td>"+
                   "<td><button type='button' class='btn btn-danger waves-effect'"+
                   "onclick='deleteCompany("+model.id+")'>Izbrisi kompaniju</button></td></tr>";
            $(".companyTable").prepend(company);
        },
        error: function(req, status, error){
            alert(error);
        }
    });
}

function deleteCompany(id)
{
    var result = confirm("Da li ste sigurni da zelite da obrisete?");
    if (result)
    {
        $.ajax({
            type: "post",
            url: "/Company/DeleteCompany",
            data: { id: id },
            success: function(data){
                $("#kompanija").html(data);
            },
            error: function(req, status, error){
                alert(error);
            }
        });
    }
}

function changeCompany(id)
{
    var name = $("."+id).eq(0).html();
    var address = $("."+id).eq(1).html();
    if (name.length == 0)
    {
        if (address.length == 0)
        {
            alert("Polja ne smeju biti prazna. Postavljaju se na izmeni dok ne izvrsite promenu!");
            $("."+id).eq(0).html("Izmeni");
            $("."+id).eq(1).html("Izmeni");
            return;
        }
        alert("Polje ne sme ostati prazno. Postavlja se na izmeni dok ne izvrsite promenu!");
        $("."+id).eq(0).html("Izmeni");
        return;
    }
    if (address.length == 0)
    {
        if (name.length == 0)
        {
            alert("Polja ne smeju biti prazna. Postavljaju se na izmeni dok ne izvrsite promenu!");
            $("."+id).eq(0).html("Izmeni");
            $("."+id).eq(1).html("Izmeni");
            return;
        }
        alert("Polje ne sme ostati prazno. Postavlja se na izmeni dok ne izvrsite promenu!");
        $("."+id).eq(1).html("Izmeni");
        return;
    }

    var data = {
        name: name,
        address: address,
        id: id
    };
    
    $.ajax({
        type: "POST",
        url: "/Company/EditCompany",
        data: data,
        success: function(komp){
            if (komp == "")
                alert("Niste uneli nikakve izmene za datu kompaniju");
            else
                alert("Kompanija je uspesno izmenjena");
        },
        error: function(req, status, error){
            alert(error);
        }
    });
}
