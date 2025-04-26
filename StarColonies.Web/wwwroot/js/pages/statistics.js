document.addEventListener('DOMContentLoaded', function() {
    const commonBarOptions = {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                display: true,
                position: 'top',
                align: 'center',
                labels: {
                    color: 'white',
                    boxWidth: 15,
                    padding: 10,
                    font: {
                        size: 11
                    }
                }
            },
            tooltip: {
                backgroundColor: 'rgba(0, 0, 0, 0.7)',
                titleFont: {
                    size: 12
                },
                bodyFont: {
                    size: 12
                },
                padding: 8
            }
        },
        layout: {
            padding: {
                top: 5,
                right: 5,
                bottom: 15,
                left: 5
            }
        },
        scales: {
            x: {
                ticks: {
                    color: 'white',
                    font: {
                        size: 12
                    },
                    maxRotation: 45,
                    minRotation: 45
                },
                grid: {
                    color: 'rgba(255, 255, 255, 0.1)'
                }
            },
            y: {
                ticks: {
                    color: 'white',
                    font: {
                        size: 12
                    }
                },
                grid: {
                    color: 'rgba(255, 255, 255, 0.1)'
                },
                suggestedMin: 0,
                suggestedMax: 10
            }
        }
    };

    const commonRadarOptions = {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                display: true,
                position: 'top',
                align: 'center',
                labels: {
                    color: 'white',
                    boxWidth: 15,
                    padding: 10,
                    font: {
                        size: 11
                    }
                }
            },
            tooltip: {
                backgroundColor: 'rgba(0, 0, 0, 0.7)',
                titleFont: {
                    size: 12
                },
                bodyFont: {
                    size: 12
                },
                padding: 8
            }
        },
        layout: {
            padding: {
                top: 5,
                right: 5,
                bottom: 5,
                left: 5
            }
        },
        scales: {
            r: {
                angleLines: {
                    color: 'rgba(255, 255, 255, 0.2)'
                },
                grid: {
                    color: 'rgba(255, 255, 255, 0.1)'
                },
                pointLabels: {
                    color: 'white',
                    font: {
                        size: 11
                    }
                },
                ticks: {
                    color: 'white',
                    backdropColor: 'transparent',
                    font: {
                        size: 10
                    }
                }
            }
        }
    };

    const pieOptions = {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                position: 'right',
                labels: {
                    color: 'white',
                    boxWidth: 15,
                    padding: 8,
                    font: {
                        size: 11
                    }
                }
            },
            tooltip: {
                backgroundColor: 'rgba(0, 0, 0, 0.7)',
                titleFont: {
                    size: 12
                },
                bodyFont: {
                    size: 12
                },
                padding: 8
            }
        }
    };

    const firstChartData = {
        labels: [
            'Nova Prime', 'Shadow League', 'Celestial Pact', 'Galactic Alliance', 'Stellar Federation',
            'Lunar Syndicate', 'Solar Dominion', 'Astral Union', 'Nebula Coalition', 'Andromeda Syndicate'
        ],
        datasets: [
            {
                label: 'Strengh',
                data: [80, 70, 85, 90, 65, 75, 88, 60, 78, 82],
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                pointBackgroundColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 2
            },
            {
                label: 'Stamina',
                data: [70, 85, 80, 60, 75, 90, 82, 65, 88, 80],
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                pointBackgroundColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 2
            }
        ]
    };

    const secondChartData = {
        labels: ['AK-47', 'Stamina Pack', 'Force Module'],
        datasets: [{
            label: "Purchase count",
            data: [55, 25, 10],
            backgroundColor: [
                'rgba(255, 159, 64, 0.6)',
                'rgba(153, 102, 255, 0.6)',
                'rgba(75, 192, 192, 0.6)',
            ],
            borderColor: [
                'rgba(255, 159, 64, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(75, 192, 192, 1)',
            ],
            borderWidth: 1
        }]
    };

    const thirdChartData = {
        labels: ['Abyssion', 'Gaia Nova', 'Glacius', 'Infernis', 'Nyx Prime', 'Lunaris', 'Solara'],
        datasets: [
            {
                label: 'Accomplished',
                data: [8, 7, 8, 6, 5, 9, 4],
                backgroundColor: 'rgba(54, 162, 235, 0.6)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            },
            {
                label: 'Failed',
                data: [6, 8, 7, 5, 4, 7, 6],
                backgroundColor: 'rgba(255, 99, 132, 0.6)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1
            }
        ]
    };
    
    const firstC = document.getElementById('firstChart').getContext('2d');
    const firstChart = new Chart(firstC, {
        type: 'radar',
        data: firstChartData,
        options: commonRadarOptions
    });

    const secondC = document.getElementById('secondChart').getContext('2d');
    const secondChart = new Chart(secondC, {
        type: 'doughnut',
        data: secondChartData,
        options: pieOptions
    });
    
    setTimeout(() => {
        const thirdC = document.getElementById('thirdChart').getContext('2d');
        const thirdChart = new Chart(thirdC, {
            type: 'bar',
            data: thirdChartData,
            options: {
                ...commonBarOptions,
                barPercentage: 0.7,
                categoryPercentage: 0.8,
                scales: {
                    ...commonBarOptions.scales,
                    y: {
                        ...commonBarOptions.scales.y,
                        beginAtZero: true,
                        suggestedMax: 10
                    }
                }
            }
        });
        
        function handleResize() {
            firstChart.resize();
            secondChart.resize();
            thirdChart.resize();
        }
        
        window.addEventListener('resize', handleResize);
        
        setTimeout(handleResize, 200);
    }, 50);
});