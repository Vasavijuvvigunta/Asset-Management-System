jQuery(function ($) {
    BindTable();
    function BindTable() {
        $('.loader').show('slow');
        $.ajax({
            url: 'GetDepartmentMasterPageload',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {
                var pageload = Result.split('|');
                var recordstatus = JSON.parse(pageload[0]);
                var resJ = JSON.parse(pageload[1]);

                $.each(recordstatus, function () {
                    $("#ddlstatus").append($("<option></option>").val(this["METASUBCODE"]).html(this["METADATADESCRIPTION"]));
                    $('#ddlstatus').trigger("chosen:updated");
                });

                BindTab(resJ, '1');

            }
        });
        $('.loader').show('slow');
    }

    //to not to allow spaces in the Department code field 

    $("#txtdepartmentcode").keypress(function (e) {
        var keyCode = e.which;

        // Allow only alphanumeric characters
        if (!(keyCode >= 48 && keyCode <= 57) &&
            !(keyCode >= 65 && keyCode <= 90) &&
            !(keyCode >= 97 && keyCode <= 122)) {
            e.preventDefault();
        }
    });

    function BindTab(ResData, type) {
        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecord = ResData[0];
        // TABLE BIND     
        if (type == '0') {
            $('#dynamic-table').dataTable().fnDestroy();
            cols.length = 0;
            cols1.length = 0;
        }
        if (exampleRecord) {
            //get keys in object. This will only work if your statement remains true that all objects have identical keys
            var keys = Object.keys(exampleRecord);

            //for each key, add a column definition
            keys.forEach(function (k) {
                cols.push({
                    title: k,
                    data: k,
                    targets: k

                    //optionally do some type detection here for render function
                });
            });

            $.each(cols, function (index, item) {

                //item.title, item.targets

                cols1.push({
                    title: item.title,
                    targets: index
                });

                cols1DATA.push({
                    data: item.title,
                });

                finalcolsresult += 'null' + ',';
            });

            $('#dynamic-table').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "bSort": false,
                "info": false,
                "ascrollX": "100%",
                "language": { "search": "" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'columnDefs': [{
                    'targets': 0,
                    'searchable': true,
                    'orderable': true,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                columns: [



                    { className: "text-capitalize", 'data': 'Department Code' },
                    { className: "text-capitalize", 'data': 'Department Name' },
                    { className: "text-capitalize", 'data': 'Status' },
                    {
                        'data': 'Edit',
                        'render': function (data, type, full, meta) {

                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="typcn typcn-edit" aria-hidden="true"></i><span style="visibility: hidden;">' + data + '</span></button>'
                        }
                    },

                ],
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Department Master',
                        text: '<img src="../../Images/excel.png" style="height: 25px;" title="Excel">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',
                        text: '<img src="../../Images/pdf.png" style="height: 25px;" title="PDF">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Department Master'
                    },
                    {
                        extend: 'print',
                        title: 'Department Master',
                        text: '<img src="../../Images/print.png" style="height: 25px;" title="Print">',
                        footer: false
                    }
                ]
            });
        }
        else {
            $('#dynamic-table tbody').remove();
            $('#dynamic-table thead').remove();
            $('#dynamic-table').dataTable({
                "language": {
                    "emptyTable": "Department Master No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false }

                ],
                "scrollCollapse": false,
                "info": true,
                "paging": true,
                "searching": true
            });
        }
    }
    // start INSERT

    $('#departmentmaster-form').parsley().on('field:validated', function () {
        var ok = $('.parsley-error').length === 0;
        $('.bs-callout-info').toggleClass('hidden', !ok);
        $('.bs-callout-warning').toggleClass('hidden', ok);

    })

   
        .on('form:submit', function () {
            save();
            return false;
        });

        // save function
    function save()
    {


            if (window.FormData !== undefined) {
                //$('.loadingGIF').show()
                var formData = new FormData();
                formData.append('DEPARTMENTCODE', $('#txtdepartmentcode').val());
                formData.append('DEPARTMENTNAME', $('#txtdepartmentname').val());
                formData.append('STATUS', $('#ddlstatus').val());
                formData.append('AUTOID', $("#hdautoid").val());
                formData.append('actiontype', $('#actiontype').text());
                $(".loader").show("slow");
                $.ajax({
                    url: 'InsertDepartmentMaster',
                    dataType: "json",
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  

                    data: formData,
                    success: function (data) {
                        var Res = data.split('|');
                        var result = Res[0];
                        var msg = Res[1];
                        if (result.toUpperCase() == "TRUE") {
                            $("form").trigger("reset");
                            $("#actiontype").text("Save")
                            $('.loadingGIF').hide();
                            bootbox.alert({

                                message: '<span class="text-success"><i class="icon ion-ios-checkmark-circle-outline tx-50 tx-success"style="margin-left: 100px;font-size: 50px;"></i><br>' + msg + '</span>',
                                size: 'small',
                                callback: function () {
                                    Clear();
                                }
                            });

                            $('#txtdepartmentcode').val("");
                            $("#txtdepartmentname").val("");
                            $('#ddlstatus').val("MDSUB_001_0001");
                            $('.loadingGIF').hide();
                        }
                        else {
                            bootbox.alert({
                                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + msg + '</span>',
                                size: 'small'
                            });
                            $('.loadingGIF').hide();
                        }
                    }
                });
            }


     }

        function Clear() {
            location.reload();
        };
        $('#btnclear').click(Clear);


    // start of edit


    $("#dynamic-table").on("click", ".editdetails", function () {
        var autoid = $(this).closest('tr').find('td:eq(3) > button > span').html();
        //var EMPLOYEECODE = $(this).closest('tr').find('td:eq(0)').html();
        GetDepartmentMasterID(autoid)
    });


    function GetDepartmentMasterID(autoid) {
        $(".loader").show("slow");
        $.ajax({
            url: 'GetDepartmentMasterID',
            dataType: 'json',
            type: 'POST',
            data: $.param({ 'ID': autoid }),

            success: function (data) {
                var resJ = JSON.parse(data);
                //  $.each(resJ, function (i, item) {

                $("#hdautoid").val(resJ[0].AUTOID);

                $("#txtdepartmentcode").val(resJ[0].DEPARTMENTCODE);
                $("#txtdepartmentname").val(resJ[0].DEPARTMENTNAME);
                $('#ddlstatus').val(resJ[0].STATUS);
                $('#ddlstatus').trigger('change');
                $("#actiontype").text("Update");

            }

        });

        $(".loader").hide("slow");
    }


});
   