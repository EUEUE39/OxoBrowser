# OxoBrowser
基于c#+cefsharp实现的DMM游戏浏览器，可以把游戏区域单独提取出来显示，不用再去另外开网页，目前支持舰娘和刀剑乱舞两个游戏。
已实现部分游戏提取、顶置、代理、关闭声音、缩放比例等基本功能。

## 关于本fork
额外支持 あんさんぶるスターズBasic 网页

## 开发指南
你可以自己fork一份代码轻松实现自己的DMM游戏浏览器。

### 添加游戏类型
* 在AppConfig.cs中定义新游戏类型GameTypeEnum，并指定SizeWithGameType函数中游戏的宽高。
* 在FlyoutConfigDialog中添加新游戏的选项及初始化、保存时调用的方法。
* ChromeMain_LoadingStateChanged函数中实现对应游戏的提取代码，可参考Touken及KanColle的代码根据Chrome调试修改。

### 实现数据包拦截
在HttpPacketHookManager.cs中可以设置需要过滤的网站地址及请求类型，以及处理函数
```csharp
/// <summary>
/// 该request是否需要被拦截，拦截后会被提交到PacketRoute
/// </summary>
/// <param name="request"></param>
/// <returns></returns>
public static bool HookThisRequest(IRequest request)
{
    var url = new Uri(request.Url);
    var extension = url.ToString().ToLower();
    if (request.Method == "POST" && url.AbsoluteUri.Contains("touken-ranbu.jp/"))
    {
        return true;
    }
    return false;
}

public static int PacketRoute(string path, string result, string postData, string hostName, string headers)
{
    //TODO 2021.03.02 使用这里作为包处理节点

    return 0;
}
```

### 界面预览
![image](https://github.com/user-attachments/assets/dd52e3cd-fe99-484b-9349-8b1796db2749)



License (MIT) 
