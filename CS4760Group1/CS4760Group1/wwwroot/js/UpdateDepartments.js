document.getElementById("collegeSelect").addEventListener("change", function () {
    var collegeId = this.value;
    var departmentSelect = document.getElementById("departmentSelect");
    departmentSelect.innerHTML = '<option value="">Select a department</option>';

    if (collegeId) {
        fetch(`/Users/Create?handler=Departments&collegeId=${collegeId}`)
            .then(response => response.json())
            .then(data => {
                data.forEach(function (department) {
                    var option = document.createElement("option");
                    option.value = department.value;
                    option.text = department.text;
                    departmentSelect.add(option);
                });
            })
            .catch(error => console.error('Error fetching departments:', error));
    }
});
