$(document).ready(() => {
    renderChartProject();
    renderChartEmployee();
    renderTableProjectDueday();
});

function renderChartProject() {
    var branchData = { "1": 0, "2": 0, "3": 0 }; // Assuming 1 = TPHCM, 2 = Ha Noi, 3 = Da Nang

    $.ajax({
        url: '/Project/getAllProject',
        type: 'GET',
        success: function (data) {
            console.log("Danh sach du lieu: ", data);
            if (data.length > 0) {
                data.forEach((item, index) => {
                    branchData[item.branch]++; // Counting projects per branch
                });

                // Call drawChart with the updated branch data
                drawChart(branchData);
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function drawChart(branchData) {
    google.charts.load("current", { packages: ["corechart"] });
    google.charts.setOnLoadCallback(function () {
        var data = google.visualization.arrayToDataTable([
            ['Branch', 'Number of Projects'],
            ['TPHCM', branchData["1"]],
            ['Ha Noi', branchData["2"]],
            ['Da Nang', branchData["3"]]
        ]);

        var options = {
            title: 'Projects by Branch',
            is3D: true,
            titleTextStyle: {
                color: '#1a73e8', // Change this to your desired color
                fontSize: 18,     // You can also change the font size if needed
                bold: true        // You can make the title bold
            },
            pieSliceText: 'value',

            slices: { 0: { offset: 0.1 }, 2: { offset: 0.1 } }, // Highlighting specific slices
            tooltip: { textStyle: { fontSize: 12 } },
            animation: {
                startup: true,
                duration: 1000,
                easing: 'out'
            }
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechartProject_3d'));
        chart.draw(data, options);
    });
}

function renderChartEmployee() {
    var addressData = {};

    $.ajax({
        url: "/Account/GetAllEmployee",
        type: 'GET',
        success: function (data) {
            console.log(data);
            data.map((item, index) => {
                if (!addressData[item.address]) {
                    addressData[item.address] = 0;
                }
                addressData[item.address]++;
            });

            drawChartEmployee(addressData);
        }
    });
}

function drawChartEmployee(addressData) {
    google.charts.load("current", { packages: ["corechart"] });
    google.charts.setOnLoadCallback(function () {
        var dataArray = [['Address', 'Number of Employees']];

        for (var address in addressData) {
            dataArray.push([address, addressData[address]]);
        }

        var data = google.visualization.arrayToDataTable(dataArray);

        var options = {
            title: 'Employee Distribution by Address',
            is3D: true,
            titleTextStyle: {
                color: '#1a73e8', // Change this to your desired color
                fontSize: 18,     // You can also change the font size if needed
                bold: true        // You can make the title bold
            },
            pieSliceText: 'value',
            slices: { 0: { offset: 0.1 }, 2: { offset: 0.1 } },
            tooltip: { textStyle: { fontSize: 12 } },
            animation: {
                startup: true,
                duration: 1000,
                easing: 'out'
            }
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechartEmployee_3d'));
        chart.draw(data, options);
    });
}

function renderTableProjectDueday() {
    var projectTurntimes = [];

    $.ajax({
        url: '/Project/getAllProject',
        type: 'GET',
        success: function (data) {
            console.log("Danh sach du lieu: ", data);
            if (data.length > 0) {
                data.forEach((item, index) => {
                    let turntime = parseInt(item.turntime);
                    let projectName = item.projectName;
                    projectTurntimes.push([projectName, turntime]);
                });

                drawLineChart(projectTurntimes);
            } else {
                $('#listproject').append(
                    `<tr>
                        <td class="text-center" colspan="8">No data</td>
                    </tr>`
                );
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function drawLineChart(projectTurntimes) {
    google.charts.load('current', { packages: ['corechart', 'line'] });
    google.charts.setOnLoadCallback(function () {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Project Name');
        data.addColumn('number', 'Turntime (days)');

        projectTurntimes.forEach(function (item) {
            data.addRow(item);
        });

        var options = {
            title: 'Project Turntime (in days)',
            titleTextStyle: {
                color: '#1a73e8', // Change this to your desired color
                fontSize: 18,     // You can also change the font size if needed
                bold: true        // You can make the title bold
            },
            hAxis: { title: 'Project' },
            vAxis: { title: 'Turntime (days)' },
            curveType: 'function',
            legend: { position: 'bottom' },
            tooltip: { textStyle: { fontSize: 12 } },
            animation: {
                startup: true,
                duration: 1000,
                easing: 'out'
            },
            series: {
                0: { color: '#e2431e' } // Customize color of the line
            },
            pointSize: 5, // Add points on the line
            lineWidth: 2, // Set line width
            focusTarget: 'category', // Highlight on hover
        };

        var chart = new google.visualization.LineChart(document.getElementById('linechart'));
        chart.draw(data, options);
    });
}
