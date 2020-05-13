$.ajax({
    url: '/Contact/ListContacts',
    type: "GET",
    success: function(data){
        $("#kontakt").html(data);
    },
    error: function(err){
        console.log(err);
    }
})

function searchContact(sortOrder)
{
    $.ajax({
        url: '/Contact/ListContacts',
        type: "GET",
        data: { sortOrder: sortOrder },
        success: function(data){
            $("#kontakt").html(data);
        },
        error: function(err){
            console.log(err);
        }
    })
}

function insertContact()
{
    var ime = $("#contactName").val();
    var prezime = $("#contactSurname").val();
    var phone1 = $("#contactPhone1").val();
    var phone2 = $("#contactPhone2").val();
    var noData = "Nema podataka";

    contactInsertValidation(ime, prezime, phone1, phone2);
   
    $(".insertedContact").hide();

    var data = {
        name: ime,
        surname: prezime,
        phone1: $("#contactPhone1").val() == "" ? "--" : phone1,
        phone2: $("#contactPhone2").val() == "" ? "--" : phone2,
        address: $("#contactAddress").val() == "" ? noData : $("#contactAddress").val(),
        email: $("#contactEmail").val() == "" ? noData : $("#contactEmail").val(),
        legalPerson: $("#contactCheck").is(':checked'),
        job: $("#contactJob").val() == "" ? noData : $("#contactJob").val(),
        companyId: $(".contactsCompany").children("option:selected").val(),
        contacts: null,
        companies: null
    };
    var comp = $(".contactsCompany").children("option:selected").text();
    
    $.ajax({
        type: "post",
        url: "/Contact/CreateContact",
        data: data,
        success: function(data){
            if (data != null)
            {
                var checked;
                if (data.legalPerson)
                {
                    checked = "checked";
                }
                else
                {
                    checked = "";
                }

                city = "<tr><td><span class="+data.id+" contenteditable>"+data.name+"</span></td><td>"+
                        "<span class="+data.id+" contenteditable>"+data.phone1+"</span></td><td>"+
                        "<span class="+data.id+" contenteditable>"+data.phone2+"</span></td><td>"+
                        "<span class="+data.id+" contenteditable>"+data.address+"</span></td><td>"+
                        "<span class="+data.id+" contenteditable>"+data.email+"</span></td><td>"+
                        "<span class="+data.id+" contenteditable>"+data.job+"</span></td><td>"+
                        "<span class="+data.id+">"+companySelectionFill(data.companies, comp, data.id)+"</span></td><td>"+
                        "<span class="+data.id+" contenteditable>Pravno lice</span><input type='checkbox'"+checked+" class="+data.id+"></input></td><td>"+
                        "<button type='button' class='btn btn-outline-primary waves-effect'"+
                        "onclick='changeContact("+data.id+")'>Izmeni</button></td>"+
                        "<td><button type='button' class='btn btn-danger waves-effect'"+
                        "onclick='deleteContact("+data.id+")'>Izbrisi</button></td></tr>";

                $(".contactTable").prepend(city);
            }
        },
        error: function(req, status, error){
            alert(error);
        }
    });
}

function deleteContact(id)
{
    var result = confirm("Da li ste sigurni da zelite da obrisete?");
    if (result)
    {
        $.ajax({
            type: "POST",
            url: "/Contact/DeleteContact",
            data: { id: id },
            success: function(data){
                $("#kontakt").html(data);
            },
            error: function(req, status, error){
                alert(error);
            }
        });
    }
}

function changeContact(id)
{
    var contact = $("."+id);
    var name = contact.eq(0).html();
    var phone1 = contact.eq(1).html();
    var phone2 = contact.eq(2).html();

    if (name.length == 0)
    {
        alert("Korisnik mora imati ime i prezime!");
        contact.eq(0).html("Izmeni");
        return;
    }

    if (phone1.length > 10 || phone2.length > 10)
    {
        alert("Broj telefona ne moze imati vise od 10 cifara");
        return;
    }

    var data = {
        id: id,
        name: name,
        phone1: phone1,
        phone2: phone2,
        address: contact.eq(3).html(),
        email: contact.eq(4).html(),
        job: contact.eq(5).html(),
        companyId: contact.find(":selected").val(),
        legalPerson: contact.eq(8).is(":checked"),
    };

    $.ajax({
        type: "POST",
        url: "/Contact/EditContact",
        data: data,
        success: function(){
            alert("Uspesna izmena");
        },
        error: function(req, status, error){
            alert(error);
        }
    });
}

function companySelectionFill(companies, selectedComp, contactId)
{
    var selection = "<select class="+contactId+">";

    if (selectedComp == "Izaberite kompaniju")
    {
        selection += "<option value="+this.id+" selected>Nepoznato</option>";
        $.each(companies, function(){
            selection += "<option value="+this.id+">"+this.name+"</option>";
        });
    }
    else
    {
        $.each(companies, function(){
            if (this.name == selectedComp)
            {
                selection += "<option value="+this.id+" selected>"+this.name+"</option>";
            }
            else
            {
                selection += "<option value="+this.id+">"+this.name+"</option>";
            }
        });
    }
    return selection;
}

function contactInsertValidation(ime, prezime, phone1, phone2)
{
    if(ime == "" || prezime == "")
    {
        alert("Morate uneti ime i prezime kontakta!");
        return;
    }

    if (phone1.length > 10 || phone2.length > 10)
    {
        alert("Broj telefona ne moze imati vise od 10 cifara!");
        return;
    }
    
}