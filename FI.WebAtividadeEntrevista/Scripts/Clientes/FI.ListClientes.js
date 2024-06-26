
$(document).ready(function () {

    if (document.getElementById("gridClientes"))
        $('#gridClientes').jtable({
            title: 'Clientes',
            paging: true, //Enable paging
            pageSize: 5, //Set page size (default: 10)
            sorting: true, //Enable sorting
            defaultSorting: 'Nome ASC', //Set default sorting
            actions: {
                listAction: urlClienteList,
            },
            fields: {
                Id: {
                    title: '#',
                },
                CPF: {
                    title: 'CPF',
                    width: '15%'
                },
                Nome: {
                    title: 'Nome',
                    width: '30%'
                },
                Cidade: {
                    title: 'Cidade',
                },
                Estado: {
                    title: 'Estado',
                },
                Email: {
                    title: 'Email',
                    width: '30%'
                },
                Alterar: {
                    title: '',
                    display: function (data) {
                        return '<button onclick="window.location.href=\'' + urlAlteracao + '/' + data.record.Id + '\'" class="btn btn-primary btn-sm">Alterar</button>';
                    }
                }
            }
        });

    //Load student list from server
    if (document.getElementById("gridClientes"))
        $('#gridClientes').jtable('load');
})