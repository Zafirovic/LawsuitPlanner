$.ajax({
    url: '/TypeOfProcess/ListTypeOfProcess',
    type: "GET",
    success: function(data){
        $("#tipP").html(data);
    },
    error: function(err){
        console.log(err);
    }
})

function insertTypeOfProcess()
{
    var process = $(".processInput").val();
    $(".insertedTypeOfProcess").hide();

    if(process == "")
    {
        alert("Potrebno je da unesete naziv postupka");
        return;
    }
    else if (process.length > 40)
    {
        alert("Tip postupka ne moze imati vise od 40 karaktera");
        return;
    }
    
    $.ajax({
        type: "POST",
        url: "/TypeOfProcess/CreateTypeOfProcess",
        data: { name: process },
        success: function(id){
            process = "<tr><td>"+id+"</td><td><span id="+id+" contenteditable>"+process+"</span></td><td>"+
                    "<button type='button' class='btn btn-outline-primary waves-effect'"+
                    "onclick='changeTypeOfProcess("+id+")'>Izmeni</button></td>"+
                    "<td><button type='button' class='btn btn-danger waves-effect'"+
                    "onclick='deleteTypeOfProcess("+id+")'>Izbrisi</button></td></tr>";
            $(".processTable").prepend(process);
        },
        error: function(req, status, error){
            alert(error);
        }
    });
}

function deleteTypeOfProcess(id)
{
    var result = confirm("Da li ste sigurni da zelite da obrisete?");
    if (result)
    {
        $.ajax({
            type: "post",
            url: "/TypeOfProcess/DeleteTypeOfProcess",
            data: { id: id },
            success: function(data){
                $("#tipP").html(data);
            },
            error: function(req, status, error){
                alert(error);
            }
        });
    }
}

function changeTypeOfProcess(id)
{
    var name = $("#"+id).html();
    if (name.length == 0)
    {
        alert("Polje ne sme ostati prazno. Postavlja se na izmeni dok ne izvrsite promenu");
        $("#"+id).html("Izmeni");
        return;
    }
    else if (name.length > 40)
    {
        alert("Tip postupka ne moze imati vise od 40 karaktera");
        return;
    }
    $.ajax({
        type: "post",
        url: "/TypeOfProcess/EditTypeOfProcess",
        data: { id: id, processName: name },
        success: function(updatedName){
            if (updatedName == "")
                alert("Niste izvrsili nikakve promene u datom polju!")
            else
                alert("Tip procesa je izmenjen u " + updatedName);
        },
        error: function(req, status, error){
            alert(error);
        }
    });
}