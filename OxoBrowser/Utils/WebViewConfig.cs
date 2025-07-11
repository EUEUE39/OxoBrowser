﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using WebBrowser = System.Windows.Controls.WebBrowser;
using System.Reflection;
using System.Diagnostics;
using OxoBrowser;
using CefSharp;
using System.Windows.Interop;
using OxoBrowser.Services;
using System.Windows.Threading;
using System.Windows;


namespace Base
{
    class WebViewConfig
    {
        private const string kDMMDoukenCSS = """
            body {
                margin:0;
                overflow:hidden;
            }

            #game_frame {
            	position:fixed;
            	left:50%;
            	top:0px;
            	margin-left:-480px;
            	z-index:1;
            }

            ul.area-menu{
            	display:none;
            }

            .dmm-ntgnavi {
            	display:none;
            }
            """;

        public static void GetKanColle2ndHtml5Core(CefSharp.Wpf.ChromiumWebBrowser browser)
        {
            try
            {
	            browser.ExecuteScriptAsync("var node = document.createElement('style'); " +
	                "node.innerHTML = 'html, body, iframe {overflow:hidden;margin:0;}'; " +
	                "document.body.appendChild(node);");
	
	            browser.ExecuteScriptAsync("var node = document.createElement('style'); " +
	                "node.innerHTML = 'game_frame {position:fixed; left:50%; top:0px; margin-left:-480px; z-index:1;}'; " +
	                "document.body.appendChild(node);");
	
	            browser.ExecuteScriptAsync("var node = document.createElement('style'); " +
	                "node.innerHTML = 'ul.area-menu {display: none;}'; " +
	                "document.body.appendChild(node);");
	            browser.ExecuteScriptAsync("var node = document.createElement('style'); " +
	                "node.innerHTML = '.dmm-ntgnavi {display: none;}'; " +
	                "document.body.appendChild(node);");
	
	            var game_frame = browser.GetBrowser().GetFrame("game_frame");
	            var source = game_frame?.GetSourceAsync();
	            source?.Wait();
	
	
	            //Browser.SetZoomLevel(Math.Log(zoomFactor, 1.2));
	
	
	            //if (StyleSheetApplied)
	            //{
	            //    Browser.Size = Browser.MinimumSize = new Size(
	            //        (int)(KanColleSize.Width * zoomFactor),
	            //        (int)(KanColleSize.Height * zoomFactor)
	            //        );
	
	            //    CenteringBrowser();
	            //}
	            //"overlap-contents"
	            //var contents_iframe = browser.GetBrowser().GetFrame("contents_iframe");
	            //var list2 = game_frame.Browser.GetFrameNames();
	            //var list = browser.GetBrowser().GetFrameNames();
	            //browser.GetBrowser();
	
	            game_frame?.ExecuteJavaScriptAsync("document.getElementById('spacing_top').style.height = '0px'");
            }
            catch (System.Exception ex)
            {
            	
            }

        }



        public static void GetToukenHtml5Core(CefSharp.Wpf.ChromiumWebBrowser browser)
		{
            try
            {
                //var mainframe = GetFrameContainsUrl(browser, @"http://pc-play.games.dmm.com/play/tohken");
                if (browser.Address.StartsWith("https://play.games.dmm.com/game/tohken"))
                {
                    try
                    {
                        var gameframe = GetFrame(browser, "game_frame");
                        if (gameframe != null)
                        {
                            string script = "document.body.style.overflow = 'hidden';";
                            gameframe.ExecuteJavaScriptAsync(script);
                        } 
                        else
                        {
                            Task.Run(async () => {

                                await Task.Delay(5000);

                                Dispatcher mainThreadDispatcher = Application.Current.Dispatcher;
                                mainThreadDispatcher.Invoke(() =>
                                {
                                    var gameframe = GetFrame(browser, "game_frame");
                                    if (gameframe != null)
                                    {
                                        string script = "document.body.style.overflow = 'hidden';";
                                        gameframe.ExecuteJavaScriptAsync(script);
                                    }
                                });


                            });
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    browser.ExecuteScriptAsync(@"
                    (function() {
                        const styles = `
                            html, body, iframe {overflow:hidden;margin:0;}
                            #game_frame {overflow: hidden !important; position: fixed; left: 0; top: 0; width: 100% !important; height: 100% !important; z-index:1;}
                            ul.area-menu {display: none;}
                            .dmm-ntgnavi {display: none;}
                        `;

                        // 检查相同的样式
                        const existingStyles = document.getElementsByTagName('style');
                        for (let i = 0; i < existingStyles.length; i++) {
                            if (existingStyles[i].innerHTML.includes(styles)) {
                                return;
                            }
                        }

                        const styleNode = document.createElement('style');
                        styleNode.innerHTML = styles;
                        document.body.appendChild(styleNode);
                    })();
                    ");

                }

            }
            catch (Exception ex)
            {
                EasyLog.Write(ex.ToString());
            }
        }

        public static void GetEnStarHtml5Core(CefSharp.Wpf.ChromiumWebBrowser browser)
        {
            try
            {
                //var mainframe = GetFrameContainsUrl(browser, @"https://play.games.dmm.com/game/ensemble_stars");
                if (browser.Address.StartsWith("https://play.games.dmm.com/game/ensemble_stars"))
                {
                    try
                    {
                        var gameframe = GetFrame(browser, "game_frame");
                        Debug.WriteLine("尝试获取webgl_frame");
                        if (gameframe != null)
                        {
                            string script = "document.body.style.overflow = 'hidden';";
                            gameframe.ExecuteJavaScriptAsync(script);
                            gameframe.ExecuteJavaScriptAsync(@"
                                                const styles = `
                                                    html, body, iframe {overflow:hidden;margin:0;}
                                                    #game_frame {overflow: hidden !important; position: fixed; left: 0; top: 0; width: 100% !important; height: 100% !important; z-index:1;}
                                                    ul.area-menu {display: none;}
                                                    .dmm-ntgnavi {display: none;}
                                                    .contentWrap {padding: 0 !important;}
                                                    .gameArea-wrap {margin: 0 !important;}
                                                    /*canvas#gameContainer,.gameArea-wrap {width: 1200px !important; height: 720px !important;}*/
                                                `;
                                                // 检查相同的样式
                                                const existingStyles = document.getElementsByTagName('style');
                                                //for (let i = 0; i < existingStyles.length; i++) {
                                                //    if (existingStyles[i].innerHTML.includes(styles)) {
                                                //        return;
                                                //    }
                                                //}

                                                const styleNode = document.createElement('style');
                                                styleNode.innerHTML = styles;
                                                document.body.appendChild(styleNode);

                                                //让iframe#wegbl_frame 的height指定为720p
                                                //const iframe = document.getElementById('webgl_frame');
                                                //iframe.height = '720'; // 或者 iframe.style.height = '720px';
                                        ");
                            Debug.WriteLine("执行js完成1");
                        }
                        else
                        {
                            Task.Run(async () => {

                                await Task.Delay(40000);

                                Dispatcher mainThreadDispatcher = Application.Current.Dispatcher;
                                mainThreadDispatcher.Invoke(async () =>
                                {
                                    var gameframe = GetFrame(browser, "game_frame");
                                    Debug.WriteLine("尝试获取game_frame");
                                    if (gameframe != null)
                                    {
                                        string script = "document.body.style.overflow = 'hidden';";
                                        gameframe.ExecuteJavaScriptAsync(script);
                                        gameframe.ExecuteJavaScriptAsync(@"
                                                const styles = `
                                                    html, body, iframe {overflow:hidden;margin:0;}
                                                    #game_frame {overflow: hidden !important; position: fixed; left: 0; top: 0; width: 100% !important; height: 100% !important; z-index:1;}
                                                    ul.area-menu {display: none;}
                                                    .dmm-ntgnavi {display: none;}
                                                    .contentWrap {padding: 0 !important;}
                                                    .gameArea-wrap {margin: 0 !important;}
                                                    /*canvas#gameContainer,.gameArea-wrap {width: 1200px !important; height: 720px !important;}*/
                                                `;
                                                // 检查相同的样式
                                                const existingStyles = document.getElementsByTagName('style');
                                                //for (let i = 0; i < existingStyles.length; i++) {
                                                //    if (existingStyles[i].innerHTML.includes(styles)) {
                                                //        return;
                                                //    }
                                                //}

                                                const styleNode = document.createElement('style');
                                                styleNode.innerHTML = styles;
                                                document.body.appendChild(styleNode);

                                                //让iframe#wegbl_frame 的height指定为720p
                                                //const iframe = document.getElementById('webgl_frame');
                                                //iframe.height = '720'; // 或者 iframe.style.height = '720px';
                                        ");
                                        Debug.WriteLine("执行js完成2");
                                    }
                                });


                            });
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    browser.ExecuteScriptAsync(@"
                    (function() {
                            const styles = `
                                html, body, iframe {overflow:hidden;margin:0;}
                                #game_frame {overflow: hidden !important; position: fixed; left: 0; top: 0; width: 100% !important; height: 100% !important; z-index:1;}
                                ul.area-menu {display: none;}
                                .dmm-ntgnavi {display: none;}
                                .contentWrap {padding: 0 !important;}
                                .gameArea-wrap {margin: 0 !important;}
                                /*canvas#gameContainer,.gameArea-wrap {width: 1200px !important; height: 720px !important;}*/
                            `;

                            // 检查相同的样式
                            const existingStyles = document.getElementsByTagName('style');
                            for (let i = 0; i < existingStyles.length; i++) {
                                if (existingStyles[i].innerHTML.includes(styles)) {
                                    return;
                                }
                            }

                            const styleNode = document.createElement('style');
                            styleNode.innerHTML = styles;
                            document.body.appendChild(styleNode);

                            //让iframe#wegbl_frame 的height指定为720p
                            //const iframe = document.getElementById('game_frame');
                            //const iframeDocument = iframe.contentDocument || iframe.contentWindow.document;
                            //const iframeElement = iframeDocument.getElementById('targetElement');
                            //iframe.onload = function(){
                            //    iframeDocument.body.appendChild(styleNode);
                            //}

                        })();
                    ");

                }

            }
            catch (Exception ex)
            {
                EasyLog.Write(ex.ToString());
            }
            Debug.WriteLine("结束修改窗体");
        }


        public void ApplyStyleSheet(FrameLoadEndEventArgs e = null)
        {
            
        }

        public static IFrame GetFrame(CefSharp.Wpf.ChromiumWebBrowser browser, string frameName)
        {
            IFrame frame = null;
            var identifiers = browser.GetBrowser().GetFrameIdentifiers();
            foreach (var i in identifiers)
            {
                frame = browser.GetBrowser().GetFrame(i);
                //Debug.WriteLine($"frameName: {frame.Name}");
            }
            foreach (var i in identifiers)
            {
                frame = browser.GetBrowser().GetFrame(i);
                if (frame.Name == frameName)
                    return frame;
            }

            return null;
        }

        public static IFrame GetFrameContainsUrl(CefSharp.Wpf.ChromiumWebBrowser browser, string url)
        {
            IFrame frame = null;
            var identifiers = browser.GetBrowser().GetFrameNames();
            foreach (var item in identifiers)
            {
                frame = browser.GetBrowser().GetFrame(item);

                if (frame.Url.Contains(url))
                {
                    return frame;
                }
            }

            return null;
        }

    }
}
