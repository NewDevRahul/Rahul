function submitForm(event) {
    event.preventDefault();
    
    var formData = new FormData();
    debugger

    formData.append('Id', $('#Id').val());
    formData.append('Username', $('#Username').val());
    formData.append('Password', $('#Password').val());
    formData.append('Fruits', $('#Fruits').val());
    formData.append('Gender', $("input[name='gender']:checked").val());

    //$("input[name='interests']:checked").each(function () {
    //    formData.append('Interests', $(this).val());
    //});

    var valuearray = $("input[name='interests']:checked").map(function () {
        return this.value;
    }).get().join(",");

    formData.append('InterestsString', valuearray);

    console.log(valuearray);
    
    // Append the profile image file if selected
    var profileImage = $('#profileImage')[0].files[0];
    if (profileImage) {
        formData.append('profileImage', profileImage);
    }

    console.log(formData);

    jQuery.ajax({
        url: createOreditURL,
        type: 'POST',
        contentType: false,
        processData: false,
        data: formData,
        success: function (reponse) {
            if (reponse) {
                debugger
                location.href = "/User";
            }
        },
        error: function (error) {
            console.error(error);
        }
    });

}

