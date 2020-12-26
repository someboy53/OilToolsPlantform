//閿佸睆		   
var a = function (e) {
	e.preventDefault();
}

$(function () {

	function wrap() {

	}

	window.onload = function () {

	}

	var cd = 0;
	//鎵撳紑璐墿杞�
	$('.storeIcon').hammer().on('tap', function () {
		var alt = $(this).attr("alt");
		if ((alt == 0)) { //鎵撳紑璐墿杞�
			$('.storeIcon').attr("alt", "1");
			$("body,html").addClass('hidden');
			$("header").addClass('showStore');
			setTimeout(function () {
				$(".storeMain").slideDown();
			}, 0);
		} else {
			$('.storeIcon').attr("alt", "0");
			$("body,html").removeClass('hidden');
			$("header").removeClass('showStore');
			setTimeout(function () {
				$(".storeMain").hide();
			}, 0);
		}
		$("header").removeClass('showMenu');
		$(".menuMain").hide().removeClass('showMain');
		$('.btn-nav').removeClass('animated2');
	});


	//鎵撳紑鑿滃崟
	$('.btn-nav').hammer().on('tap', function () {
		var alt = $(".storeIcon").attr("alt");

		if ((cd == 0) && (alt == 0)) { //璐墿杞�-鍏� 鎵撳紑鑿滃崟
			cd = 1;
			$("body,html").addClass('hidden');

		} else if ((cd == 0) && (alt == 1)) { //璐墿杞�-寮€ 鎵撳紑鑿滃崟
			cd = 1;
			$("body,html").addClass('hidden');
		} else if (cd == 1) { //鍏抽棴鑿滃崟
			cd = 0;
			$("body,html").removeClass('hidden');
		}
		$(this).toggleClass('animated2');
		$("header").toggleClass('showMenu');
		$("header").removeClass('showStore');
		$('.storeIcon').attr("alt", "0");
		$(".menuMain").slideToggle();
		$('.menuBox .list').hide();
		$('.menuBox li').removeClass("on");
		setTimeout(function () {
			$(".menuMain").toggleClass('showMain');
			$(".storeMain").hide();
		}, 300);
	});
	//灞曞紑瀛愯彍鍗�
	$('.menuBox li>a').hammer().on('tap', function () {
		$(this).parents("li").toggleClass('on');
		$(this).parents("li").find(".list").slideToggle();
	});
	//鎼滅储
	$(".searchBtn").hammer().on('tap', function () {
		// 鐩戞祴浠ｇ爜 20180914 娣诲姞
		// _taq.push({convert_id:"1608029767946276", event_type:"button"});

		$(".menuBox").addClass("menuSearch");
		$(".linkBtn").fadeOut(300);
		$('.menuBox .list').hide();
		$('.menuBox li').removeClass("on");
		$(".searchForm input").val("").focus();
		$(".menuIcon").hide();
		$(".menuIcon2").fadeIn();
		setTimeout(function () {
			$(".menuBox").addClass("searchHide");
			$(".searchForm input").focus();
			$(".menuBox .nextBox .other").fadeIn();
		}, 500);
	});
	//鍏抽棴鎼滅储
	$(".menuIcon2").hammer().on('tap', function () {
		$(".menuBox").removeClass("searchHide");
		$(".menuBox").removeClass("menuSearch");
		$(".linkBtn").fadeIn(300);
		$(".searchForm input").blur();
		$(".menuBox .nextBox .other").hide();
		setTimeout(function () {
			$(".menuIcon2").hide();
			$(".menuIcon").fadeIn();
		}, 300);
	});


	//搴曢儴鑿滃崟鎵撳紑
	//灞曞紑瀛愯彍鍗�
	$('.footNav li>a').hammer().on('tap', function () {
		$(this).parents("li").toggleClass('on');
		$(this).parents("li").find(".list").slideToggle();
	});

	//浜岀淮鐮�
	$('.maBox').hammer().on('tap', function () {
		$(this).find("img").hide();
		$(this).fadeOut();
	});
	$('.maBtn').hammer().on('tap', function () {
		$('.maBox').fadeIn();
		$('.maBox img').fadeIn();
	});

	//hero瑙嗛寮规
	$(".video_icon").hammer().on('tap', function () {
		$(".moviebox").fadeIn();
		var href = $(this).attr("alt");
		$("#movie").attr('src', href);
		setTimeout(function () {
			Media = document.getElementById("movie");
			Media.play();
		}, 300);
	});
	$(".moviebox .close").click(function () {
		$(".moviebox").fadeOut();
		Media = document.getElementById("movie");
		Media.pause();
	});

	$('#runsearch1').click(function (event) {
		var skey = $('input[name=headerkey]').val();
		window.location.href = '/search/index/keyword/' + skey + '.html';
	});

	$("body").on("touchstart", ".backTop", function () {
		$(window).scrollTop(0);
	});

	//
	$(".btn-nav").click(function () {
		var num = $(this).attr('alt');
		if (num == 0) {
			//$(".storeIcon").hide();
			//$(".zxIcon").hide();
			$(this).attr('alt', '1');
			// console.log(num)
		} else {
			$(this).attr('alt', '0');

			setTimeout(function () {
				//$(".storeIcon").fadeIn(300);
				//$(".zxIcon").fadeIn(300);
			}, 300);
		}
	})

});

function getUrlParam(name) {
	var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
	var r = window.location.search.substr(1).match(reg);  //匹配目标参数
	if (r != null) return unescape(r[2]); return null; //返回参数值
}