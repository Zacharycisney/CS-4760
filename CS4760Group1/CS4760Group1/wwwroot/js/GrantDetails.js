document.addEventListener('DOMContentLoaded', function () {
    // Getting each combo box by its ID
    const a1Budget = document.getElementById('A1Budget'); // Weight 3
    const a1Support = document.getElementById('A1Support'); // Weight 3
    const a2UniversityProgramReputation = document.getElementById('A2UniversityProgramReputation'); // Weight 5
    const a2InnovativeTeaching = document.getElementById('A2InnovativeTeaching'); // Weight 5
    const a2CommunityEducationalEngagement = document.getElementById('A2CommunityEducationalEngagement'); // Weight 5
    const a3ProcedureMethods = document.getElementById('A3ProcedureMethods'); // Weight 1
    const a3TimelineBudgetDescription = document.getElementById('A3TimelineBudgetDescription'); // Weight 1
    const a3EvaluationAndDissemination = document.getElementById('A3EvaluationAndDissemination'); // Weight 3
    const a3EvidentialDocumentation = document.getElementById('A3EvidentialDocumentation'); // Weight 1

    const calcBtn = document.getElementById('calcBtn');

    calcBtn.addEventListener('click', function () {
        // Calculate the total score as a decimal
        const totalScore =
            ((parseFloat(a1Budget.value) * 3) +
                (parseFloat(a1Support.value) * 3) +
                (parseFloat(a2UniversityProgramReputation.value) * 5) +
                (parseFloat(a2InnovativeTeaching.value) * 5) +
                (parseFloat(a2CommunityEducationalEngagement.value) * 5) +
                (parseFloat(a3ProcedureMethods.value) * 1) +
                (parseFloat(a3TimelineBudgetDescription.value) * 1) +
                (parseFloat(a3EvaluationAndDissemination.value) * 3) +
                (parseFloat(a3EvidentialDocumentation.value) * 1)) / 54;

        // Store the raw decimal score (e.g., 0.7556)
        document.getElementById('FinalScore').value = totalScore.toFixed(4);

        // Display the percentage in the UI (e.g., 75.56%)
        const percentageScore = (totalScore * 100).toFixed(2);
        document.getElementById('calc-score').textContent = percentageScore + '%';

        // Toggle visibility to show the calculated score
        document.getElementById('grant-score').style.display = 'block';
        document.getElementById('grant-review-grading').style.display = 'none';
    });

});
