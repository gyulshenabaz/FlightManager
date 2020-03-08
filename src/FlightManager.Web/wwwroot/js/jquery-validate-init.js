$.validator.addMethod("checkUniqueIdentifier", function(value, element) {
    // allow any non-whitespace characters as the host part
    return this.optional(element) || /^[0-9]{10}$/.test(value);
}, 'Моля въведете валидно ЕГН.');

$.validator.addMethod("checkPhoneNumber", function(value, element) {
    // allow any non-whitespace characters as the host part
    return this.optional(element) || /^([0-9\s\-]{7,})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$/.test(value);
}, 'Моля въведете валиден телефонен номер.');

var passengerFormValidator  = $('#passengerForm').validate({
    errorPlacement: function(label, element) {
        label.addClass('invalid-feedback');
        label.insertAfter(element);
    },
    rules: {
        firstName: {
            required: true,
            minlength: 2,
            maxlength: 12
        },
        middleName: {
            required: true,
            minlength: 2,
            maxlength: 12
        },
        lastName: {
            required: true,
            minlength: 2,
            maxlength: 12
        },
        uniqueIdentifier: {
            required: true,
            checkUniqueIdentifier: true
        },
        phoneNumber: {
            required: true,
            checkPhoneNumber: true
        },
        ticketType: {
            required: true
        },
        nationality: {
            required: true,
        }
    },
    messages: {
        firstName: {
            required: "Това поле е задължително",
            minlength: 'Това поле трябва да има поне 2 букви',
            maxlength: 'Моля въведете не повече от 12 букви'
        },
        middleName: {
            required: "Това поле е задължително",
            minlength: 'Това поле трябва да има поне 2 букви',
            maxlength: 'Моля въведете не повече от 12 букви'
        },
        lastName: {
            required: "Това поле е задължително",
            minlength: 'Това поле трябва да има поне 2 букви',
            maxlength: 'Моля въведете не повече от 12 букви'
        },
        uniqueIdentifier: {
            required: "Това поле е задължително",
        },
        phoneNumber: {
            required: "Това поле е задължително",
        }
    },
    submitHandler: function(form) {
        var firstName = $('#firstNameField').val();
        var middleName = $('#middleNameField').val();
        var lastName = $('#lastNameField').val();
        var uniqueIdentifier = $('#uniqueIdentifierField').val();
        var phoneNumber = $('#phoneNumberField').val();
        var ticketType = $("#ticketTypeField option:selected").text();
        var nationality = $("#nationalityField option:selected").text();

        passengersTable
            .row.add({
            "FirstName": firstName,
            "MiddleName": middleName,
            "LastName": lastName,
            "UniqueCitizenshipNumber": uniqueIdentifier,
            "PhoneNumber": phoneNumber,
            "TicketType": ticketType,
            "Nationality": nationality
        })
            .draw()
            .node();

        $('#passengerModal').modal('hide');
    }
});