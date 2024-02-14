function submitForm(event) {
    event.preventDefault();

    var form = document.getElementById("registrationForm");

    if (form.checkValidity()) {

        var formData = new FormData();
        formData.append('Id', $('#Id').val());
        formData.append('Username', $('#Username').val());
        formData.append('Password', $('#Password').val());
        formData.append('Fruits', $('#Fruits').val());
        formData.append('Gender', $("input[name='gender']:checked").val());
        formData.append('Address', $('#Address').val());
        formData.append('SelectedCountryId', $('#countryDropdown').val());
        formData.append('SelectedStateId', $('#stateDropdown').val());
        formData.append('SelectedCityId', $('#cityDropdown').val());
        formData.append('Date', new Date($('#Date').val()).toISOString());

        if ($("input[name=hobbies]:checked").length == 0) {
            $("input[name=hobbies]:first").attr("required", "required");
        }

        var valuearray = $("select#InterestsString").val();
        formData.append('InterestsString', valuearray)

        var hobbiesadd = $("input[name='hobbies']:checked").map(function () {
            return this.value;
        }).get().join(",");

        formData.append('Hobbies', hobbiesadd);

        // Append the profile image file if selected
        var profileImage = $('#profileImage')[0].files[0];
        if (profileImage) {
            formData.append('profileImage', profileImage);
        } else {
            formData.append('ImagePath', $('#currentImagePath').val());
        }

        jQuery.ajax({
            url: createOreditURL,
            type: 'POST',
            contentType: false,
            processData: false,
            data: formData,
            success: function (reponse) {
                if (reponse) {
                    location.href = "/User";
                }
            },
            error: function (error) {
                console.error(error);
            }
        });
    }
    else {
        form.reportValidity();
    }
}