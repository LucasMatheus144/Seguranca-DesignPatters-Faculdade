$(document).ready(function () {
    $('#Cadastro').DataTable({
        language: {          
                "decimal":        "",
                "emptyTable":     "Nenhum dado disponível na tabela",
                "info":           "Mostrando _START_ até _END_ de _TOTAL_ registros",
                "infoEmpty":      "Mostrando 0 até 0 de 0 registros",
                "infoFiltered":   "(filtrado de _MAX_ registros no total)",
                "infoPostFix":    "",
                "thousands":      ",",
                "lengthMenu":     "Mostrar _MENU_ registros",
                "loadingRecords": "Carregando...",
                "processing":     "",
                "search":         "Pesquisar:",
                "zeroRecords":    "Nenhum registro correspondente encontrado",
                "paginate": {
                    "first":      "Primeiro",
                    "last":       "Último",
                    "next":       "Próximo",
                    "previous":   "Anterior"
                },
                "aria": {
                    "orderable":  "Ordenar por esta coluna",
                    "orderableReverse": "Inverter a ordem desta coluna"
                }
            },
        lengthMenu: [4, 6, 8 ]
    });


});
