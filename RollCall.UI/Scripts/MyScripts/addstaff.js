$(function () {
    $("#Del").click(function () {
        $('#myModalSmall').modal('show');//打開模板
        return false;
        //  alert("del")
    })
    $("#Update").click(function () {
        $('#myModalSmall1').modal('show');//打開模板
        return false;
        //  alert("del")
    })

    $("#delete").click(function () {
      
        //<!--获取选中复选框的值-->
               var arr = "";
               var chk_value = [];
               $('input[name="UserId"]:checked').each(function () {//遍历每一个名字为UserId的复选框，其中选中的执行函数  
                   chk_value.push($(this).val());//将选中的值添加到数组chk_value中
                   arr = arr + $(this).val() + ",";
                  
               });
               var time = (new Date()).valueOf();//添加時間戳 防止緩存
               $.ajax({
                   url: "/Staff/CrudStaff/" +time,
                   data: { Uid: arr, act: 'Del' },
                   success: function () {
                       //刷新頁面
                       history.go(0)
                      // $(this).addClass("done");
                   }
               });
               //alert(arr);
        $('#myModalSmall').modal('hide');//關閉模板
    });

    $("#updateclass").click(function () {

        //<!--获取选中复选框的值-->
        var arr = "";
        var chk_value = [];
        $('input[name="UserId"]:checked').each(function () {//遍历每一个名字为UserId的复选框，其中选中的执行函数  
            chk_value.push($(this).val());//将选中的值添加到数组chk_value中
            arr = arr + $(this).val() + ",";

        });
        var time = (new Date()).valueOf();//添加時間戳 防止緩存
        $.ajax({
            url: "/Staff/CrudStaff/"+time,
            data: { Uid: arr, act: 'UpdateClass' },
            success: function () {
               // alert("更改成功")
                //刷新頁面
                history.go(0)
                // $(this).addClass("done");
            }
        });

      
        //alert(arr);
        $('#myModalSmall').modal('hide');//關閉模板
    });


});



function dele(data) {
    alert(data);
   //刷新頁面
    history.go(0) 
  
}
function addstaff(data) {
    alert(data);
    //刷新頁面
   // history.go(0)
}
