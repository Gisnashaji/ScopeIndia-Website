﻿const locations = {
    "India": {

        "Tamilnadu": ["Chennai", "Kanyakumari", "Tirunelveli"],
        "Kerala": ["Trivandram", "Kochi", "Palakad","Thrissur"]
    }
    

};

// Function to update the state dropdown based on the selected country
function updateStates() {
    const countrySelect = document.getElementById("country");
    const stateSelect = document.getElementById("state");
    const citySelect = document.getElementById("city");
    const selectedCountry = countrySelect.value;

    // Clear existing options
    stateSelect.innerHTML = '<option value="" disabled selected>Select State</option>';
    citySelect.innerHTML = '<option value="" disabled selected>Select City</option>';

    // Populate states based on the selected country
    if (selectedCountry in locations) {
        for (const state in locations[selectedCountry]) {
            const option = document.createElement("option");
            option.value = state;
            option.text = state;
            stateSelect.add(option);
        }
    }
}

// Function to update the city dropdown based on the selected state
function updateCities() {
    const countrySelect = document.getElementById("country");
    const stateSelect = document.getElementById("state");
    const citySelect = document.getElementById("city");
    const selectedCountry = countrySelect.value;
    const selectedState = stateSelect.value;

    // Clear existing options
    citySelect.innerHTML = '<option value="" disabled selected>Select City</option>';

    // Populate cities based on the selected state
    if (selectedCountry in locations && selectedState in locations[selectedCountry]) {
        for (const city of locations[selectedCountry][selectedState]) {
            const option = document.createElement("option");
            option.value = city;
            option.text = city;
            citySelect.add(option);
        }
    }
}