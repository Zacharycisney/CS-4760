document.addEventListener("DOMContentLoaded", function () {
    const college_select = document.getElementById('collegeSelect');
    const department_select = document.getElementById('departmentSelect');
    const potential_members_table = document.querySelector('#potentialMembers tbody');
    const all_rows = Array.from(potential_members_table.querySelectorAll('tr'));

    // Filter user table based on selected college and department
    function filter_users() {
        const selected_college = college_select.options[college_select.selectedIndex].text.trim().toLowerCase(); 
        const selected_department = department_select.options[department_select.selectedIndex].text.trim().toLowerCase(); 

        all_rows.forEach(row => {
            const college = row.querySelector('td:nth-child(3)').innerText.trim().toLowerCase();
            const department = row.querySelector('td:nth-child(4)').innerText.trim().toLowerCase();

            let show_row = true;

            //console.log("Colleges: " + selected_college + " " + college + " " + "\nDepartments: " + selected_department + " " + department)

            // Filter by college
            if (selected_college && selected_college !== 'select a college' && selected_college !== college) {
                show_row = false;
            }

            // Filter by department
            if (selected_department && selected_department !== 'select a department' && selected_department !== department) {
                show_row = false;
            }

            // Show or hide the row based on the filters
            row.style.display = show_row ? '' : 'none';
        });
    }

    // Attach event listeners to the dropdowns
    college_select.addEventListener('change', filter_users);
    department_select.addEventListener('change', filter_users);
});
