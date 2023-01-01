
    fnPreviewFiles();
    fnWhenLoaded();
    fnAddNew();
    fnSaveItem();
    fnEditRow();
fnDeleteRow();


    $('.delete').click(function(){
        console.log(this);
    });
    function fnPreviewFiles(){

            const fileInput = document.getElementById('FileUpload_FormFile');
    const previewContainer = document.getElementById('previewContainer');
            
            fileInput.addEventListener('change', () => {
        previewContainer.innerHTML = ''; // clear previous previews

    for (let i = 0; i < fileInput.files.length; i++) {
                const file = fileInput.files[i];

    // const iframe = document.createElement('iframe');

    const reader = new FileReader();
            
                reader.onload = (event) => {
        //iframe.src = event.target.result;
        console.log(event.target.result);
    var row = '<div class="iframeParent col-md-2 m-2">'
        + '<iframe src='+ event.target.result +' scrolling="no"></iframe>'
        + '<button><i class="bi bi-trash3-fill"></i></button>'
        + '</div>';
    ////previewContainer.appendChild(iframe);
    //previewContainer.appendChild(row2);
    $("#previewContainer").append(row);
                };

    reader.readAsDataURL(file);
              }
            });   
    };

    $("#downloadButton").click(function(){

        $.ajax({
            url: "https://localhost:7250/api/Document/GetFile",
            type: 'GET',
            data: { expenseId: 0 },
            success: function (result) {
                $("#fileOuput").innerHTML = '';

                var fileServerUrl = "https://localhost:7250/Files/";

                for (let i = 0; i < result.length; i++) {
                    //console.log(result[i].fileDownloadName);
                    var fileurl = fileServerUrl + result[i];
                    var row2 = '<div class="iframeParent col-md-2 m-2">'
                        + '<iframe src=' + fileurl + ' scrolling="no"></iframe>'
                        + '<button class="delete"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16"><path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5Zm-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5ZM4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06Zm6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528ZM8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5Z"/></svg></button>'
                        + '</div>';
                    $("#fileOuput").append(row2);
                }
            },
            error: function (res) {
            },
            failure: function (errMsg) {
            }
        });
    });

    //add a new row
    function fnAddNew(){
        // Append table with add row form on add new button click
        $(".add-new").click(function () {
            //var actions = $("table td:last-child").html();
            var actions = '<a class="add" title="Add" data-toggle="tooltip"><i class="material-icons"></i></a>' +
                '<a class="edit" title="Edit" data-toggle="tooltip"><i class="material-icons"></i></a>' +
                '<a class="delete" title="Delete" data-toggle="tooltip"><i class="material-icons"></i></a>';
            $(this).attr("disabled", "disabled");
            var index = $("table tbody tr:last-child").index();
            index = index + 2;
            var row = '<tr>' +
                '<td>' + fnAddElement("", "") + '</td>' +
                '<td>' + fnAddElement("", "expenseType") + '</td>' +
                '<td>' + fnAddElement("", "expenseSubType") + '</td>' +
                '<td>' + fnAddElement("", "") + '</td>' +
                '<td>' + fnAddElement("", "") + '</td>' +
                '<td>' + fnAddElement("", "") + '</td>' +
                '<td>' + actions + '</td>' +
                '</tr>';

            $("table").append(row);
            $("table tbody tr").eq(index).find(".add, .edit").toggle();

            var typeId = '.' + $("table tbody tr").eq(index - 1).find('input[type="text"]').eq(1).attr("class");
            var subTypeId = '.' + $("table tbody tr").eq(index - 1).find('input[type="text"]').eq(2).attr("class");

            //$('[data-toggle="tooltip"]').tooltip();
            loadDropdown(typeId, subTypeId);

        });
    };

    function fnWhenLoaded(){
    };

    //saveItem
    function fnSaveItem(){
        // Add row on add button click
        $(document).on("click", ".add", function () {
            var empty = false;
            var input = $(this).parents("tr").find('input[type="text"]');
            input.each(function (index) {
                if (index != 5) {
                    if (!$(this).val()) {
                        $(this).addClass("error");
                        empty = true;
                    } else {
                        $(this).removeClass("error");
                    }
                }
            });
            $(this).parents("tr").find(".error").first().focus();
            if (!empty) {

                var rowNo = $("table tbody tr:last-child").index();
                rowNo = rowNo + 1;
                var quantity = 0;
                var price = 0;
                input.each(function (index) {
                    if (index == 0) {
                        $(this).attr('value', $(this).val());
                    }
                    else if (index == 1) {
                        $(this).attr('value', $(this).val());
                    }
                    else if (index == 2) {
                        $(this).attr('value', $(this).val());

                    }
                    else if (index == 3) {
                        $(this).attr('value', $(this).val());
                        quantity = $(this).val();
                    }
                    else if (index == 4) {
                        $(this).attr('value', $(this).val());
                        price = $(this).val();
                    }
                    else if (index == 5) {
                        $(this).attr('value', parseFloat(quantity) * parseFloat(price));
                    }

                });

                $(this).parents("tr").find(".add, .edit").toggle();
                $(".add-new").removeAttr("disabled");
            }

            $("#TotalAmountId").attr('value', calculateTotal());
        });
    };

    //editing a row
    function fnEditRow(){
        //// Edit row on edit button click
        $(document).on("click", ".edit", function () {
            $(this).parents("tr").find("td:not(:last-child)").each(function (index) {

                var currentIndex = $(this).parent("tr").index();
                currentIndex = currentIndex + 1;

                if (index == 0) {
                    $(this).children("input").attr('value', $(this).children("input").val());
                }
                else if (index = 1) {
                    $(this).children("input").attr('value', $(this).children("input").val());
                }
                else if (index = 2) {
                    $(this).children("input").attr('value', $(this).children("input").val());
                }
                else if (index = 3) {
                    $(this).children("input").attr('value', $(this).children("input").val());
                }
                else if (index = 4) {
                    $(this).children("input").attr('value', $(this).children("input").val());
                }
                else {
                    $(this).children("input").attr('value', $(this).children("input").val());
                }
            });
            $(this).parents("tr").find(".add, .edit").toggle();
            $(".add-new").attr("disabled", "disabled");
        });
    };

    //delete a row
    function fnDeleteRow(){

        // Delete row on delete button click
        $(document).on("click", ".delete", function () {

            $(this).parents("tr").remove();
            $(".add-new").removeAttr("disabled");
            $("#TotalAmountId").attr('value', calculateTotal());

        });
    };

    //add an input field
    function fnAddElement(value,Class) {
            var ele = "<input style='width: 100%;' type='text' value='"+value+"' class='"+Class+"' />";
    return ele;
    };

    function calculateTotal(){

            var count = $("table tbody tr:last-child").index();
    count = count+1;
    var sum = 0;
    for(let i = 1; i<=count; i++){
        sum = parseFloat(sum) + parseFloat($("#ItemAmountId" + i).val());
            }

    return sum;
    };

    function loadDropdown(typeId, subTypeId){

        $(typeId).kendoDropDownList({
            optionLabel: "Select Item Type...",
            height: 310,
            footerTemplate: 'Please ensure the type selected is correct',
            dataSource: {
                transport: {
                    read: {
                        url: "https://localhost:7250/api/Common/GetDropDownList",
                        type: "Post",
                        contentType: "application/json",
                        data: { mode: "ExpenseType", condition1: "", condition2: "" },
                        dataType: 'json'
                    },
                    parameterMap: function (data) {
                        var returnData = JSON.stringify(data);
                        return returnData
                    }
                }
            },
            dataTextField: "ddlText",
            dataValueField: "ddlVal"
        }).data("kendoDropDownList");

    $(subTypeId).kendoDropDownList({
        optionLabel: "Select SubType...",
    height: 310,
    footerTemplate: 'Please ensure the subtype selected is correct',
    dataSource:{
        transport: {
        read: {
        url: "https://localhost:7250/api/Common/GetDropDownList",
    type: "Post",
    contentType: "application/json",
    data: {mode: "ExpenseSubType",condition1: "",condition2: ""},
    dataType: 'json'
                            },
    parameterMap: function (data) {
                                var returnData = JSON.stringify(data);
    return returnData
                            }
                        }
                    },
    dataTextField: "ddlText",
    dataValueField: "ddlVal"
                }).data("kendoDropDownList");
     };

    function fnGetModelList(){
            var arrList = [];
    $("table tbody tr").each(function(index){

                var model = {
        itemId: 1,
    expenseId: 0,
    itemName: $("table tbody tr").eq(index).find('input[type="text"]').eq(0).val(),
    expenseType: $("table tbody tr").eq(index).find('input[type="text"]').eq(1).val(),
    expenseSubType: $("table tbody tr").eq(index).find('input[type="text"]').eq(2).val(),
    itemPrice: $("table tbody tr").eq(index).find('input[type="text"]').eq(4).val(),
    itemQuantity: $("table tbody tr").eq(index).find('input[type="text"]').eq(3).val(),
    itemAmount: $("table tbody tr").eq(index).find('input[type="text"]').eq(5).val()
                };
    arrList.push(model);
            });
    return arrList;
    };

    function fnGetItemDetailModel(){
            var itemDetailData = JSON.stringify({
        expenseId : 0,
    item: fnGetModelList()
            });
    return itemDetailData;
    };

    function fnModelSave(){

        $.ajax({
            url: "https://localhost:7250/api/Expense/SaveExpense",
            type: 'POST',
            datatype: 'json',
            data: fnGetItemDetailModel(),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
            },
            error: function (res) {
            },
            failure: function (errMsg) {
            }
        });
    };

    function fileUpload(){
    
       // Checking whether FormData is available in browser  
       if (window.FormData !== undefined) {  
    
           var fileUpload = $("#FileUpload_FormFile").get(0);
    var files = fileUpload.files;

    // Create FormData object  
    var fileData = new FormData();
    //var expenseId = $("").val();

    // Looping over all files and add it to FormData object  
    for (var i = 0; i < files.length; i++) {
        fileData.append(0, files[i]);  
           }

    $.ajax({
        url: 'https://localhost:7250/api/Document/Upload',
    type: "POST",
    contentType: false, // Not to set any content header
    processData: false, // Not to process data
    data: fileData,
    success: function (result) {
        alert(result);  
               },
    error: function (err) {
        alert(err.statusText);  
               }  
           });  
       } else {
        alert("FormData is not supported.");  
       }  
};

