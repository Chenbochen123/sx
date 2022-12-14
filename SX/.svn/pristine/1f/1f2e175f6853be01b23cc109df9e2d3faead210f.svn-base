<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/") %>tpl/login.css" />
    <script type="text/javascript" language="javascript" src="resources/js/jquery-1.7.1.js"></script>
    <!-- 登录界面普通处理开始 -->
    <script type="text/javascript">
        var gUrlPrefix = '/';
        var theargs = new getarg();
        var isCaptcha = theargs.isCaptcha;
        var code = theargs.code;
        function getarg() {
            var url = unescape(window.location.href);
            var allargs = url.split("?")[1];
            try {
                if (!allargs) {
                    allargs = "code=0&isCaptcha=2";
                }
            }
            catch (exception) { }
            var args = allargs.split("&");
            for (var i = 0; i < args.length; i++) {
                var arg = args[i].split("=");
                eval('this.' + arg[0] + '="' + arg[1] + '";');
            }
        }

        //自动完成匹配用的域名列表
        var domainList = [];

        var gLockInfo = ""; var msgTpl = {
            1: "您填写的用户名称或密码错误。",
            2: "您的帐户已经被锁定。",
            3: "验证码已经过期，请重新输入。",
            4: "您填写的验证码错误，请重新输入。",
            5: "重试次数太多，用户被锁定。",
            6: "重试次数太多，IP被锁定，" + gLockInfo + "分钟后再试。"
        };

        var showMsgCode = function (c) {
            c = c - 0;
            if (c === 0) {
                showMsg();
            } else if (msgTpl[c]) {
                showMsg(msgTpl[c]);
            } else {
                showMsg(msgTpl[1]);
            }
        }

        var showMsg = function (msg) {
            if (msg === undefined) {
                document.getElementById('msg').style.display = 'none';
            } else {
                document.getElementById('msg').innerHTML = msg;
                document.getElementById('msg').style.display = '';
            }
        }

        var getLoginTo = function () {
            return getCookie('MesnacLoginTo');
            return '';
        }

        var setCookie = function (k, v) {
            document.cookie = k + '=' + encodeURIComponent(v) + ' ;expires=' + (new Date(
                (new Date()).getTime() + 180 * 24 * 3600 * 1000
                )).toGMTString() + ' ;path=/';
        }

        var getCookie = function (k) {
            var cookieStr = document.cookie;
            if (cookieStr != "") {
                var cookieArr = cookieStr.split(';');
                var everyCookie, cookieKey, cookieValue, splitPos;
                for (i = 0; i < cookieArr.length; i++) {
                    everyCookie = cookieArr[i].replace(/(^\s*)|(\s*$)/g, '');
                    if (everyCookie != "") {
                        splitPos = everyCookie.indexOf('=');
                        if (splitPos == -1) continue;
                        cookieKey = everyCookie.substr(0, splitPos);
                        cookieValue = everyCookie.substr(splitPos + 1);

                        if (cookieKey === k) {
                            return decodeURIComponent(cookieValue);
                        }
                    }
                }
            }

            return '';
        }

        var setLoginTo = function (loginTo) {
            setCookie('MesnacLoginTo', loginTo);
        }

        var on = function (node, ev, callback) {
            if (!node) return;
            var _callback = function (ev) {
                if (false === callback.apply(node, [ev])) {
                    if (ev.preventDefault) {
                        ev.preventDefault();
                    }
                    return false;
                }
            }
            if (node.addEventListener) {
                node.addEventListener(ev, _callback, false);
            } else {
                node.attachEvent('on' + ev, _callback);
            }
        }

        var _setPlaceholder = function (b) {
            var a = b.getAttribute("_placeholder");
            if (!a) return;
            if ("placeholder" in b) {
                b.placeholder = a;
                return b;
            }

            var f = false;
            var _b;
            //password补丁
            if (b.type === 'password') {
                f = true;
                _b = document.createElement('input');
                _b.type = 'text';
                _b.style.fontSize = '20px';
                _b.style.color = "#999";
                _b.value = a;
                _b.className = 'text';
                _b.style.display = 'none';
                b.parentNode.insertBefore(_b, b);
            }

            //初始化
            if (f) {
                if (b.value !== '') {
                    b.style.display = '';
                    _b.style.display = 'none';
                } else {
                    b.style.display = 'none';
                    _b.style.display = '';
                }
            } else {
                if (b.value !== "") {
                    b.empty = 0;
                    b.style.color = "#000";
                } else {
                    b.empty = 1;
                    b.value = a;
                    b.style.color = "#999";
                }
            }
            if (f) {
                on(_b, "focus", function () {
                    _b.style.display = 'none';
                    b.style.display = '';
                    b.focus();
                });
                on(b, "blur", function () {
                    if (b.value === "") {
                        _b.style.display = '';
                        b.style.display = 'none';
                    }
                });
            } else {
                on(b, "focus", function () {
                    if (this.empty == 1) {
                        this.value = "";
                        this.style.color = "#000";
                    }
                });
                on(b, "blur", function () {
                    if (this.empty == 1) {
                        this.value = a
                        this.style.color = "#999";
                    }
                });
                on(b, "change", function () { if (this.value === "") { this.empty = 1; } else { this.empty = 0; } });
            }
            return b;
        }

        window.onload = function () {
            if (isCaptcha == 1) {
                var authCode = document.getElementById('auth_code');
                authCode.style.display = '';
            }



            var _u = getCookie('MesnacUser');
            if (_u) {
                document.getElementById('user').value = _u;
                document.form_login.user.value = _u;
            }

            var ipts = document.getElementsByTagName('input');
            for (var i = ipts.length - 1; i >= 0; i--) {
                var type = ipts[i].type;
                if ((type == 'text' || type == 'password') && ipts[i].getAttribute('_placeholder')) {
                    _setPlaceholder(ipts[i]);
                }
            }


            var authCode = document.getElementById('auth_code');
            var authImage = document.getElementById('auth_code_img');
            var timer = 0;
            on(authCode, 'focus', function () {
                if (authImage.innerHTML === '') {
                    loadImage();
                }
                clearTimeout(timer);
                authImage.style.display = '';
            });

            on(authCode, 'blur', function () {
                clearTimeout(timer);
                timer = setTimeout(function () {
                    authImage.style.display = 'none';
                }, 100);
            });

            on(authImage, 'click', function () {
                loadImage();
                clearTimeout(timer);
                authCode.focus();
            });

            var loadImage = function () {
                authImage.innerHTML = '<img width=112 height=38 title="点击更换验证码" src="' + "Manager/inc/CodeImg.aspx?r=" + Math.random() + '">';
            }

            if (isCaptcha == 2) {
                var forceCaptcha = false;
                var showCaptcha = function () {
                    var authCode = document.getElementById('auth_code');
                    authCode.style.display = '';
                    forceCaptcha = true;
                }

                var hideCaptcha = function () {
                    var authCode = document.getElementById('auth_code');
                    authCode.style.display = 'none';
                    forceCaptcha = false;
                }

                var changeFun = function () {
                    var u = document.getElementById('user').value.replace(/^\s+|\s+$/g, '');
                    if (u === '') {
                        hideCaptcha();
                        return;
                    }
                    if (isCaptcha == 1 || forceCaptcha) {
                        showCaptcha();
                    }
                    else {
                        hideCaptcha();
                    }
                };


                on(document.getElementById('user'), 'blur', changeFun);
                changeFun();
            }

            var beforeLogin = function () {
                var uc1, uc2, fUrl;
                var loginUrl, locationUrl;

                var loginSsl = document.form_login.login_ssl.value;

                uc1 = new UrlControl();
                uc2 = new UrlControl();

                uc1.setUrl(window.location.href);
                fUrl = uc1.getParam("furl");

                var tabs = document.getElementById('tab').getElementsByTagName('li');
                for (var i = 0; i < tabs.length - 1; i++) {
                    if (tabs[i].className === 'current') {
                        fUrl = document.form_login.go.value;
                    }
                }

                var urlPort1, urlPort2, urlProtocol1, urlProtocol2;
                if ("" === uc1.urlProtocol) {
                    urlPort1 = "";
                } else {
                    urlProtocol1 = uc1.urlProtocol + "://";
                    urlPort1 = "" === uc1.urlPort ? "" : (":" + uc1.urlPort);
                }

                uc2.setUrl(urlProtocol1 + uc1.urlHostname + urlPort1 + uc1.getPath());
                switch (parseInt(loginSsl)) {
                    case 1:
                        if (document.form_login.login_ssl.checked) {
                            uc2.urlProtocol = 'https';
                        }
                        break;
                    case 2:
                        uc2.urlProtocol = 'https'; //login.do 
                        break;
                    case 3:
                        uc1.urlProtocol = 'https'; //q=base 
                        uc2.urlProtocol = 'https';
                        break;
                    default:
                }

                if ("" === uc2.urlProtocol) {
                    urlPort2 = "";
                } else {
                    urlProtocol2 = uc2.urlProtocol + "://";
                    urlPort2 = "" === uc2.urlPort ? "" : (":" + uc2.urlPort);
                }

                if ("" === uc1.urlProtocol) {
                    urlPort1 = "";
                } else {
                    urlProtocol1 = uc1.urlProtocol + "://";
                    urlPort1 = "" === uc1.urlPort ? "" : (":" + uc1.urlPort);
                }

                if (fUrl) {
                    var uc3 = new UrlControl();
                    uc3.setUrl(decodeURIComponent(fUrl));
                    if (!uc3.urlProtocol) {
                        locationUrl = urlProtocol1 + uc1.urlHostname + urlPort1 + uc3.getPath() + "?" + uc3.getQs();
                    } else {
                        locationUrl = uc3.getUrl();
                    }
                } else {
                    locationUrl = urlProtocol1 + uc1.urlHostname + urlPort1 + uc1.getPath() + "?q=base";
                }

                document.form_login.setAttribute('action', uc2.getUrl() + "?q=login.do");
                document.form_login.referer.value = window.location.href;
                document.form_login.go.value = locationUrl;
            }

            var onsubmit = function (ev, noAlert) {
                noAlert = !!noAlert;
                showMsg();
                var uNode = document.getElementById('user');
                var u = uNode.value.replace(/^\s+|\s+$/g, '');
                if (u === '' || uNode.empty === 1 || document.form_login.password.value === '') {
                    if (!noAlert) {
                        showMsg("请填写用户名称和密码。");
                    }
                    if (u === '' || uNode.empty === 1) {
                        uNode.focus();
                    } else {
                        if (document.form_login.password.style.display === 'none') {
                            document.form_login.password.previousSibling.style.display = 'none';
                            document.form_login.password.style.display = '';
                        }
                        document.form_login.password.focus();
                    }
                    return false;
                }

                document.form_login.user.value = u;

                //如果开启验证码
                if (isCaptcha == 1 || forceCaptcha) {
                    if ("" == authCode.value || authCode.empty === 1) {
                        if (!noAlert) {
                            showMsg("请填写验证码。");
                        }
                        authCode.focus();
                        return false;
                    }
                }

                setCookie('MesnacUser', u);
                beforeLogin();

                return true;
            };

            on(document.getElementById('user'), 'keydown', function (ev) {
                if (ev.keyCode === 13) {
                    setTimeout(function () {
                        if (false === onsubmit(null, true)) {
                            return;
                        } else {
                            document.form_login.submit();
                        }
                    }, 100);
                }
            });

            on(document.form_login, 'submit', onsubmit);

            showMsgCode(code);
        }
    </script>
    <!-- 登录界面普通处理结束 -->
</head>
<body>
    <div class="t">
        <div class="h">
            <div class="logo">
                <img src="tpl/login/user/images/login_logo.png" /></div>
            <a href="help.html" target="_blank" class="help">帮助</a>
        </div>
    </div>
    <div class="c">
        <div class="box" style="right: 60px; top: 60px;" id="box">
            <!-- 登录到切换工具栏开始 -->
            <ul class="tab" id="tab">
                <!--li class="current">收信</li-->
                <li id='inbox'>用户登录</li>
                <li class="dragbar" id="dragbar"></li>
            </ul>
            <!-- 登录到切换工具栏结束 -->
            <!-- 错误信息提示框开始 -->
            <div class="msg" style="display: none" id="msg">
            </div>
            <!-- 错误信息提示框结束 -->
            <div class="boxc">
                <h3>
                    &nbsp;<span id="where"></span></h3>
                <div class="text_item">
                    <input type="text" class="text" id="user" autocomplete="off" _placeholder="用户名称" onfocus="this.className='text_f'"
                        onblur="this.className='text'" />
                    <div class="pop" style="display: none;" id="pop">
                    </div>
                </div>
                <form name='form_login' method="post" action="Manager/LoginHandler.ashx">
                <div class="text_item">
                    <input type="hidden" class="text" name="user" value="" />
                    <input type="password" class="text" name="password" _placeholder="密码" onfocus="this.className='text_f'"
                        onblur="this.className='text'" />
                </div>
                <div class="bl">
                    <span style="float: left">
                        <input type="hidden" name="login_ssl" value="0" />
                    </span><span class="blt"><a href="help.html" target="_blank">忘记密码</a> </span>
                </div>
                <div class="btnb">
                    <!-- 验证码控制开始 -->
                    <input type="text" name="auth_code" class="text" _placeholder="验证码" style="width: 100px;
                        float: left; display: none;" onfocus="this.className='text_f'" onblur="this.className='text'"
                        id="auth_code" />
                    <div class="yzmbox" style="z-index: 1000; display: none" id="auth_code_img">
                    </div>
                    <!-- 验证码控制结束 -->
                    <!-- 登录后的URL控制开始 -->
                    <!-- 登录成功以后跳转的URL -->
                    <input type="hidden" name="go" value="Manager/MainFrame.aspx">
                    <!-- 登录成功失败后跳转的URL -->
                    <input type="hidden" name="referer" value="Default.aspx">
                    <!-- 登录后的URL控制结束 -->
                    <!-- 登录按钮 -->
                    <input type="submit" class="btn" value="登  录" style="float: right" />
                    <div style="clear: both">
                    </div>
                </div>
                </form>
            </div>
        </div>
        <div class="f" id="f" style="display: none;">
        </div>
        <div class="login_drag" id="drag_target">
        </div>
    </div>
    <div class="b">
        <!-- 版权信息开始 -->
        MES 5.0 &copy;2002-2013 mesnac.com &nbsp;&nbsp;&nbsp;&nbsp;中国第一大轮胎工业MES整体解决方案提供商&nbsp;&nbsp;&nbsp;&nbsp;
        <!-- 版权信息结束 -->
    </div>
    <!--<script type="text/javascript" src="tpl/public/js/load_cache.js"></script>-->
</body>
<!-- 登录界面高级处理开始 -->
<script type="text/javascript" src="tpl/public/js/login.js"></script>
<script type="text/javascript">
    if (window.Login) {
        try {
            (new Login()).init();
        } catch (e) { }
    }
</script>
<!-- 登录界面高级处理结束 -->
</html>
