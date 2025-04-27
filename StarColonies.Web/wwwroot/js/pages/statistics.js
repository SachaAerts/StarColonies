document.addEventListener('DOMContentLoaded', function() {
    const statistics = window.statisticsData;
    console.log(statistics.ItemsLabel);
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
                },
                onHover: function(event, legendItem, legend) {
                    event.native.target.style.cursor = 'pointer';
                },
                onLeave: function(event, legendItem, legend) {
                    event.native.target.style.cursor = 'default';
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
                },
                onHover: function(event, legendItem, legend) {
                    event.native.target.style.cursor = 'pointer';
                },
                onLeave: function(event, legendItem, legend) {
                    event.native.target.style.cursor = 'default';
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
                },
                onHover: function(event, legendItem, legend) {
                    event.native.target.style.cursor = 'pointer';
                },
                onLeave: function(event, legendItem, legend) {
                    event.native.target.style.cursor = 'default';
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
        labels: statistics.Top10ColonyLabels,
        datasets: [
            {
                label: 'Strengh',
                data: statistics.Top10ColonyStrength,
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                pointBackgroundColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 2
            },
            {
                label: 'Stamina',
                data: statistics.Top10ColonyStamina,
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                pointBackgroundColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 2
            }
        ]
    };

    const secondChartData = {
        labels: statistics.ItemsLabel,
        datasets: [{
            label: "Purchase count",
            data: statistics.NumberOfBuysPerItems,
            backgroundColor: [
                'rgba(255, 159, 64, 0.6)',   
                'rgba(153, 102, 255, 0.6)',  
                'rgba(75, 192, 192, 0.6)',  
                'rgba(255, 205, 86, 0.6)',   
                'rgba(201, 203, 207, 0.6)',  
                'rgba(54, 162, 235, 0.6)',   
                'rgba(255, 99, 132, 0.6)',   
                'rgba(100, 255, 218, 0.6)'   
            ],
            borderColor: [
                'rgba(255, 159, 64, 1)',     
                'rgba(153, 102, 255, 1)',    
                'rgba(75, 192, 192, 1)',     
                'rgba(255, 205, 86, 1)',     
                'rgba(201, 203, 207, 1)',    
                'rgba(54, 162, 235, 1)',     
                'rgba(255, 99, 132, 1)',     
                'rgba(100, 255, 218, 1)'     
            ],
            borderWidth: 1
        }]
    };

    const thirdChartData = {
        labels: statistics.PlanetsLabel,
        datasets: [
            {
                label: 'Accomplished',
                data: statistics.MissionsSucceed,
                backgroundColor: 'rgba(54, 162, 235, 0.6)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            },
            {
                label: 'Failed',
                data: statistics.MissionsFailed,
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