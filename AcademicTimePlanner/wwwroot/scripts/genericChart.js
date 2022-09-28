export function generatePieChart(chartId, chartType, dataToDisplay, labelsToDisplay, chartTitle){
    var ctx = document.getElementById(chartId);
    new Chart(ctx, {
        type: chartType,
        data: {
            labels: labelsToDisplay,

            datasets: [{
                label: 'Zahlen',
                data: dataToDisplay,
                backgroundColor: 'rgba(255,0,0,0.2)',
                borderColor: [
                    'rgba(0,0,255,1)',
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });
}