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