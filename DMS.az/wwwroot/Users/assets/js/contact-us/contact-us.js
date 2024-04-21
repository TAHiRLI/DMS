document.getElementById("contact-submit").onclick = function (event) {
    event.preventDefault();
  
    var formFields = document.querySelectorAll('#contact input');
  
    var isFormValid = Array.from(formFields).every(function (field) {
      return field.value.trim() !== '';
    });
  
    if (isFormValid) {
      swal("Successfully!");
  
    } else {
      swal("Please fill in all fields!");
    }
  }; 