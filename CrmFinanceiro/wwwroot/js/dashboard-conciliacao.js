document.addEventListener("DOMContentLoaded", function () {

    const dados = window.dadosDashboard;

    const ctxFluxo = document.getElementById('graficoFluxo').getContext('2d');

    new Chart(ctxFluxo, {
        type: 'bar',
        data: {
            labels: dadosFLuxo.Labels,
            datasets: [
                {
                    label: 'Entradas Projetadas',
                    data: dadosFLuxo.Entradas,
                    backgroundColor: '#198754',
                    borderRadius: 4
                },
                {
                    label: 'Saídas Projetadas',
                    data: dadosFLuxo.Saidas,
                    backgroundColor: '#dc3545',
                    borderRadius: 4
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { position: 'bottom' }
            },
            scales: {
                y: { beginAtZero: true }
            }
        }
    });

    const ctxParceiros = document.getElementById('graficoParceiros').getContext('2d');

    new Chart(ctxParceiros, {
        type: 'doughnut',
        data: {
            labels: dadosParceiros.Labels,
            datasets: [{
                data: dadosParceiros.Valores,
                backgroundColor: dadosParceiros.Cores,
                borderWidth: 0
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { position: 'bottom' }
            },
            cutout: '70%' // Deixa o gráfico mais fino e moderno
        }
    });
})