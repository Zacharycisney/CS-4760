document.addEventListener('DOMContentLoaded', function () {

    var a1 = document.getElementById('A1'); //w 3 
    var a2 = document.getElementById('A2'); //w 5
    var procMethods = document.getElementById('ProcMethods'); //w 1
    var timeline = document.getElementById('Timeline'); //w 1
    var evalDissem = document.getElementById('EvalDissem'); //w 3
    var evidenceDoc = document.getElementById('EvidenceDoc'); //w 1

    

    calcBtn.addEventListener('click', function () {

        const scoreA1 = parseFloat(a1.value);
        const scoreA2 = parseFloat(a2.value);
        const scoreProcMethods = parseFloat(procMethods.value);
        const scoreTimeline = parseFloat(timeline.value);
        const scoreEvalDissem = parseFloat(evalDissem.value);
        const scoreEvidenceDoc = parseFloat(evidenceDoc.value);

        //Multiply review number by area weight and then add areas. Then divide by max area num (area weight * max score)

        //I don't understand what the equation is supposed to actually look like, so using place holder. 
        const totalScore = scoreA1 + scoreA2 + scoreProcMethods + scoreTimeline + scoreEvalDissem + scoreEvidenceDoc;

        document.getElementById('calc-score').textContent = totalScore;
        document.getElementById('FinalScore').value = totalScore;


        document.getElementById('grant-score').style.display = 'block';
        document.getElementById('grant-review-grading').style.display = 'none';
    });

});