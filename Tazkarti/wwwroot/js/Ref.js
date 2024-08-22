document.addEventListener('DOMContentLoaded', function () {
    var sidebarLinks = document.querySelectorAll('.sidebar-link');

    sidebarLinks.forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault();

            var target = this.getAttribute('data-target');
            loadContent(target);
        });
    });

    function loadContent(target) {
        var dynamicContainer = document.getElementById('dynamicContainer');

        fetch('/Admin/' + target)
            .then(response => response.text())
            .then(data => {
                dynamicContainer.innerHTML = data;
                initializeLoadedContent(); // Initialize the form after content is loaded
            })
            .catch(error => console.error('Error loading content:', error));
    }

    function initializeLoadedContent() {
        const form = document.getElementById('formm');
        if (form) {
            form.addEventListener('submit', function (e) {
                e.preventDefault(); // Prevent default form submission
                validateForm(); // Validate and submit via AJAX
            });

            function validateForm() {
                var isValid = true;
                var nameField = document.getElementById('name');
                var yearField = document.getElementById('year');
                var TournamentError = document.getElementById('TournamentError');
                var YearError = document.getElementById('YearError');

                // Reset previous error messages
                TournamentError.textContent = '';
                YearError.textContent = '';

                // Validate Tournament Name
                if (!/^[A-Za-z\s]+$/.test(nameField.value.trim())) {
                    TournamentError.textContent = 'Tournament name must contain only alphabetic characters.';
                    isValid = false;
                }

                // Validate Year
                var yearValue = yearField.value.trim();
                var yearPattern = /^\d{4}\/\d{4}$/;
                if (!yearPattern.test(yearValue)) {
                    YearError.textContent = 'Year must be in the format "YYYY/YYYY".';
                    isValid = false;
                } else {
                    var [startYear, endYear] = yearValue.split('/').map(Number);
                    var currentYear = new Date().getFullYear();
                    if (startYear < currentYear || endYear < currentYear) {
                        YearError.textContent = `Both years must not be less than ${currentYear}.`;
                        isValid = false;
                    } else if (startYear >= endYear) {
                        YearError.textContent = 'The start year must be less than the end year.';
                        isValid = false;
                    }
                }

                // If valid, submit the form via AJAX
                if (isValid) {
                    var formData = new FormData(form);

                    fetch(form.action, {
                        method: 'POST',
                        body: formData
                    })
                        .then(response => response.json())
                        .then(data => {
                            if (data.success) {
                                window.location.href = '/Admin?message=Tournament+has+been+saved+successfully';
                            } else {
                                // Display server-side validation errors
                                if (data.errors) {
                                    if (data.errors.Name) {
                                        TournamentError.textContent = data.errors.Name;
                                    }
                                    if (data.errors.Year) {
                                        YearError.textContent = data.errors.Year;
                                    }
                                }
                            }
                        })
                        .catch(error => {
                            console.error('Error:', error);
                            alert('An error occurred. Please try again.');
                        });
                }
            }
        } else {
            console.warn('Form not found.');
        }
    }
});
