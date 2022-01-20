using AdelTest.SyncData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AdelTest.Controllers
{
    public class ab
    {
        public string a { get; set; }
        public int b { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IMyHttpClient _client;

        public TestController(IMyHttpClient client)
        {
            _client = client;
        }

        [HttpGet("ee")]
        public ActionResult<ab> AB()
        {
            return new ab() { a = "as", b = 12 };
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            try
            {
                var res = await _client.Test();
                return res.content + " " + $"code:{res.code}";
            }
            catch (Exception e)
            {

                return "ERROR" + e.Message;
            }
        }
        [HttpGet("success")]
        public ActionResult<string> GetTest()
        {
            return Ok("success");

        }








        [HttpGet("culture")]
        public ActionResult<string> GetCulture()
        {
            string st = @"<!DOCTYPE HTML>
<html lang='en - US'>
  < head >
  
    < meta charset = 'UTF-8' />
   
     < meta http - equiv = 'Content-Type' content = 'text/html; charset=UTF-8' />
        
          < meta http - equiv = 'X-UA-Compatible' content = 'IE=Edge,chrome=1' />
             
               < meta name = 'robots' content = 'noindex, nofollow' />
                
                  < meta name = 'viewport' content = 'width=device-width,initial-scale=1' />
                   
                     < title > Just a moment...</ title >
                      
                        < style type = 'text/css' >
                           html, body { width: 100 %; height: 100 %; margin: 0; padding: 0; }
            body {
                background - color: #ffffff; color: #000000; font-family:-apple-system, system-ui, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, 'Helvetica Neue',Arial, sans-serif; font-size: 16px; line-height: 1.7em;-webkit-font-smoothing: antialiased;}
    h1 {
                    text - align: center; font - weight:700; margin: 16px 0; font - size: 32px; color:#000000; line-height: 1.25;}
    p { font - size: 20px; font - weight: 400; margin: 8px 0; }
                    p, .attribution, { text - align: center; }
    #spinner {margin: 0 auto 30px auto; display: block;}
    .attribution { margin - top: 32px; }
                    @keyframes fader     { 0 % { opacity: 0.2; } 50 % { opacity: 1.0; } 100 % { opacity: 0.2; } }
    @-webkit - keyframes fader { 0 % { opacity: 0.2; } 50 % { opacity: 1.0; } 100 % { opacity: 0.2; } }
    #cf-bubbles > .bubbles { animation: fader 1.6s infinite;}
    #cf-bubbles > .bubbles:nth-child(2) { animation-delay: .2s;}
    #cf-bubbles > .bubbles:nth-child(3) { animation-delay: .4s;}
    .bubbles {
                        background - color: #f58220; width:20px; height: 20px; margin:2px; border-radius:100%; display:inline-block; }
    a {
                        color: #2c7cb0; text-decoration: none; -moz-transition: color 0.15s ease; -o-transition: color 0.15s ease; -webkit-transition: color 0.15s ease; transition: color 0.15s ease; }
    a: hover{
                            color: #f4a15d}
    .attribution{ font - size: 16px; line - height: 1.5; }
    .ray_id{ display: block; margin - top: 8px; }
    #cf-wrapper #challenge-form { padding-top:25px; padding-bottom:25px; }
    #cf-hcaptcha-container { text-align:center;}
    #cf-hcaptcha-container iframe { display: inline-block;}
  </ style >

      < meta http - equiv = 'refresh' content = '35' >
     
       < script type = 'text/javascript' >
          //<![CDATA[
          (function(){

                                    window._cf_chl_opt ={
                                    cvId: '2',
        cType: 'non-interactive',
        cNounce: '70968',
        cRay: '6a2bb6aaffcc36de',
        cHash: '6f92dc0a0a1caec',
        cFPWv: 'b',
        cTTimeMs: '1000',
        cRq:
                                        {
                                        ru: 'aHR0cHM6Ly9hcGkuYXV0aG9yLnRvZGF5L3YxL3dvcmsvMTM0NjYzL21ldGEtaW5mbw==',
          ra: '',
          rm: 'R0VU',
          d: 'Mn40eDpTKk8jfaLLcmDl530veqQzq0xdmWOO6K7zQHBUCt0/NErJE4rT/wcuNVApococsmtuywITbciOo+P9BUs9gCVal3cxZMwCSGVu8pvkVtcFjcadGFkO6fbI1RdsZ8cqMhHIM+7HieOHx1IdPFYYVH+5E48VuteVifg0BFVWFuxuV8PdifJpWdvpfsqRRxe6jPIxoFqr9bblLT7jOzJBjoNxxaS2gIqpxl7lQTazgsIsMUNYvkfkhZGoOzn+R6lXwzfg2Up6Tzm6ja3DktDb1RjGiCOa77XeA6yeEBfUyUDKSCU1taw1uaKtNWMnWIiV4IZcYNLXWtaDG5bpgAGMoq7UvxaqKpDmbQ00UWrFL1yDn1oEJvCQx+mOACmDsCcHqfqEIJtYtt8Rl/LaTdGQ7vtCw6AANe75L14VTJf6WCAZU5fRtZ2TdPJaEydQW3E9CItSw5MzvCkbFmMTn5K7mMovmW8wOvPLvN2qr2/IK48112TbH724JGdk8XDkKvY1ykLuF08RvRC8dPx6S9RH2TzUBno67G6o/ESTZXM=',
          t: 'MTYzNDk5OTk1My4xMjMwMDA=',
          m: 'MC+oN2KTjanvSWHyTMB4Cyik9M+JjULoy0fczhka1Mk=',
          i1: 'uD2xGp2zvg/BwNcAIYMxhg==',
          i2: 'tq4YGV4vTbnlhf7zAFQPUw==',
          zh: 'GgOcwwAf++4IGXbFFAyKTgxJvtmpMgCEhtKdX7Dgb60=',
          uh: 'DV4j3Tmrbi5Rs1q3ahwVS6SgbPbI7np5884QO1u1Cgg=',
          hh: '3ly1+4BZOEGaYaeyiMzo/+m86KaK0AfEFSVMoHB7cRc=',
        }
                                    }
                                    window._cf_chl_enter = function(){ window._cf_chl_opt.p = 1};

                                })();
    //]]>
  </ script >



</ head >
< body >
  < table width = '100%' height = '100%' cellpadding = '20' >
     
         < tr >
     
           < td align = 'center' valign = 'middle' >
        
                  < div class='cf-browser-verification cf-im-under-attack'>
  <noscript>
    <h1 data-translate='turn_on_js' style='color:#bd2426;'>Please turn JavaScript on and reload the page.</h1>
  </noscript>
  <div id = 'cf-content' style= 'display:none' >


    < div id= 'cf-bubbles' >
      < div class='bubbles'></div>
      <div class='bubbles'></div>
      <div class='bubbles'></div>
    </div>
    <h1><span data-translate='checking_browser'>Checking your browser before accessing</span> author.today.</h1>
    
    <div id = 'no-cookie-warning' class='cookie-warning' data-translate='turn_on_cookies' style='display:none'>
      <p data-translate='turn_on_cookies' style='color:#bd2426;'>Please enable Cookies and reload the page.</p>
    </div>
    <p data-translate='process_is_automatic'>This process is automatic.Your browser will redirect to your requested content shortly.</p>
    <p data-translate='allow_5_secs' id='cf-spinner-allow-5-secs' >Please allow up to 5 seconds&hellip;</p>
    <p data-translate='redirecting' id='cf-spinner-redirecting' style='display:none'>Redirecting&hellip;</p>
  </div>
   
  <form class='challenge-form' id='challenge-form' action='/v1/work/134663/meta-info?__cf_chl_jschl_tk__=pmd_4hXS1MX4gH5TAVu8CrR57hC.kLbh5ipMISs0lWwH8Os-1634999953-0-gqNtZGzNAmWjcnBszQiR' method='POST' enctype='application/x-www-form-urlencoded'>
    <input type = 'hidden' name='md' value='B0qWHd3ZnXgYhB2ipBNs4K_FFz0ybxlqjodgFv.juoo-1634999953-0-Ac0sEkXTdXswVXFu6PHQ7DFUoskD7cKcDv8Ht9yEjDqfxpVRBzFrf-Yoym6dOhHU9bL1Cvjj096mEc8ozGeyExt_ZongaMNbNGzU7dOb07ICQejx-tG8Mbbr62Rd39SwH8vcYK69zcljhn8rQEkz1SNG67goP4OqMLff9KH6WN3uTjbMd797If7wEa8-0riZOlLuJhhdovq5QkWv7NR15ODV4qKS-deOLxO8dILlm0wCUNc6iZgTWtUmU4on0pPW32WN4wzeBTYf3YSuOipE1MgUo_y7lBu8v-w7KdIEgj6lmG9MoWJNuCzGGmcVAilJC0FmjyoAOj-73hFFlFf5JODBRkYmYlwU1MWmqSNSfOBRgqYshG49xuHWD3On-Jlla7dIAl7q6wD-BKdv4Xea1qQMY9c2cW5xLW5UomA4mYcnQVgpzYlZ_QybQKnnNxcBxcRfatdUz21pYGmio6jfj4IrKj8C4oEn38zwvvTjoF_basxuBoPqBgebqBdE_FCmpl524DPUYQg2cY7uu8gpnoYvmqmy6jBkShC0aRqsJ881' />
    <input type = 'hidden' name='r' value='P4bovkJQ6gm_GX0YSzKNwT2X_NrJtMcSZysCWqDg350-1634999953-0-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'/>
    <input type = 'hidden' value='44ce981ed5f9e4b5698199aab2de2bc3' id='jschl-vc' name='jschl_vc'/>
    <!-- <input type = 'hidden' value='' id='jschl-vc' name='jschl_vc'/> -->
    <input type = 'hidden' name='pass' value='1634999954.123-XHaGnKJd0M'/>
    <input type = 'hidden' id='jschl-answer' name='jschl_answer'/>
  </form>
     
    <script type = 'text/javascript' >
      //<![CDATA[
      (function(){
          var a = document.getElementById('cf-content');
        a.style.display = 'block';
          var isIE = / (MSIE | Trident\/|Edge\/)/i.test(window.navigator.userAgent);
          var trkjs = isIE ? new Image() : document.createElement('img');
        trkjs.setAttribute('src', '/cdn-cgi/images/trace/jschal/js/transparent.gif?ray=6a2bb6aaffcc36de');
          trkjs.id = 'trk_jschal_js';
          trkjs.setAttribute('alt', '');
          document.body.appendChild(trkjs);
          var cpo = document.createElement('script');
        cpo.type='text/javascript';
          cpo.src='/cdn-cgi/challenge-platform/h/b/orchestrate/jsch/v1?ray=6a2bb6aaffcc36de';
          document.getElementsByTagName('head')[0].appendChild(cpo);
    }
    ());
      //]]>
    </script>
  

  
  <div id = 'trk_jschal_nojs' style='background-image:url('/cdn-cgi/images/trace/jschal/nojs/transparent.gif?ray=6a2bb6aaffcc36de')'> </div>
</div>

          
          <div class='attribution'>
            DDoS protection by<a rel='noopener noreferrer' href='https://www.cloudflare.com/5xx-error-landing/' target='_blank'> Cloudflare</a>
            <br />
            <span class='ray_id'>Ray ID: <code>6a2bb6aaffcc36de</code></span>
          </div>
      </td>
     
    </tr>
  </table>
</body>
</html>
 code:503";

            st = st.Substring(5778, st.Length - 5778);
            var i = st.IndexOf("' method='POST'", 0);
            var __cf_chl_jschl_tk__ = st.Substring(0, i);
            var mdstart = st.IndexOf("name='md'", 0);
            st = st.Substring(mdstart + 17, st.Length - mdstart - 17);
            var mdend = st.IndexOf("/>", 0);
            var md = st.Substring(0, mdend - 2);
            var rstart = st.IndexOf("name='r'", 0);
            st = st.Substring(rstart + 16, st.Length - rstart - 16);
            var rend = st.IndexOf("/>", 0);
            var r = st.Substring(0, rend - 1);

            var vcend = st.IndexOf("name='jschl_vc'", 0);
            var vctst = st.Substring(0, vcend);
            var vcttst = vctst.IndexOf("name='jschl_vc'", 0);
            vctst = vctst.Substring(vcttst - 100, vcttst);
            return Ok(CultureInfo.CurrentCulture.Name);

        }
    }
}

