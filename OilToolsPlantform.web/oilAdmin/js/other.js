//获取顶层url中的参数
var getMainParam = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$|#)"); //构造一个含有目标参数的正则表达式对象
    var r = window.top.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
};

//获取当前url中的参数
var getParam = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$|#)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
};

var closeCurrent = function (needQuery) {
    var ifIndex = parent.layer.getFrameIndex(window.name);
    parent.layer.close(ifIndex);
    console.log(needQuery);
    if (needQuery) {
        parent.funcList.doSearch();
    }
};

var curAccess = window.top.curAccess;

