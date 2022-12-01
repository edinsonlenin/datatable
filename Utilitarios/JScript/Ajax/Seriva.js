function Seriva_AddEvent(obj, evType, fn, useCapture)
{
    if (obj.addEventListener)
    {
        obj.addEventListener(evType, fn, useCapture);
        return true;
    }
    else if (obj.attachEvent)
    {
        var r = obj.attachEvent("on" + evType, fn);
        return r;
    }
    else
    {
        alert("Seriva_AddEvent could not add event!");
    }
}

function Seriva_GetXMLHttpRequest()
{
    if (window.XMLHttpRequest)
    {
        return new XMLHttpRequest();
    }
    else
    {
        if (window.Seriva_XMLHttpRequestProgID)
        {
            return new ActiveXObject(window.Seriva_XMLHttpRequestProgID);
        }
        else
        {
            var progIDs = ["Msxml2.XMLHTTP.5.0", "Msxml2.XMLHTTP.4.0",
                "MSXML2.XMLHTTP.3.0", "MSXML2.XMLHTTP", "Microsoft.XMLHTTP"];
            for (var i = 0; i < progIDs.length; ++i)
            {
                var progID = progIDs[i];
                try
                {
                    var x = new ActiveXObject(progID);
                    window.Seriva_XMLHttpRequestProgID = progID;
                    return x;
                }
                catch (e){}
            }
        }
    }
    return null;
}

function Seriva_CallBack(url, target, id, method, args, clientCallBack,
    clientCallBackArg, includeControlValuesWithCallBack,
    updatePageAfterCallBack)
{
    if (window.Seriva_PreCallBack)
    {
        var preCallBackResult = Seriva_PreCallBack();
        if (!(typeof preCallBackResult == "undefined" || preCallBackResult))
        {
            if (window.Seriva_CallBackCancelled)
            {
                Seriva_CallBackCancelled();
            }
            return null;
        }
    }
    var x = Seriva_GetXMLHttpRequest();
    var result = null;
    if (!x)
    {
        result =
        {
            "value": null, "error": "NOXMLHTTP"
        };
        Seriva_DebugError(result.error);
        if (clientCallBack)
        {
            clientCallBack(result, clientCallBackArg);
        }
        return result;
    }
    x.open("POST", url ? url : Seriva_DefaultURL, clientCallBack ? true : false)
        ;
    x.setRequestHeader("Content-Type",
        "application/x-www-form-urlencoded; charset=utf-8");
    if (clientCallBack)
    {
        x.onreadystatechange = function()
        {
            if (x.readyState != 4)
            {
                return ;
            }
            Seriva_DebugResponseText(x.responseText);
            result = Seriva_GetResult(x);
            if (result.error)
            {
                Seriva_DebugError(result.error);
            }
            if (updatePageAfterCallBack)
            {
                Seriva_UpdatePage(result);
            }
            Seriva_EvalClientSideScript(result);
            clientCallBack(result, clientCallBackArg);

            x = null;

            if (window.Seriva_PostCallBack)
            {
                Seriva_PostCallBack();
            }
        }
    }
    var encodedData = "";
    if (target == "Page")
    {
        encodedData += "&Seriva_UtilitariosWeb_Ajax_PageMethod=" + method;
    }
    else if (target == "Control")
    {
        encodedData += "&Seriva_UtilitariosWeb_Ajax_ControlID=" + id.split(":").join("_");
        encodedData += "&Seriva_UtilitariosWeb_Ajax_ControlMethod=" + method;
    }
    if (args)
    {
        for (var argsIndex = 0; argsIndex < args.length; ++argsIndex)
        {
            if (args[argsIndex]instanceof Array)
            {
                for (var i = 0; i < args[argsIndex].length; ++i)
                {
                    encodedData += "&Seriva_CallBackArgument" + argsIndex + "="
                        + encodeURIComponent(args[argsIndex][i]);
                }
            }
            else
            {
                encodedData += "&Seriva_CallBackArgument" + argsIndex + "=" +
                    encodeURIComponent(args[argsIndex]);
            }
        }
    }
    if (updatePageAfterCallBack)
    {
        encodedData += "&Seriva_UpdatePage=true";
    }
    if (includeControlValuesWithCallBack)
    {
        var form = document.getElementById(Seriva_FormID);
        if (form != null)
        {
            for (var elementIndex = 0; elementIndex < form.length;
                ++elementIndex)
            {
                var element = form.elements[elementIndex];
                if (element.name)
                {
                    var elementValue = null;
                    if (element.nodeName == "INPUT")
                    {
                        var inputType = element.getAttribute("TYPE")
                            .toUpperCase();
                        if (inputType == "TEXT" || inputType == "PASSWORD" ||
                            inputType == "HIDDEN")
                        {
                            elementValue = element.value;
                        }
                        else if (inputType == "CHECKBOX" || inputType ==
                            "RADIO")
                        {
                            if (element.checked)
                            {
                                elementValue = element.value;
                            }
                        }
                    }
                    else if (element.nodeName == "SELECT")
                    {
                        if (element.multiple)
                        {
                            elementValue = [];
                            for (var i = 0; i < element.length; ++i)
                            {
                                if (element.options[i].selected)
                                {
                                    elementValue.push(element.options[i].value);
                                }
                            }
                        }
                        else
                        {
                            elementValue = element.value;
                            if (elementValue == "")
                            {
                                elementValue = null;
                            }
                        }
                    }
                    else if (element.nodeName == "TEXTAREA")
                    {
                        elementValue = element.value;
                    }
                    if (elementValue instanceof Array)
                    {
                        for (var i = 0; i < elementValue.length; ++i)
                        {
                            encodedData += "&" + element.name + "=" +
                                encodeURIComponent(elementValue[i]);
                        }
                    }
                    else if (elementValue != null)
                    {
                        encodedData += "&" + element.name + "=" +
                            encodeURIComponent(elementValue);
                    }
                }
            }
            // ASP.NET 1.1 won't fire any events if neither of the following
            // two parameters are not in the request so make sure they're
            // always in the request.
            if (typeof form.__VIEWSTATE == "undefined")
            {
                encodedData += "&__VIEWSTATE=";
            }
            if (typeof form.__EVENTTARGET == "undefined")
            {
                encodedData += "&__EVENTTARGET=";
            }
        }
    }
    Seriva_DebugRequestText(encodedData.split("&").join("\n&"));
    x.send(encodedData);
    if (!clientCallBack)
    {
        Seriva_DebugResponseText(x.responseText);
        result = Seriva_GetResult(x);
        if (result.error)
        {
            Seriva_DebugError(result.error);
        }
        if (updatePageAfterCallBack)
        {
            Seriva_UpdatePage(result);
        }
        Seriva_EvalClientSideScript(result);
    }
    return result;
}

function Seriva_GetResult(x)
{
    var result =
    {
        "value": null, "error": "BADRESPONSE"
    };
    try
    {
        result = eval("(" + x.responseText + ")");
    }
    catch (e)
    {
    }
    return result;
}

function Seriva_SetHiddenInputValue(form, name, value)
{
    if (form[name])
    {
        form[name].value = value;
    }
    else
    {
        var input = document.createElement("input");
        input.setAttribute("name", name);
        input.setAttribute("type", "hidden");
        input.setAttribute("value", value);
        form.appendChild(input);
        form[name] = input;
    }
}

function Seriva_FireEvent(eventTarget, eventArgument, clientCallBack,
    clientCallBackArg, includeControlValuesWithCallBack,
    updatePageAfterCallBack)
{
    var form = document.getElementById(Seriva_FormID);
    Seriva_SetHiddenInputValue(form, "__EVENTTARGET", eventTarget);
    Seriva_SetHiddenInputValue(form, "__EVENTARGUMENT", eventArgument);
    Seriva_CallBack(null, null, null, null, null, clientCallBack,
        clientCallBackArg, includeControlValuesWithCallBack,
        updatePageAfterCallBack);
    form.__EVENTTARGET.value = "";
    form.__EVENTARGUMENT.value = "";
}

function Seriva_UpdatePage(result)
{
    var form = document.getElementById(Seriva_FormID);
    if (result.viewState)
    {
        Seriva_SetHiddenInputValue(form, "__VIEWSTATE", result.viewState);
    }
    if (result.viewStateEncrypted)
    {
        Seriva_SetHiddenInputValue(form, "__VIEWSTATEENCRYPTED",
            result.viewStateEncrypted);
    }
    if (result.eventValidation)
    {
        Seriva_SetHiddenInputValue(form, "__EVENTVALIDATION",
            result.eventValidation);
    }
    if (result.controls)
    {
        for (var controlID in result.controls)
        {
            var containerID = "__" + controlID.split("$").join("_") + "__";
            var control = document.getElementById(containerID);
            if (control)
            {
                control.innerHTML = result.controls[controlID];
                if (result.controls[controlID] == "")
                {
                    control.style.display = "none";
                }
                else
                {
                    control.style.display = "";
                }
            }
        }
    }
}

function Seriva_EvalClientSideScript(result)
{
    if (result.script)
    {
        for (var i = 0; i < result.script.length; ++i)
        {
            try
            {
                eval(result.script[i]);
            }
            catch (e)
            {
                alert("Error evaluating client-side script!\n\nScript: " +
                    result.script[i] + "\n\nException: " + e);
            }
        }
    }
}

function Seriva_DebugRequestText(text){}

function Seriva_DebugResponseText(text){}

function Seriva_DebugError(text){}

function Seriva_InvokePageMethod(methodName, args, callBack, context, includeControls, updatePage)
{
    return Seriva_CallBack(null, "Page", null, methodName, args, callBack,
        context, includeControls, updatePage);
}

function Seriva_InvokeControlMethod(id, methodName, args, callBack, context, includeControls, updatePage)
{
    return Seriva_CallBack(null, "Control", id, methodName, args, callBack,
        context, includeControls, updatePage);
}

function SerivaButton_Click(button, id, causesValidation, textDuringCallBack,
    enabledDuringCallBack, preCallBackFunction, postCallBackFunction,
    callBackCancelledFunction, includeControlValuesWithCallBack,
    updatePageAfterCallBack)
{
    var preCallBackResult = true;
    if (preCallBackFunction)
    {
        preCallBackResult = preCallBackFunction(button);
    }
    if (typeof preCallBackResult == "undefined" || preCallBackResult)
    {
        var valid = true;
        if (causesValidation && typeof Page_ClientValidate == "function")
        {
            valid = Page_ClientValidate();
        }
        if (valid)
        {
            var text = button.value;
            if (textDuringCallBack)
            {
                button.value = textDuringCallBack;
            }
            var enabled = !button.disabled;
            button.disabled = !enabledDuringCallBack;
            Seriva_FireEvent(id, "", function(result)
            {
                if (postCallBackFunction)
                {
                    postCallBackFunction(button);
                }
                button.disabled = !enabled; button.value = text;
            }
            , null, includeControlValuesWithCallBack, updatePageAfterCallBack)
                ;
        }
    }
    else if (callBackCancelledFunction)
    {
        callBackCancelledFunction(button);
    }
}

function SerivaLinkButton_Click(button, id, causesValidation,
    textDuringCallBack, enabledDuringCallBack, preCallBackFunction,
    postCallBackFunction, callBackCancelledFunction,
    includeControlValuesWithCallBack, updatePageAfterCallBack)
{
    var preCallBackResult = true;
    if (preCallBackFunction)
    {
        preCallBackResult = preCallBackFunction(button);
    }
    if (typeof preCallBackResult == "undefined" || preCallBackResult)
    {
        var valid = true;
        if (causesValidation && typeof Page_ClientValidate == "function")
        {
            valid = Page_ClientValidate();
        }
        if (valid)
        {
            var text = button.innerHTML;
            if (textDuringCallBack)
            {
                button.innerHTML = textDuringCallBack;
            }
            var enabled = !button.disabled;
            button.disabled = !enabledDuringCallBack;
            Seriva_FireEvent(id, "", function(result)
            {
                if (postCallBackFunction)
                {
                    postCallBackFunction(button);
                }
                button.disabled = !enabled; button.innerHTML = text;
            }
            , null, includeControlValuesWithCallBack, updatePageAfterCallBack)
                ;
        }
    }
    else if (callBackCancelledFunction)
    {
        callBackCancelledFunction(button);
    }
}

function SerivaImageButton_Click(button, id, causesValidation,
    imageUrlDuringCallBack, enabledDuringCallBack, preCallBackFunction,
    postCallBackFunction, callBackCancelledFunction,
    includeControlValuesWithCallBack, updatePageAfterCallBack)
{
    var preCallBackResult = true;
    if (preCallBackFunction)
    {
        preCallBackResult = preCallBackFunction(button);
    }
    if (typeof preCallBackResult == "undefined" || preCallBackResult)
    {
        var valid = true;
        if (causesValidation && typeof Page_ClientValidate == "function")
        {
            valid = Page_ClientValidate();
        }
        if (valid)
        {
            var imageUrl = button.src;
            if (imageUrlDuringCallBack)
            {
                button.src = imageUrlDuringCallBack;
            }
            var enabled = !button.disabled;
            button.disabled = !enabledDuringCallBack;
            Seriva_FireEvent(id, "", function(result)
            {
                if (postCallBackFunction)
                {
                    postCallBackFunction(button);
                }
                button.disabled = !enabled; button.src = imageUrl;
            }
            , null, includeControlValuesWithCallBack, updatePageAfterCallBack)
                ;
        }
    }
    else if (callBackCancelledFunction)
    {
        callBackCancelledFunction(button);
    }
}

function SerivaTextBox_TextChanged(textBox, id,
    includeControlValuesWithCallBack, updatePageAfterCallBack)
{
    Seriva_FireEvent(id, "", function(result){}
    , null, includeControlValuesWithCallBack, updatePageAfterCallBack);
}

function SerivaRadioButtonList_OnClick(e)
{
    var target = e.target || e.srcElement;
    var eventTarget = target.id.split("_").join("$");
    Seriva_FireEvent(eventTarget, "", function(){}
    , null, true, true);
}