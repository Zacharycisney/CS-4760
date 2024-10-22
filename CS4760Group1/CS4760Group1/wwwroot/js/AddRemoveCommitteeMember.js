document.addEventListener("DOMContentLoaded", function () {
    // Get all the Add buttons
    const addButtons = document.querySelectorAll('.add-btn');

    // Loop through each button and attach a click event listener
    addButtons.forEach(button => {
        button.addEventListener('click', function () {
            // Get the table row containing the clicked button
            const row = this.closest('tr');

            // Capture first name, last name, college, and department
            const firstName = row.querySelector('td:nth-child(1)').innerText;
            const lastName = row.querySelector('td:nth-child(2)').innerText;
            const college = row.querySelector('td:nth-child(3)').innerText;
            const department = row.querySelector('td:nth-child(4)').innerText;

            // Create a new list item for the committee member
            const newListItem = document.createElement('li');
            newListItem.innerHTML = `
                ${firstName} ${lastName} - ${college} ${department} 
                <button class="btn btn-secondary btn-sm remove-btn ml-2">Remove</button>
            `;

            // Add padding between list items
            newListItem.style.padding = "2px 0";

            // Add the new list item to the committee member list at the top
            const committeeMemberList = document.getElementById('CommitteeMemberList');
            committeeMemberList.insertBefore(newListItem, committeeMemberList.firstChild);

            // Remove the user from the "Potential Members" table
            row.remove();

            // Attach event listener to the remove button
            newListItem.querySelector('.remove-btn').addEventListener('click', function () {
                // Re-add the user to the "Potential Members" table when removed from committee
                const potentialMembersTable = document.querySelector('#potentialMembers tbody');

                // Create a new row in the potential members table
                const newRow = document.createElement('tr');
                newRow.innerHTML = `
                    <td>${firstName}</td>
                    <td>${lastName}</td>
                    <td>${college}</td>
                    <td>${department}</td>
                    <td><button class="btn btn-primary add-btn">Add</button></td>
                `;

                // Add the new row back to the potential members table
                potentialMembersTable.appendChild(newRow);

                // Remove the member from the committee member list
                newListItem.remove();

                // Reattach the event listener to the newly added Add button
                newRow.querySelector('.add-btn').addEventListener('click', function () {
                    // Re-adding the member to the top list
                    const row = this.closest('tr');

                    const firstName = row.querySelector('td:nth-child(1)').innerText;
                    const lastName = row.querySelector('td:nth-child(2)').innerText;
                    const college = row.querySelector('td:nth-child(3)').innerText;
                    const department = row.querySelector('td:nth-child(4)').innerText;

                    const newListItem = document.createElement('li');
                    newListItem.innerHTML = `
                        ${firstName} ${lastName} - ${college} ${department} 
                        <button class="btn btn-secondary btn-sm remove-btn ml-2">Remove</button>
                    `;
                    committeeMemberList.insertBefore(newListItem, committeeMemberList.firstChild);

                    // Add padding to the list item
                    newListItem.style.padding = "2px 0";

                    row.remove();

                    // Attach event listener for the new Remove button
                    newListItem.querySelector('.remove-btn').addEventListener('click', function () {
                        // Call the same logic to add the user back to the potential members table
                        potentialMembersTable.appendChild(newRow);
                        newListItem.remove();
                    });
                });
            });
        });
    });
});
