document.addEventListener('DOMContentLoaded', function () {
    initializeSidebarLinks();
    initializeSearch();
});

function initializeSidebarLinks() {
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
                e.preventDefault();
                validateForm();
            });

            function validateForm() {
                var isValid = true;
                var nameField = document.getElementById('name');
                var yearField = document.getElementById('year');
                var TournamentError = document.getElementById('TournamentError');
                var YearError = document.getElementById('YearError');

                TournamentError.textContent = '';
                YearError.textContent = '';

                if (!/^[A-Za-z\s]+$/.test(nameField.value.trim())) {
                    TournamentError.textContent = 'Tournament name must contain only alphabetic characters.';
                    isValid = false;
                }

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

                if (isValid) {
                    var formData = new FormData(form);

                    fetch(form.action, {
                        method: 'POST',
                        body: formData
                    })
                        .then(response => response.json())
                        .then(data => {
                            if (data.success) {
                                window.location.href = '/Auth/AdminView';
                            } else {
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
}

function initializeSearch() {
    const searchForm = document.getElementById('seachform');
    const searchInput = document.getElementById('Sname');
    const tourTable = document.querySelector('.TourTable');

    searchForm.addEventListener('submit', function (e) {
        e.preventDefault(); // Prevent the form from submitting normally

        const searchTerm = searchInput.value.trim();

        fetch('/Admin/Search', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: new URLSearchParams({
                'name': searchTerm
            })
        })
            .then(response => {
                if (response.ok) {
                    return response.text(); // Expecting HTML content in response
                } else if (response.status === 404) {
                    return '<p>No results found.</p>'; // Handle no results case
                } else {
                    throw new Error('Search request failed.');
                }
            })
            .then(html => {
                tourTable.innerHTML = html; // Update the table with the new content
            })
            .catch(error => {
                console.error('Error during search:', error);
                tourTable.innerHTML = '<p>An error occurred while searching.</p>';
            });
    });
}
