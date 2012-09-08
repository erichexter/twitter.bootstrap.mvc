using System.Web.Mvc;

namespace $rootnamespace$.Controllers
{
    public class BootstrapBaseController: Controller
    {

	public void Success(string message)
	{
                TempData.Add("success",message);	
	}
	public void Information(string message){
                TempData.Add("info",message);		
	}
	public void Error(string message){
        	TempData.Add("error",message);				
	}
    }
}
