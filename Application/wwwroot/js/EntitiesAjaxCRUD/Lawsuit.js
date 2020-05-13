//returning lawsuits for admin review
$.ajax({
    url: '/Lawsuit/ListLawsuitsAdmin',
    type: "GET",
    success: function(data){
        $("#parnica").html(data);
    },
    error: function(err){
        console.log(err);
    }
})

//appending ListLawuits partial view to LawsuitManagement view
$.ajax({
    url: '/Lawsuit/ListLawsuits',
    type: "GET",
    success: function(data){
        $("#lawsuitManage").html(data);
    },
    error: function(err){
        console.log(err);
    }
})

//appending CalendarLawsuit partial view to LawsuitManagement view
$.ajax({
    url: '/User/CalendarLawsuits',
    type: "GET",
    success: function(data){
        $("#calen").html(data);
    },
    error: function(err){
        console.log(err);
    }
})

//sorting when <th> is clicked in the table
function sortLawsuit(sortOption)
{
    $.ajax({
        url: '/Lawsuit/ListLawsuits',
        type: "GET",
        data: { sortOrder: sortOption },
        success: function(data){
            $("#lawsuitManage").html(data);
        },
        error: function(err){
            console.log(err);
        }
    })
}

//validation and lawsuit insertion into the database 
function insertLawsuit()
{
    var errors = lawsuitInsertValidation();

    if (errors != null && errors.length > 0)
    {
        alert(errors);
        return;
    }

    var date = $("#datepicker").val();
    var time = $('#timepicker').val();
    var dateArray = date.split("-");
    var timeArray = time.split(":");

    var datetime = moment({ year: dateArray[0], month: dateArray[1] - 1, day: dateArray[2], 
        hour: timeArray[0], minute: timeArray[1] }).format();
        
    var lawyers = $(".lawsuitLawyers").val();

    $(".insertedLawsuit").hide();

    var data = {
        dateTimeOfEvent: datetime,
        processId: $("#lawsuitProcessId").val(),
        note: $("#lawsuitNote").val(),
        courtroomNumber: $("#lawsuitCourtroom").val(),
        location: $(".lawsuitLocation").children("option:selected").val(),
        judge: $(".lawsuitJudge").children("option:selected").val(),
        courtType: $(".lawsuitCourttype").children("option:selected").val(),
        prosecutor: $(".lawsuitProsecutor").children("option:selected").val(),
        defendant: $(".lawsuitDefendant").children("option:selected").val(),
        typeOfProcess: $(".lawsuitProcessType").children("option:selected").val(),
    };
    
    $.ajax({
        type: "POST",
        url: "/Lawsuit/CreateLawsuit",
        data: { model : data, lawyers: lawyers },
        success: function(data){
            if (data != null)
            {
                var date = new Date(Date.parse(data.dateTimeOfEvent)).toLocaleString('sr-Latn-RS');
                lawsuit = "<tr><td><span class="+data.id+" contenteditable>"+date+"</span></td><td>"+
                        "<span class="+data.id+">"+locationSelectionFill(data.locations, data.id)+"</span></td><td>"+
                        "<span class="+data.id+">"+judgeSelectionFill(data.contacts, data.id)+"</span></td><td>"+
                        "<span class="+data.id+">"+courtTypeSelectionFill(data.courtTypes, data.id)+"</span></td><td>"+
                        "<span class="+data.id+" contenteditable>"+data.processId+"</span></td><td>"+
                        "<span class="+data.id+" contenteditable>"+data.courtroomNumber+"</span></td><td>"+
                        "<span class="+data.id+">"+contactSelectionFill(data.contacts, data.id, true)+"</span></td><td>"+
                        "<span class="+data.id+">"+contactSelectionFill(data.contacts, data.id, false)+"</span></td><td>"+
                        "<span class="+data.id+" contenteditable>"+data.note+"</span></td><td>"+
                        "<span class="+data.id+">"+processTypeSelectionFill(data.processes, data.id)+"</span></td><td>"+
                        "<button type='button' class='btn btn-outline-primary waves-effect'"+
                        "onclick='changeLawsuit("+data.id+")'>Izmeni</button></td>"+
                        "<td><button type='button' class='btn btn-danger waves-effect'"+
                        "onclick='deleteLawsuit("+data.id+")'>Izbrisi</button></td></tr>";

                $(".lawsuitTable").prepend(lawsuit);
            }
            alert("Uspesno dodata parnica");
        },
        error: function(req, status, error){
            alert(error);
        }
    });
}

function deleteLawsuit(id)
{
    var result = confirm("Da li ste sigurni da zelite da obrisete?");
    if (result)
    {
        $.ajax({
            type: "post",
            url: "/Lawsuit/DeleteLawsuit",
            data: { id: id },
            success: function(data){
                $("#lawsuitManage").html(data);
            },
            error: function(req, status, error){
                alert(error);
            }
        });
    }
}

function changeLawsuit(id)
{
    var lawsuit = $("."+id);
    var lawsuitSelectedFields = $("."+id+">option:selected");

    var datetime = lawsuit.eq(0).html();
    var splitSpaces = datetime.split(" ");
    var dateArray = splitSpaces[0].split(".");
    var timeArray = splitSpaces[1].split(":");

    var dt = moment({ year: dateArray[2], month: dateArray[1] - 1, day: dateArray[0], 
        hour: timeArray[0], minute: timeArray[1] }).format();

    var processId = lawsuit.eq(4).html(); 
    var courtroomNum = lawsuit.eq(5).html();
    var note = lawsuit.eq(8).html();
    
    changeLawsuitValidation(processId, courtroomNum, note);

    var data = {
        id: id,
        dateTimeOfEvent: dt,
        location: lawsuitSelectedFields.eq(0).val(),
        judge: lawsuitSelectedFields.eq(1).val(),
        courtType: lawsuitSelectedFields.eq(2).val(),
        processId: processId,
        courtroomNumber: courtroomNum,
        note: note,
        prosecutor: lawsuitSelectedFields.eq(3).val(),
        defendant: lawsuitSelectedFields.eq(4).val(),
        typeOfProcess: lawsuitSelectedFields.eq(5).val(),
    };

    $.ajax({
        type: "POST",
        url: "/Lawsuit/EditLawsuit",
        data: data,
        success: function(){
            alert("Uspesna izmena");
        },
        error: function(req, status, error){
            alert(error);
        }
    });
}

//select tags for lawsuit filling
function locationSelectionFill(locations, lawsuitId)
{
    var selection = "<select class="+lawsuitId+">";
    $.each(locations, function(){
        if (this.id == $(".lawsuitLocation").children("option:selected").val())
        {
            selection += "<option value="+this.id+" selected>"+this.cityName+"</option>";
        }
        else
        {
            selection += "<option value="+this.id+">"+this.cityName+"</option>";
        }
    });

    return selection;
}

function judgeSelectionFill(contacts, lawsuitId)
{
    var selection = "<select class="+lawsuitId+">";

    $.each(contacts, function(){
        if (this.legalPerson)
        {
            if (this.id == $(".lawsuitJudge").children("option:selected").val())
            {
                selection += "<option value="+this.id+" selected>"+this.name+"</option>";
            }
            else
            {
                selection += "<option value="+this.id+">"+this.name+"</option>";
            }
        }
    });

    return selection;
}

function contactSelectionFill(contacts, lawsuitId, prosecutor)
{
    var selection = "<select class="+lawsuitId+">";

    $.each(contacts, function(){
        if(prosecutor)
        {
            if (this.id == $(".lawsuitProsecutor").children("option:selected").val())
            {
                selection += "<option value="+this.id+" selected>"+this.name+"</option>";
            }
            else
            {
                selection += "<option value="+this.id+">"+this.name+"</option>";
            }
        }
        else
        {
            if (this.id == $(".lawsuitDefendant").children("option:selected").val())
            {
                selection += "<option value="+this.id+" selected>"+this.name+"</option>";
            }
            else
            {
                selection += "<option value="+this.id+">"+this.name+"</option>";
            }
        }
        
    });

    return selection;
}

function courtTypeSelectionFill(courtTypes, lawsuitId)
{
    var selection = "<select class="+lawsuitId+">";
    $.each(courtTypes, function(index, value){
        if (index == $(".lawsuitCourttype").children("option:selected").val())
        {
            selection += "<option value="+index+" selected>"+value+"</option>";
        }
        else
        {
            selection += "<option value="+index+">"+value+"</option>";
        }
    });

    return selection;
}

function processTypeSelectionFill(processTypes, lawsuitId)
{
    var selection = "<select class="+lawsuitId+">";
    $.each(processTypes, function(){
        if (this.id == $(".lawsuitProcessType").children("option:selected").val())
        {
            selection += "<option value="+this.id+" selected>"+this.name+"</option>";
        }
        else
        {
            selection += "<option value="+this.id+">"+this.name+"</option>";
        }
    });

    return selection;
}

//Validation for insertion and updating
function lawsuitInsertValidation()
{
    var errors = [];

    if ($("#datepicker").val() == '')
    {
        errors.push("Morate uneti datum parnice");
        return errors;
    }
    if ($("#lawsuitProcessId").val() == '')
    {
        errors.push("Morate uneti identifikator parnice");
        return errors;
    }
    if ($("#lawsuitCourtroom").val() == '' || $("#lawsuitCourtroom").val().length > 5)
    {
        errors.push("Morate uneti broj sudnice ili broj sudnice ima vise od 5 karaktera");
        return errors;
    }
    if ($(".lawsuitLocation").children("option:selected").html() == "Izaberite lokaciju" ||
            $(".lawsuitJudge").children("option:selected").html() == "Izaberite sudiju")
    {
        errors.push("Niste izabrali sve stavke parnice");
        return errors;
    }
    if ($(".lawsuitProsecutor").children("option:selected").html() == "Izaberite tuzioca" || $(".lawsuitDefendant").children("option:selected").html() == "Izaberite tuzenika" ||
            $(".lawsuitProcessType").children("option:selected").html() == "Izaberite postupak")
    {
        errors.push("Niste izabrali sve stavke parnice");
        return errors;
    }
    if ($(".lawsuitLawyers").val().length === 0)
    {
        errors.push("Niste izabrali nijednog advokata");
        return errors;
    }
}

function changeLawsuitValidation(processId, courtroomNum, note)
{
    if (processId.length == 0 || processId.length > 15)
    {
        alert("Nema identifikatora ili je premasen limit od 15 karaktera. Postavlja se na izmeni dok ne izvrsite izmenu");
        lawsuit.eq(4).html("Izmeni");
        return;
    }

    if (courtroomNum.length == 0 || courtroomNum.length > 5)
    {
        alert("Nema identifikatora ili je premasen limit od 15 karaktera. Postavlja se na -- dok ne izvrsite izmenu");
        lawsuit.eq(5).html("--");
        return;
    }

    if (note.length == 0 || note.length > 30)
    {
        alert("Nema napomene ili je premasen limit od 30 karaktera. Postavlja se na izmeni dok ne izvrsite izmenu");
        lawsuit.eq(8).html("Izmeni");
        return;
    }
}